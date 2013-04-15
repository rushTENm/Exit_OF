using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Kinect;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Exit_OF
{
    class KinectHelper
    {
        KinectSensor kinect;
        Skeleton[] skeletons = null;
        Skeleton activeSkeleton = null;

        public enum LR { left, front, right }
        public LR shoulderLR = LR.front;

        public enum Standing { standing, walking }
        public Standing standing = Standing.standing;
        int standingCounter = 0;

        Texture2D lineDot;

        public void Init(ContentManager content)
        {
            kinect = KinectSensor.KinectSensors[0];
            kinect.SkeletonStream.Enable();
            kinect.SkeletonFrameReady += new EventHandler<SkeletonFrameReadyEventArgs>(kinect_SkeletonFrameReady);
            kinect.Start();

            lineDot = content.Load<Texture2D>(@"whiteDot");
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

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            if (activeSkeleton != null)
            {
                drawSkeleton(spriteBatch, activeSkeleton, Color.White);
            }

            spriteBatch.End();
        }

        void drawLine(SpriteBatch spriteBatch, Vector2 v1, Vector2 v2, Color color)
        {
            Vector2 diff = v2 - v1;
            Vector2 scale = new Vector2(1.0f, diff.Length() / lineDot.Height);
            float angle = (float)(Math.Atan2(diff.Y, diff.X)) - MathHelper.PiOver2;
            Vector2 origin = new Vector2(0.5f, 0.0f);

            spriteBatch.Draw(lineDot, v1, null, color, angle, origin, scale, SpriteEffects.None, 1.0f);
        }

        void drawBone(SpriteBatch spriteBatch, Joint j1, Joint j2, Color color)
        {
            ColorImagePoint mapped = kinect.CoordinateMapper.MapSkeletonPointToColorPoint(j1.Position, ColorImageFormat.RgbResolution640x480Fps30);
            Vector2 mapToV = new Vector2(mapped.X, mapped.Y) * new Vector2(2.134375f);

            ColorImagePoint mapped2 = kinect.CoordinateMapper.MapSkeletonPointToColorPoint(j2.Position, ColorImageFormat.RgbResolution640x480Fps30);
            Vector2 mapToV2 = new Vector2(mapped2.X, mapped2.Y) * new Vector2(2.134375f);

            drawLine(spriteBatch, mapToV, mapToV2, color);
        }

        void drawSkeleton(SpriteBatch spriteBatch, Skeleton skel, Color col)
        {
            // Spine
            drawBone(spriteBatch, skel.Joints[JointType.Head], skel.Joints[JointType.ShoulderCenter], col);
            drawBone(spriteBatch, skel.Joints[JointType.ShoulderCenter], skel.Joints[JointType.Spine], col);
                     
            // Left lspriteBatch, eg
            drawBone(spriteBatch, skel.Joints[JointType.Spine], skel.Joints[JointType.HipCenter], col);
            drawBone(spriteBatch, skel.Joints[JointType.HipCenter], skel.Joints[JointType.HipLeft], col);
            drawBone(spriteBatch, skel.Joints[JointType.HipLeft], skel.Joints[JointType.KneeLeft], col);
            drawBone(spriteBatch, skel.Joints[JointType.KneeLeft], skel.Joints[JointType.AnkleLeft], col);
            drawBone(spriteBatch, skel.Joints[JointType.AnkleLeft], skel.Joints[JointType.FootLeft], col);
                     
            // Right spriteBatch, leg
            drawBone(spriteBatch, skel.Joints[JointType.HipCenter], skel.Joints[JointType.HipRight], col);
            drawBone(spriteBatch, skel.Joints[JointType.HipRight], skel.Joints[JointType.KneeRight], col);
            drawBone(spriteBatch, skel.Joints[JointType.KneeRight], skel.Joints[JointType.AnkleRight], col);
            drawBone(spriteBatch, skel.Joints[JointType.AnkleRight], skel.Joints[JointType.FootRight], col);
                     
            // Left aspriteBatch, rm
            drawBone(spriteBatch, skel.Joints[JointType.ShoulderCenter], skel.Joints[JointType.ShoulderLeft], col);
            drawBone(spriteBatch, skel.Joints[JointType.ShoulderLeft], skel.Joints[JointType.ElbowLeft], col);
            drawBone(spriteBatch, skel.Joints[JointType.ElbowLeft], skel.Joints[JointType.WristLeft], col);
            drawBone(spriteBatch, skel.Joints[JointType.WristLeft], skel.Joints[JointType.HandLeft], col);
                     
            // Right spriteBatch, arm
            drawBone(spriteBatch, skel.Joints[JointType.ShoulderCenter], skel.Joints[JointType.ShoulderRight], col);
            drawBone(spriteBatch, skel.Joints[JointType.ShoulderRight], skel.Joints[JointType.ElbowRight], col);
            drawBone(spriteBatch, skel.Joints[JointType.ElbowRight], skel.Joints[JointType.WristRight], col);
            drawBone(spriteBatch, skel.Joints[JointType.WristRight], skel.Joints[JointType.HandRight], col);
        }
    }
}
