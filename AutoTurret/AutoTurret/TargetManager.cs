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
    class TargetManager
    {
        #region Variables
        private BackgroundWorker runAllTrackers = new BackgroundWorker();

        private double overlapThreshold = .1;

        private List<Rectangle> targets = new List<Rectangle>();
        private TargetTrack tracker = new TargetTrack();

        #endregion
        public TargetManager(){}
        public void UpdateTargets(Image<Bgr, byte> currentFrame, Image<Bgr, byte> previousFrame, Image<Bgr,byte> acquireTargetState)
        {
            if (targets.Count != 0)
            {
                RemoveNonTargets();
                int temp, limit = targets.Count;
                for (temp = 0; temp < targets.Count; ++temp)
                {
                    try
                    {
                        targets[temp] = tracker.CalculateNewPosition(currentFrame, previousFrame, targets[temp]);
                    }
                    catch
                    {
                        targets.Clear();
                    }
                }
            }
            tracker.setInternalPreviousFrame(currentFrame);
        }
        public List<Rectangle> GetTargets()
        {
            RemoveNonTargets();
            return targets;
        }
        public Rectangle GetBiggestTarget()
        {
            RemoveNonTargets();
            Rectangle biggestTarget = new Rectangle();
            if (targets.Count == 0)
                return new Rectangle(0, 0, 0, 0);
            foreach (var rec in targets)
            {
                if (KnownTargetArea(rec)>KnownTargetArea(biggestTarget))
                    biggestTarget=rec;
            }
            return biggestTarget;
        }
        public void NewTarget(Rectangle Target)
        {
            bool isNewTarget = true;
            foreach (var rec in targets)
            {
                if (CalculateOverlapArea(rec, Target) > 0)//KnownTargetArea(rec) * overlapThreshold)
                {
                    isNewTarget = false;
                }
            }
            if (isNewTarget)
            {
                targets.Add(Target);
            }
        }
        private double CalculateOverlapArea(Rectangle rec1, Rectangle rec2)
        {
            double xOverLap = Math.Max(0, Math.Min(rec1.Right, rec2.Right) - Math.Max(rec1.Left, rec2.Left));
            double yOverLap = Math.Max(0, Math.Min(rec1.Bottom, rec2.Bottom) - Math.Max(rec1.Top, rec2.Top));
            return xOverLap * yOverLap;
        }
        private double KnownTargetArea(Rectangle rec)
        {
            return rec.Width * rec.Height;
        }
        private void RemoveNonTargets()
        {
            int temp, limit = targets.Count;
            for (temp = 0; temp < limit; ++temp)
            {
                try
                {
                    if (targets[temp].Width == 0 || targets[temp].Height == 0)
                    {
                        targets.RemoveAt(temp);
                        temp--;
                        limit--;
                    }
                }
                catch
                {
                    break;
                }
            }
        }

        //temporary
        public Image<Gray, byte> getSMask()
        {
            return tracker.sMask;
        }
        public Image<Bgr, byte> getFeats()
        {
            return tracker.feats;
        }
        public Image<Bgr, byte> getPrev()
        {
            return tracker.prevMasked;
        }
    }
}
