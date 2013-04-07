using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Exit_2R
{
    class LivingRoomScreen : IScreen
    {
        GameModel m_LIvingRoom;
        Ball ball;
        Model ballModel;

        ChaseCamera camera;

        public override void Init(ContentManager content)
        {
            m_LIvingRoom = new GameModel();
            m_LIvingRoom.ContentLoad(content, @"LivingRoomScreen\LivingRoom");

            ball = new Ball();
            ballModel = content.Load<Model>(@"LivingRoomScreen\JustBall");

            camera = new ChaseCamera();

            camera.DesiredPositionOffset = new Vector3(0.0f, 50.0f, 400.0f);
            camera.LookAtOffset = new Vector3(0.0f, 0.0f, 0.0f);

            camera.NearPlaneDistance = 1.0f;
            camera.FarPlaneDistance = 100000.0f;
            camera.AspectRatio = 16/9f;

            UpdateCameraChaseTarget();
        }

        private void UpdateCameraChaseTarget()
        {
            camera.ChasePosition = ball.Position;
            camera.ChaseDirection = ball.Direction;
            camera.Up = ball.Up;
        }

        public override void Update(MouseState mouseState)
        {
            if (mouseState.RightButton == ButtonState.Pressed)
            {
                m_ScreenManager.SelectScreen(Mode.ThemeScreen);
            }
        }

        public override void Update(GameTime gameTime)
        {
            ball.Update(gameTime);
            UpdateCameraChaseTarget();
            camera.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            ball.DrawModel(ballModel, ball.World, camera);

            m_LIvingRoom.DrawMeshes(camera);
        }
    }
}
