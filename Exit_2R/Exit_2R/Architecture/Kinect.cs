using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Kinect;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Exit_2R
{
    class Kinect
    {
        KinectSensor m_Kinect;
        public Skeleton[] m_Skeletons = null;

        Texture2D m_LineDot;
        Texture2D m_Hand_l;

        Vector2 m_mouseState = Vector2.Zero;

        public void Init(ContentManager content)
        {
            m_Kinect = KinectSensor.KinectSensors[0];
            m_Kinect.SkeletonStream.Enable();
            m_Kinect.SkeletonFrameReady += new EventHandler<SkeletonFrameReadyEventArgs>(kinect_SkeletonFrameReady);
            m_Kinect.Start();

            m_LineDot = content.Load<Texture2D>("lineDot");
            m_Hand_l = content.Load<Texture2D>("hand_l");
        }

        void kinect_SkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)
        {
            using (SkeletonFrame frame = e.OpenSkeletonFrame())
            {
                if (frame != null)
                {
                    m_Skeletons = new Skeleton[frame.SkeletonArrayLength];
                    frame.CopySkeletonDataTo(m_Skeletons);
                }
            }
        }

        public Vector2 Update()
        {
            if (m_Skeletons != null)
            {
                foreach (Skeleton s in m_Skeletons)
                {
                    if ((s.TrackingState == SkeletonTrackingState.Tracked))
                    {
                        ColorImagePoint mapped = m_Kinect.CoordinateMapper.MapSkeletonPointToColorPoint(s.Joints[JointType.HandLeft].Position, ColorImageFormat.RgbResolution640x480Fps30);
                        m_mouseState.X = mapped.X * 1366 / 640f;
                        m_mouseState.Y = mapped.Y * 1366 / 640f;
                    }
                }
            }
            return m_mouseState;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (m_Skeletons != null)
            {
                foreach (Skeleton s in m_Skeletons)
                {
                    if ((s.TrackingState == SkeletonTrackingState.Tracked))
                    {
                        DrawCursor(s, spriteBatch);
                        DrawHUDSkeleton(s, spriteBatch, Color.HotPink);
                    }
                }
            }
        }

        private void DrawCursor(Skeleton s, SpriteBatch spriteBatch)
        {
            ColorImagePoint mapped = m_Kinect.CoordinateMapper.MapSkeletonPointToColorPoint(s.Joints[JointType.HandLeft].Position, ColorImageFormat.RgbResolution640x480Fps30);
            Vector2 mapToV = new Vector2(mapped.X, mapped.Y) * new Vector2(1366/640f);

            spriteBatch.Draw(m_Hand_l, mapToV, null, Color.White, 0f, new Vector2(m_Hand_l.Width / 2, m_Hand_l.Height / 2), 0.4f, SpriteEffects.None, 0);
        }

        private void DrawHUDSkeleton(Skeleton skeleton, SpriteBatch spriteBatch, Color color)
        {
            // Spine
            drawBone(skeleton.Joints[JointType.Head], skeleton.Joints[JointType.ShoulderCenter], spriteBatch, color);
            drawBone(skeleton.Joints[JointType.ShoulderCenter], skeleton.Joints[JointType.Spine], spriteBatch, color);

            // Left arm
            drawBone(skeleton.Joints[JointType.ShoulderCenter], skeleton.Joints[JointType.ShoulderLeft], spriteBatch, color);
            drawBone(skeleton.Joints[JointType.ShoulderLeft], skeleton.Joints[JointType.ElbowLeft], spriteBatch, color);
            drawBone(skeleton.Joints[JointType.ElbowLeft], skeleton.Joints[JointType.WristLeft], spriteBatch, color);
            drawBone(skeleton.Joints[JointType.WristLeft], skeleton.Joints[JointType.HandLeft], spriteBatch, color);

            // Right arm
            drawBone(skeleton.Joints[JointType.ShoulderCenter], skeleton.Joints[JointType.ShoulderRight], spriteBatch, color);
            drawBone(skeleton.Joints[JointType.ShoulderRight], skeleton.Joints[JointType.ElbowRight], spriteBatch, color);
            drawBone(skeleton.Joints[JointType.ElbowRight], skeleton.Joints[JointType.WristRight], spriteBatch, color);
            drawBone(skeleton.Joints[JointType.WristRight], skeleton.Joints[JointType.HandRight], spriteBatch, color);
        }

        void drawBone(Joint j1, Joint j2, SpriteBatch spriteBatch, Color color)
        {
            ColorImagePoint mapped = m_Kinect.CoordinateMapper.MapSkeletonPointToColorPoint(j1.Position, ColorImageFormat.RgbResolution640x480Fps30);
            Vector2 mapToV = new Vector2(mapped.X, mapped.Y) * new Vector2(1366 / 640);

            ColorImagePoint mapped2 = m_Kinect.CoordinateMapper.MapSkeletonPointToColorPoint(j2.Position, ColorImageFormat.RgbResolution640x480Fps30);
            Vector2 mapToV2 = new Vector2(mapped2.X, mapped2.Y) * new Vector2(1366 / 640);

            drawLine(mapToV, mapToV2, spriteBatch, color);
        }

        void drawLine(Vector2 v1, Vector2 v2, SpriteBatch spriteBatch, Color color)
        {
            Vector2 diff = v2 - v1;
            Vector2 scale = new Vector2(1.0f, diff.Length() / m_LineDot.Height);
            float angle = (float)(Math.Atan2(diff.Y, diff.X)) - MathHelper.PiOver2;
            Vector2 origin = new Vector2(0.5f, 0.0f);

            spriteBatch.Draw(m_LineDot, v1, null, color, angle, origin, scale, SpriteEffects.None, 1.0f);
        }
    }
}
