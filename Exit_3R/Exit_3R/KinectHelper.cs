using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Kinect;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Exit_3R
{
    class KinectHelper
    {
        KinectSensor kinect;
        Skeleton[] skeletons = null;

        public enum LR { left, front, right }
        public LR shoulderLR = LR.front;

        public enum Standing { standing, walking }
        public Standing standing = Standing.standing;
        int standingCounter = 0;

        public void Init(ContentManager content)
        {
            kinect = KinectSensor.KinectSensors[0];
            kinect.SkeletonStream.Enable();
            kinect.SkeletonFrameReady += new EventHandler<SkeletonFrameReadyEventArgs>(kinect_SkeletonFrameReady);
            kinect.Start();
        }

        void kinect_SkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)
        {
            using (SkeletonFrame frame = e.OpenSkeletonFrame())
            {
                if (frame != null)
                {
                    skeletons = new Skeleton[frame.SkeletonArrayLength];
                    frame.CopySkeletonDataTo(skeletons);
                }
            }
        }

        public void Update()
        {
            if (skeletons != null)
            {
                foreach (Skeleton s in skeletons)
                {
                    if (s.TrackingState == SkeletonTrackingState.Tracked)
                    {
                        CheckLR(s);

                        CheckStanding(s);
                    }
                }
            }
        }

        private void CheckLR(Skeleton s)
        {
            float shoulderRawLR = s.Joints[JointType.ShoulderRight].Position.Z - s.Joints[JointType.ShoulderLeft].Position.Z;

            if (shoulderRawLR >= 0.15)
            {
                shoulderLR = LR.right;
            }
            else if (shoulderRawLR <= -0.15)
            {
                shoulderLR = LR.left;
            }
            else
            {
                shoulderLR = LR.front;
            }
        }

        private void CheckStanding(Skeleton s)
        {
            float standingRaw = Math.Abs(s.Joints[JointType.KneeLeft].Position.Y - s.Joints[JointType.KneeRight].Position.Y);
            standingCounter++;

            if (standingCounter > 40)
            {
                if (standingRaw > 0.1)
                {
                    standing = Standing.walking;
                    standingCounter = 0;
                }
                else
                {
                    standing = Standing.standing;
                }
            }
        }
    }
}