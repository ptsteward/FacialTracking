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


using Pololu.UsbWrapper;
using Pololu.Usc;
namespace AutoTurret
{
    class ServoControl
    {
        #region Variables
        private const byte pan=1;
        private const byte tilt=2;

        private int Xcen=winSize.Width/2;
        private int Ycen=winSize.Height/2;

        private Rectangle target=new Rectangle();

        private static Size winSize=new Size(640, 480);

        //negative move right, down
        private int left = 1800,
                        right = 1200,
                        up = 1800,
                        down = 1200, deadP = 0, deadT = 0;

        #endregion

        public ServoControl() { }

        public void SetTarget(Rectangle newTarget)
        {
            target=newTarget;
        }
        public void MoveToTarget(Rectangle newTarget)
        {
            Console.WriteLine(newTarget.X + "," + newTarget.Y);

            int movPan = (newTarget.X - Xcen) + deadP;
            int movTilt = (Ycen - newTarget.Y) + deadT;
            Console.WriteLine(movPan + ","+ movTilt);
            SendCmd(pan, movPan);
            SendCmd(tilt, movTilt);
            //bool connected = (newTarget.X>Xcen?SendCmd(pan, right):SendCmd(pan, left));
              //   connected = (newTarget.Y>Ycen?SendCmd(tilt,up):SendCmd(tilt,down));
                 //if (!connected)
                     //Console.WriteLine("fail");
        }
        public void Left()
        {
            SendCmd(pan, left);
            Console.WriteLine("l");
        }
        public void Right()
        {
            SendCmd(pan, right);
            Console.WriteLine("r");
        }
        public void Up()
        {
            SendCmd(tilt, up);
            Console.WriteLine("u");
        }
        public void Down()
        {
            SendCmd(tilt, down);
            Console.WriteLine("D");
        }
        public void StopAll(int panDead, int tiltDead)
        {
            //Console.WriteLine("stop");
            //Console.WriteLine(panDead + "," + tiltDead);
            deadP = panDead;
            deadT = tiltDead;
            bool pn = SendCmd(pan, panDead);
            bool ti = SendCmd(tilt, tiltDead);
        }
        private bool SendCmd(Byte channel, int target)
        {
            try
            {
            using (Usc device=connectToDevice())
            {
                device.setTarget(channel, (ushort)(target*4));
                return true;
            }
            }
            catch (Exception exception)  
            {
                return false;
            }
        }

        Usc connectToDevice()
        {
            List<DeviceListItem> connectedDevices=Usc.getConnectedDevices();
            foreach (DeviceListItem dli in connectedDevices)
            {
                Usc device=new Usc(dli);
                return device;
            }
            throw new Exception("Could not find device");
        }
    }
}
