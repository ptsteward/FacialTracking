using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.UI;
using Emgu.CV.CvEnum;
using Emgu.CV.Features2D;
using Emgu.CV.Util;

namespace AutoTurret
{
    class AcquireTarget
    {
        #region Variables
        private CascadeClassifier frontalFace = new CascadeClassifier(Application.StartupPath + "/Cascades/lbpcascade_frontalface.xml");
        private CascadeClassifier faceClassifier = new CascadeClassifier(Application.StartupPath + "/Cascades/haarcascade_profileface.xml");

        private double scale = 1.1;

        private int minNeighbors = 3;

        private Size minSize = new Size(20,20);
        private Size maxSize = Size.Empty;

        private List<Rectangle> targets = new List<Rectangle>();
        #endregion

        public AcquireTarget(){}
        public Rectangle FindTargets(Image<Gray, byte> frame)
        {
            if (targets.Count != 0)
                targets.Clear();
            ScanFrame(frame);
            //ScanFrameRight(frame);
            //ScanFrameLeft(frame);
            return returnBiggestTarget(targets);
        }
        private void ScanFrame(Image<Gray, byte> frame)
        {
            var target =  frontalFace.DetectMultiScale(frame, scale, minNeighbors, minSize, maxSize);
            foreach (var tar in target)
            {
                targets.Add(tar);
            }
            //Console.WriteLine(targets.Count);
        }
        private void ScanFrameRight(Image<Gray, byte> frame)
        {
            var target = faceClassifier.DetectMultiScale(frame, scale, minNeighbors, minSize, maxSize);
            foreach (var tar in target)
            {
                targets.Add(tar);
            }
        }
        private void ScanFrameLeft(Image<Gray, byte> frame)
        {
            frame._Flip(FLIP.HORIZONTAL);
            Size frameSize = new Size(320, 240);
            var target = faceClassifier.DetectMultiScale(frame, scale, minNeighbors, minSize, maxSize);
            for (int i = 0; i < target.Length; i++)
            {
                target[i].X = (frameSize.Width / 2) + ((frameSize.Width / 2) - (target[i].X + target[i].Width));
            }
            foreach (var tar in target)
            {
                targets.Add(tar);
            }
        }
        private Rectangle returnBiggestTarget(List<Rectangle> targets)
        {
            Rectangle biggestRec  =new Rectangle(0,0,0,0);
            foreach(var rec in targets)
            {
                if(AreaOfRectangle(biggestRec) < AreaOfRectangle(rec))
                    biggestRec = rec;
            }
            return biggestRec;
        }
        private double AreaOfRectangle(Rectangle rec)
        {
            return rec.Width*rec.Height;
        }
    }
}
