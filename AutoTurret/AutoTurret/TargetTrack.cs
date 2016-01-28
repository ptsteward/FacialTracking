using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Accord.Imaging;
using Accord.Imaging.Filters;
using Accord.Math;
using AForge;
using AForge.Imaging.Filters;

using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.UI;
using Emgu.CV.CvEnum;
using Emgu.CV.Features2D;
using Emgu.CV.Util;

namespace AutoTurret
{
    class TargetTrack
    {
        #region Variables 
        //New variables for new tracking code

        private IntPoint[] targetKeyPoints;
        private IntPoint[] frameKeyPoints;

        private IntPoint[] targetMacthes;
        private IntPoint[] frameMatches;
        //end
        private bool internalFrameSet=false;
        
        private byte[] status;

        private double qualityLevel = .001;
        private double minDistance = 2;
        private double dX = 0, dY = 0;

        private float[] trackError;

        private Image<Gray,byte> mask = new Image<Gray,byte>(frameSize);
        private Image<Bgr, byte> internalPreviousFrame = new Image<Bgr, byte>(frameSize);

        private MCvTermCriteria criteria = new MCvTermCriteria(.01d);

        private PointF[] previousFeatures;
        private PointF[] currentFeatures;

        private int levels = 4;
        private int blockSize = 2;
        static private int maxFeatures = 100;

        private Size winSize = new Size(5,5);
        static private Size frameSize=new Size(320, 240);

        SURFDetector surfDetector = new SURFDetector(500, false);

        //temporary
        public Image<Gray, byte> sMask;
        public Image<Bgr, byte> feats;
        public Image<Bgr, byte> prevMasked;
        #endregion
        public TargetTrack()
        {

        }
        public void setInternalPreviousFrame(Image<Bgr, byte> prevFrame)
        {
            internalPreviousFrame=prevFrame;
            internalFrameSet=true;
        }
        public Rectangle CalculateNewPosition(Image<Bgr, byte> currentFrame, Image<Bgr, byte> previousFrame, Rectangle prevTarget)
        {
            if (!internalFrameSet)
                internalPreviousFrame=previousFrame;
            if (prevTarget.X+prevTarget.Width>frameSize.Width||prevTarget.Y+prevTarget.Height>frameSize.Height
                ||prevTarget.X<0 || prevTarget.Y<0)
                return new Rectangle(0, 0, 0, 0);
            Image<Bgr, byte> targetBGR=new Image<Bgr, byte>(prevTarget.Size);
            targetBGR=internalPreviousFrame.Copy((Rectangle)prevTarget);
            if (CheckForLikelyTarget(targetBGR))
            {
                //MakePreviousFrameMask(prevTarget);
                if (GenerateKeypointMatches(targetBGR.ToBitmap(), currentFrame.ToBitmap(), prevTarget))
                {
                    //GetPreviousFeatures(targetBGR,prevTarget);
                    //RunOpticalFlow(currentFrame.Convert<Gray,byte>(), internalPreviousFrame.Convert<Gray, byte>());
                    return AdjustRectangle(prevTarget);
                }
                else
                {
                    return new Rectangle(0, 0, 0, 0);
                }
            }
            else
            {
                return new Rectangle(0,0,0,0);
            }
        }
        private bool GenerateKeypointMatches(Bitmap target, Bitmap frame, Rectangle prevTarget)
        {
            HarrisCornersDetector harrisDetector = new HarrisCornersDetector(HarrisCornerMeasure.Harris, 1000f, 1f, 2);
            targetKeyPoints=harrisDetector.ProcessImage(target).ToArray();
            frameKeyPoints=harrisDetector.ProcessImage(frame).ToArray();
            //Console.WriteLine("tr={0} fr={1}", targetKeyPoints.Length, frameKeyPoints.Length);
            if (targetKeyPoints.Length==0||frameKeyPoints.Length==0)
            {
                return false;
            }

            CorrelationMatching matcher=new CorrelationMatching(15);
            IntPoint[][] matches=matcher.Match(target, frame, targetKeyPoints, frameKeyPoints);
            targetMacthes=matches[0];
            frameMatches=matches[1];
            if (targetMacthes.Length<4||frameMatches.Length<4)
            {
                return false;
            }
            RansacHomographyEstimator ransac=new RansacHomographyEstimator(0.1, 0.50);
            MatrixH estiment = new MatrixH();
            try
            {

                estiment = ransac.Estimate(targetMacthes, frameMatches);
            }
            catch
            {
                return false;
            }
            IntPoint[] targetGoodMatch=targetMacthes.Submatrix(ransac.Inliers);
            IntPoint[] frameGoodMatch=frameMatches.Submatrix(ransac.Inliers);
            CalculatePosChange(targetGoodMatch, frameGoodMatch, prevTarget);
            return true;
        }
        private void CalculatePosChange(IntPoint[] targetPts, IntPoint[] framePts, Rectangle prevTarget)
        {
            int i, limit = targetPts.Length;
            for (i=0; i<limit; i++)
            {
                targetPts[i].X+=prevTarget.Left;
                targetPts[i].Y+=prevTarget.Top;
            }
            limit=Math.Min(targetPts.Length, framePts.Length);
            int sumX=0, sumY=0;
            for (i=0; i<limit; i++)
            {
                sumX+=(framePts[i].X-targetPts[i].X);
                sumY+=(framePts[i].Y-targetPts[i].Y);
            }
            dX=sumX/limit;
            dY=sumY/limit;

        }
        private void MakePreviousFrameMask(Rectangle prevTarget)
        {
            mask.SetValue(0);
            mask.Draw(prevTarget, new Gray(255), -1);
        }
        private bool CheckForLikelyTarget(Image<Bgr, byte> targetBGR)
        {
            Image<Ycc, byte> targetYCC=targetBGR.Convert<Ycc, byte>();
            Image<Ycc, Byte> lowerYCC=new Image<Ycc, byte>(targetYCC.Width, targetYCC.Height, new Ycc(30, 131, 80));
            Image<Ycc, Byte> upperYCC=new Image<Ycc, byte>(targetYCC.Width, targetYCC.Height, new Ycc(220, 185, 135));
            Image<Gray, byte> result=targetYCC.InRange(lowerYCC, upperYCC);
            sMask=result;
            double minForTarget=50;
            if (result.GetAverage().Intensity>minForTarget)
                return true;
            else
                return false;
            #region old skin detection
            /* AdaptiveSkinDetector likelyTargetDetector=new AdaptiveSkinDetector(1, AdaptiveSkinDetector.MorphingMethod.ERODE_ERODE);
            double minForTarget=35;
            targetBGR.i
            //targetBGR._GammaCorrect(.75);
            tarBgr=targetBGR.Clone();
            likelyTargetDetector.Process(targetBGR, skinMask);
            sMask=skinMask.Clone();
            skinMask._ThresholdBinary(new Gray(1), new Gray(255));
            tMask=skinMask.Clone();
            //Console.WriteLine(skinMask.GetAverage().Intensity);
            if (skinMask.GetAverage().Intensity<minForTarget)
                return false;
            else
                return true; */
            #endregion
        }
        private void GetPreviousFeatures(Image<Bgr, byte> previousFrame, Rectangle prevTarget)
        {
            feats=previousFrame;
            previousFeatures=previousFrame.Convert<Gray,byte>().GoodFeaturesToTrack(maxFeatures, qualityLevel, minDistance, blockSize)[0];
            
            int temp, limit=previousFeatures.Length;
            for (temp=0; temp<limit; temp++)
            {
                feats.Draw(new CircleF(previousFeatures[temp], 1), new Bgr(Color.Blue), 0);
                previousFeatures[temp].X+=prevTarget.X;
                previousFeatures[temp].Y+=prevTarget.Y;
            }
            
            InitializeArrays(previousFeatures.Length);
        }
        private void GetAllKeyPointsAndMatch(Image<Gray, byte> prevFrame, Image<Gray, byte> currFrame)
        {
            int k = 2;
            double uniquenessThreshold = .8;
            VectorOfKeyPoint targetKeyPoints = new VectorOfKeyPoint();
            VectorOfKeyPoint currentFrameKeyPoints = new VectorOfKeyPoint();
            Matrix<float> targetDescriptors = surfDetector.DetectAndCompute(prevFrame, null, targetKeyPoints);
            Matrix<float> currentFrameDescriptors = surfDetector.DetectAndCompute(currFrame, null, currentFrameKeyPoints);
            if (targetDescriptors != null && currentFrameDescriptors != null)
            {
                BruteForceMatcher<float> keyPointMatcher = new BruteForceMatcher<float>(DistanceType.L2);
                keyPointMatcher.Add(targetDescriptors);

                Matrix<int> indices = new Matrix<int>(currentFrameDescriptors.Rows, k);
                using (Matrix<float> dist = new Matrix<float>(currentFrameDescriptors.Rows, k))
                {
                    keyPointMatcher.KnnMatch(currentFrameDescriptors, indices, dist, k, null);
                    Matrix<byte> matchMask = new Matrix<byte>(dist.Rows, 1);
                    matchMask.SetValue(255);
                    Features2DToolbox.VoteForUniqueness(dist, uniquenessThreshold, matchMask);
                    prevMasked = Features2DToolbox.DrawMatches(prevFrame, targetKeyPoints, currFrame, currentFrameKeyPoints, indices, new Bgr(255, 0, 0), new Bgr(0, 255, 0), matchMask, Features2DToolbox.KeypointDrawType.DEFAULT);
                    CalculateAvgDelta(targetDescriptors, currentFrameDescriptors, matchMask);
                }
            }
        }
        private void InitializeArrays(int numOfFeatures)
        {
            status = new byte[numOfFeatures];
            trackError = new float[numOfFeatures];
            previousFeatures = new PointF[numOfFeatures];
            currentFeatures = new PointF[numOfFeatures];
        }
        private void RunOpticalFlow(Image<Gray, byte> currentFrame, Image<Gray, byte> previousFrame)
        {
            OpticalFlow.PyrLK(previousFrame, currentFrame, previousFeatures, winSize, levels, criteria, out currentFeatures, out status, out trackError);
        }
        private void CalculateAvgDelta(Matrix<float> targetDescriptors, Matrix<float> currentFrameDescriptors, Matrix<byte> matchMask)
        {
            #region Old Average Delta
            /*
            int numOfFeatures = currentFeatures.Length;
            int featuresTracked=0;
            dX = 0; dY = 0;
            int temp, limit = numOfFeatures;
            for (temp = 0; temp < limit; temp++)
            {
                if (status[temp]==0)
                    continue;
                if (dX<currentFeatures[temp].X)
                    dX=currentFeatures[temp].X;
                if (dY<currentFeatures[temp].Y)
                    dY=currentFeatures[temp].Y;
                //dX+=currentFeatures[temp].X;
                //dY+=currentFeatures[temp].Y;
                featuresTracked++;
            }
            if (numOfFeatures==0||featuresTracked==0)
            {
                Console.WriteLine("none");
                dX = 0;
                dY = 0;
            }
            else
            {
                Console.WriteLine(featuresTracked + ","+dX+","+dY);
                //dX /= featuresTracked;
                //dY/=featuresTracked;
            }*/
            #endregion
        }
        private Rectangle AdjustRectangle(Rectangle prevTarget)
        {
            prevTarget.X += (int)dX;
            prevTarget.Y += (int)dY;
            return prevTarget;
        }
    }
}
