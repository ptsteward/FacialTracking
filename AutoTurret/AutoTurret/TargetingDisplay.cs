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

using DirectShowLib;

namespace AutoTurret
{
    public partial class TargetingDisplay : Form
    {
        #region Variables
        private AcquireTarget findTargets = new AcquireTarget();

        private BackgroundWorker videoStream = new BackgroundWorker();
        private BackgroundWorker findTargetsThread = new BackgroundWorker();
        private BackgroundWorker trackTargetsThread = new BackgroundWorker();
        private BackgroundWorker controlServosThread=new BackgroundWorker();

        private bool pauseVideo=false;
        private bool targetingAuto=false;

        private Capture video = new Capture(0);

        private int videoFramesCount = 0, findTargetFramesCount = 0, trackingFramesCount = 0;

        private Image<Bgr, byte> currentFrame = new Image<Bgr, byte>(frameSize);
        private Image<Bgr, byte> previousFrame = new Image<Bgr, byte>(frameSize);
        private Image<Bgr, byte> findTargetsOverlay = new Image<Bgr, byte>(frameSize);
        private Image<Bgr, byte> trackingOverlay = new Image<Bgr, byte>(frameSize);

        Rectangle newTarget;

        static private Size frameSize = new Size(640,480);

        private ServoControl servoControler=new ServoControl();

        private TargetManager manager = new TargetManager();

        System.Timers.Timer cameraFramesTimer = new System.Timers.Timer(1000);

        #endregion

        public TargetingDisplay()
        {
            InitializeComponent();
            ConfigureThreads();
            FillCameraSelectionBox();
            ConfigureAndStartFPSTimer();
            StartVideoThread();
        }
        private void ConfigureThreads()
        {
            videoStream.DoWork+=new DoWorkEventHandler(videoStream_DoWork);
            findTargetsThread.DoWork+=new DoWorkEventHandler(findTargetsThread_DoWork);
            trackTargetsThread.DoWork+=new DoWorkEventHandler(trackTargetsThread_DoWork);
            controlServosThread.DoWork+=new DoWorkEventHandler(controlServosThread_DoWork);

            videoStream.RunWorkerCompleted+=new RunWorkerCompletedEventHandler(videoStream_RunWorkerCompleted);
            findTargetsThread.RunWorkerCompleted+=new RunWorkerCompletedEventHandler(findTargetsThread_RunWorkerCompleted);
            trackTargetsThread.RunWorkerCompleted+=new RunWorkerCompletedEventHandler(trackTargetsThread_RunWorkerCompleted);
            controlServosThread.RunWorkerCompleted+=new RunWorkerCompletedEventHandler(controlServosThread_RunWorkerCompleted);
        }
        private void FillCameraSelectionBox()
        {
            int deviceIndex=0;
            List<KeyValuePair<int, string>> listCamerasData=new List<KeyValuePair<int, string>>();

            DsDevice[] systemCameras=DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice);

            foreach (DirectShowLib.DsDevice camera in systemCameras)
            {
                listCamerasData.Add(new KeyValuePair<int, string>(deviceIndex, camera.Name));
                deviceIndex++;
            }

            cameraSelectionBox.DataSource=null;
            cameraSelectionBox.Items.Clear();
            cameraSelectionBox.DataSource=new BindingSource(listCamerasData, null);
            cameraSelectionBox.DisplayMember="Value";
            cameraSelectionBox.ValueMember="Key";
        }
        private void StartVideoThread()
        {
            videoStream.RunWorkerAsync();
        }
        private void ConfigureAndStartFPSTimer()
        {
            cameraFramesTimer.AutoReset = true;
            cameraFramesTimer.Start();
            cameraFramesTimer.SynchronizingObject = lblCamFPS_out;
            cameraFramesTimer.Elapsed += new System.Timers.ElapsedEventHandler(UpdateFPS);
        }
        private void UpdateFPS(object sender, EventArgs e)
        {
            lblCamFPS_out.Text = videoFramesCount.ToString();
            lblAcquireFPS_out.Text = findTargetFramesCount.ToString();
            lblTrackFPS_out.Text = trackingFramesCount.ToString();
            videoFramesCount = 0;
            findTargetFramesCount = 0;
            trackingFramesCount = 0;
        }
        private Rectangle ScaleRectangleUp(Rectangle rec, double scale)
        {
            return new Rectangle(
                                (int)(rec.X*scale),
                                (int)(rec.Y*scale),
                                (int)(rec.Width*scale),
                                (int)(rec.Height*scale));
        }
        private Rectangle ScaleRectangleDown(Rectangle rec, double scale)
        {
            return new Rectangle(
                                (int)(rec.X + (rec.Width * (scale/2))),
                                (int)(rec.Y + (rec.Height * (scale/2))),
                                (int)(rec.Width * scale),
                                (int)(rec.Height * scale));
        }
        private Rectangle ScaleRectangleUpInPlace(Rectangle rec, double scale)
        {
            return new Rectangle(
                                (int)(rec.X - (rec.Width * (scale / 2))),
                                (int)(rec.Y - (rec.Height * (scale / 2))),
                                (int)(rec.Width * scale),
                                (int)(rec.Height * scale));
        }
        #region Threads
        #region Video Stream Thread
        void  videoStream_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            currentFrame = currentFrame.Add(findTargetsOverlay);
            currentFrame = currentFrame.Add(trackingOverlay);
            imgDisplay.Image = currentFrame;
            videoFramesCount++;
            if (!pauseVideo)
                videoStream.RunWorkerAsync();
            else
            {
                video.Dispose();
                video=new Capture(cameraSelectionBox.SelectedIndex);
                pauseVideo = !pauseVideo;
                videoStream.RunWorkerAsync();
            }
        }
        void videoStream_DoWork(object sender, EventArgs e)
        {
            previousFrame=currentFrame;
            currentFrame=video.QueryFrame();   
        }
        #endregion
        #region Find Targets
        void findTargetsThread_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            textBox1.BackColor = Color.DarkGray;
            if (targetingAuto)
            {
                findTargetFramesCount++;
                findTargetsThread.RunWorkerAsync();
            }
            else
            {
                findTargetsOverlay.SetValue(new Bgr(0, 0, 0));
            }
        }
        void  findTargetsThread_DoWork(object sender, DoWorkEventArgs e)
        {
            if (trackingOverlay.GetAverage().Red==0)
            {
                var rec=findTargets.FindTargets(currentFrame.Resize(320, 240, INTER.CV_INTER_LINEAR).Convert<Gray, byte>());
                if (rec!=null)
                {
                    rec=ScaleRectangleDown(rec, .5);
                    findTargetsOverlay.SetValue(new Bgr(0, 0, 0));
                    findTargetsOverlay.Draw(ScaleRectangleUp(rec, 2), new Bgr(0, 175, 175), 1);
                    newTarget=rec;
                    if (ScaleRectangleUp(rec, 2).X > (frameSize.Width / 2) + 100 || ScaleRectangleUp(rec, 2).X > (frameSize.Width / 2) - 100)
                    {
                        if (ScaleRectangleUp(rec, 2).Y > (frameSize.Height / 2) + 100 || ScaleRectangleUp(rec, 2).Y > (frameSize.Height / 2) - 100)
                        {
                            textBox1.BackColor = Color.Red;
                        }
                    }
                }
                else
                {
                    newTarget.Width=0;
                }
            }
            else
            {
                findTargetsOverlay.SetValue(new Bgr(0, 0, 0));
                newTarget.Width=0;
            }
        }
        #endregion
        #region Track Targets
        void trackTargetsThread_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            textBox1.BackColor = Color.DarkGray;
            if (targetingAuto)
            {
                trackingFramesCount++;
                //temporary
                imageBox1.Image=manager.getSMask();
                imageBox2.Image=manager.getFeats();
                imageBox3.Image=manager.getPrev();
                trackTargetsThread.RunWorkerAsync();
            }
            else
            {
                trackingOverlay.SetValue(new Bgr(0, 0, 0));
            }
        }

        void trackTargetsThread_DoWork(object sender, DoWorkEventArgs e)
        {
            manager.NewTarget(newTarget);
            manager.UpdateTargets(video.QueryFrame().Clone().Resize(320, 240, INTER.CV_INTER_LINEAR),
                                  previousFrame.Resize(320, 240, INTER.CV_INTER_LINEAR), findTargetsOverlay);
            trackingOverlay.SetValue(new Bgr(0, 0, 0));
            foreach (var rec in manager.GetTargets())
            {
                trackingOverlay.Draw(ScaleRectangleUp(rec,2), new Bgr(0, 0, 255), 2);
                if (ScaleRectangleUp(rec, 2).X > (frameSize.Width / 2) + 100 || ScaleRectangleUp(rec, 2).X > (frameSize.Width / 2) - 100)
                {
                    if (ScaleRectangleUp(rec, 2).Y > (frameSize.Height / 2) + 100 || ScaleRectangleUp(rec, 2).Y > (frameSize.Height / 2) - 100)
                    {
                        textBox1.BackColor = Color.Red;
                    }
                }
            }
        }
        #endregion
        #region Control Servos
        void controlServosThread_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            servoControler.StopAll(Convert.ToInt16(panTXT.Text), Convert.ToInt16(tiltTXT.Text));
            tmrServoUpdate.Enabled = true;
            
        }
        void controlServosThread_DoWork(object sender, DoWorkEventArgs e)
        {
            if (newTarget != null)
            {
                lock (manager)
                {
                    if (!manager.GetBiggestTarget().IsEmpty)
                    {
                        //Console.WriteLine("serv");
                        ///Console.WriteLine(manager.GetBiggestTarget().Location.ToString());
                        servoControler.MoveToTarget(ScaleRectangleUp(manager.GetBiggestTarget(),2));
                    }
                    else
                    {
                        //Console.WriteLine("no target");
                    }
                }
            }
        }
        #endregion
        #endregion

        #region Menu Items
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void cameraSelectionBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            pauseVideo=true;            
        }
        private void btnStartTargeting_Click(object sender, EventArgs e)
        {
            if (!targetingAuto)
            {
                timer1.Enabled = false;
                findTargetsThread.RunWorkerAsync();
                trackTargetsThread.RunWorkerAsync();
                controlServosThread.RunWorkerAsync();
                targetingAuto=true;
                btnStartTargeting.Text="Stop";
            }
            else
            {
                targetingAuto=false;
                btnStartTargeting.Text="Start targeting";
            }
        }
        #endregion

        private void tmrServoUpdate_Tick(object sender, EventArgs e)
        {
            if (targetingAuto)
            {
                servoControler.StopAll(Convert.ToInt16(panTXT.Text), Convert.ToInt16(tiltTXT.Text));
                controlServosThread.RunWorkerAsync();
                tmrServoUpdate.Enabled=false;
            }
            else
            {
                servoControler.StopAll(Convert.ToInt16(panTXT.Text), Convert.ToInt16(tiltTXT.Text));
            }
        }

        private void btnStartTargeting_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!targetingAuto)
            {
                timer1.Enabled = false;
                if (e.KeyChar=='a')
                {
                    servoControler.Left();
                }
                if (e.KeyChar=='d')
                {
                    servoControler.Right();
                }
                if (e.KeyChar=='w')
                {
                    servoControler.Up();
                }
                if (e.KeyChar=='s')
                {
                    servoControler.Down();
                }
            }
        }

        private void btnStartTargeting_KeyUp(object sender, KeyEventArgs e)
        {
            servoControler.StopAll(Convert.ToInt16(panTXT.Text), Convert.ToInt16(tiltTXT.Text));
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            servoControler.StopAll(Convert.ToInt16(panTXT.Text), Convert.ToInt16(tiltTXT.Text));
        }
    }
}
