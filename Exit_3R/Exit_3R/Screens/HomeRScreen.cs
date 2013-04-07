using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Exit_3R
{
    class HomeRScreen : IScreen
    {
        Texture2D m_Gogo;
        Texture2D m_Go1;
        Texture2D m_Go2;
        Texture2D m_Go3;

        bool m_IsStarted = false;
        int m_GoCounter = 0;

        Vector2 m_GoPosition = new Vector2(433, 234);

        Texture2D dialer;
        Texture2D dialer9;
        Texture2D dialer91;
        Texture2D dialer911;
        Texture2D dialerCall;
        Texture2D dialerEnd;

        bool Isdialer= true;
        int dialerCounter = 0;

        KeyboardState lastKeyboardState = new KeyboardState();
        KeyboardState currentKeyboardState = new KeyboardState();

        CameraTarget target;
        ChaseCamera camera;

        GameModel smallHome;
        GameModel extinguisher;
        GameModel extinguisherUse;
        GameModel fire;
        GameModel phone;

        bool IsExtinguisherGet = false;

        bool IsFireNear = false;
        int fireCounter = 0;

        bool cameraSpringEnabled = true;

        private void Reset()
        {
            target.Position = Vector3.Zero;
            m_IsStarted = false;
            IsExtinguisherGet = false;
            IsFireNear = false;
            m_GoCounter = 0;
            dialerCounter = 0;
            fireCounter = 0;
        }
        
        public override void Init(ContentManager content)
        {
            m_Gogo = content.Load<Texture2D>(@"HomeRScreen\gogo");
            m_Go1 = content.Load<Texture2D>(@"HomeRScreen\go1");
            m_Go2 = content.Load<Texture2D>(@"HomeRScreen\go2");
            m_Go3 = content.Load<Texture2D>(@"HomeRScreen\go3");

            dialer = content.Load<Texture2D>(@"HomeRScreen\dialer");
            dialer9 = content.Load<Texture2D>(@"HomeRScreen\dialer9");
            dialer91 = content.Load<Texture2D>(@"HomeRScreen\dialer91");
            dialer911 = content.Load<Texture2D>(@"HomeRScreen\dialer911");
            dialerCall = content.Load<Texture2D>(@"HomeRScreen\dialerCall");
            dialerEnd = content.Load<Texture2D>(@"HomeRScreen\dialerEnd");

            target = new CameraTarget();
            target.Init(content, 0.02f, @"HomeRScreen\JustBall");
            target.Position = new Vector3(0, 350, 0);

            smallHome = new GameModel();
            smallHome.Init(content, 10f, new Vector3(-500, -50, 0), @"HomeRScreen\homeSmall");

            extinguisher = new GameModel();
            extinguisher.Init(content, 0.5f, new Vector3(-1000, 0, 250), @"HomeRScreen\extinguisherNomal");

            extinguisherUse = new GameModel();
            extinguisherUse.Init(content, 0.5f, target.Position, @"HomeRScreen\extinguisherUse");

            fire = new GameModel();
            fire.Init(content, 1f, new Vector3(-1050, 150, 1100), @"HomeRScreen\JustBall");

            phone = new GameModel();
            phone.Init(content, 0.5f, new Vector3(-800, 300, -100), @"HomeRScreen\JustBall");

            camera = new ChaseCamera();

            UpdateCameraChaseTarget();
            camera.Reset();
        }

        public override void Update(MouseState mouseState)
        {
            if (mouseState.RightButton == ButtonState.Pressed)
            {
                m_ScreenManager.SelectScreen(Mode.StageRScreen);
            }
        }

        public override void Update(GameTime gameTime)
        {
            CheckExtingGet();

            CheckFireNear();

            CheckEnd();

            CheckDialer();

            PositionUpdate(gameTime);
        }

        private void CheckExtingGet()
        {
            if (extinguisher.boundingS.Intersects(target.boundingS) && Isdialer)
            {
                IsExtinguisherGet = true;
            }
        }

        private void CheckFireNear()
        {
            if (fire.boundingS.Contains(target.boundingS) == ContainmentType.Intersects && IsExtinguisherGet)
            {
                IsFireNear = true;
                fireCounter++;
                extinguisherUse.Position = new Vector3(target.Position.X, target.Position.Y - 300, target.Position.Z);
            }
            else
            {
                IsFireNear = false;
            }
        }

        private void CheckEnd()
        {
            if (fireCounter > 120)
            {
                Reset();
                m_ScreenManager.SelectScreen(Mode.ResoultRScreen);
            }
        }

        private void CheckDialer()
        {
            if (phone.boundingS.Intersects(target.boundingS))
            {
                Isdialer = false;
            }
        }

        private void PositionUpdate(GameTime gameTime)
        {
            if (m_IsStarted)
            {
                // TODO: Add your update logic here
                lastKeyboardState = currentKeyboardState;

                currentKeyboardState = Keyboard.GetState();

                // Pressing the A button or key toggles the spring behavior on and off
                if (lastKeyboardState.IsKeyUp(Keys.A) && (currentKeyboardState.IsKeyDown(Keys.A)))
                {
                    cameraSpringEnabled = !cameraSpringEnabled;
                }

                // Reset the ship on R key or right thumb stick clicked
                if (currentKeyboardState.IsKeyDown(Keys.R))
                {
                    target.Reset();
                    camera.Reset();
                }

                // Update the ship
                target.Update(gameTime);

                // Update the camera to chase the new target
                UpdateCameraChaseTarget();

                // The chase camera's update behavior is the springs, but we can
                // use the Reset method to have a locked, spring-less camera
                if (cameraSpringEnabled)
                    camera.Update(gameTime);
                else
                    camera.Reset();
            }
        }

        private void UpdateCameraChaseTarget()
        {
            camera.ChasePosition = target.Position;
            camera.ChaseDirection = target.Direction;
            camera.Up = target.Up;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            target.DrawMeshes(camera);

            smallHome.DrawMeshes(camera);

            phone.DrawMeshes(camera);

            if (!IsExtinguisherGet)
            {
                extinguisher.DrawMeshes(camera);
            }

            if (fireCounter < 90)
            {
                fire.DrawMeshes(camera);
            }

            if (IsFireNear)
            {
                extinguisherUse.DrawMeshes(camera);
            }

            spriteBatch.Begin();

            DrawGo(spriteBatch);
            DrawDialer(spriteBatch);

            spriteBatch.End();
        }

        private void DrawGo(SpriteBatch spriteBatch)
        {
            if (m_IsStarted == false)
            {
                if (m_GoCounter > 240)
                {
                    m_IsStarted = true;
                }
                else if (m_GoCounter > 180)
                {
                    spriteBatch.Draw(m_Gogo, m_GoPosition, Color.White);
                }
                else if (m_GoCounter > 120)
                {
                    spriteBatch.Draw(m_Go1, m_GoPosition, Color.White);
                }
                else if (m_GoCounter > 60)
                {
                    spriteBatch.Draw(m_Go2, m_GoPosition, Color.White);
                }
                else
                {
                    spriteBatch.Draw(m_Go3, m_GoPosition, Color.White);
                }

                m_GoCounter++;
            }
        }

        private void DrawDialer(SpriteBatch spriteBatch)
        {
            if (Isdialer == false)
            {
                if (dialerCounter > 360)
                {
                    Isdialer = true;
                }
                else if (dialerCounter > 300)
                {
                    spriteBatch.Draw(dialerEnd, new Vector2(0, 0), null, Color.White, 0f, Vector2.Zero, 768 / 1280f, SpriteEffects.None, 0);
                }
                else if (dialerCounter > 240)
                {
                    spriteBatch.Draw(dialerCall, new Vector2(0, 0), null, Color.White, 0f, Vector2.Zero, 768 / 1280f, SpriteEffects.None, 0);
                }
                else if (dialerCounter > 180)
                {
                    spriteBatch.Draw(dialer911, new Vector2(0, 0), null, Color.White, 0f, Vector2.Zero, 768 / 1280f, SpriteEffects.None, 0);

                }
                else if (dialerCounter > 120)
                {
                    spriteBatch.Draw(dialer91, new Vector2(0, 0), null, Color.White, 0f, Vector2.Zero, 768 / 1280f, SpriteEffects.None, 0);
                }
                else if (dialerCounter > 60)
                {
                    spriteBatch.Draw(dialer9, new Vector2(0, 0), null, Color.White, 0f, Vector2.Zero, 768 / 1280f, SpriteEffects.None, 0);
                }
                else
                {
                    spriteBatch.Draw(dialer, new Vector2(0, 0), null, Color.White, 0f, Vector2.Zero, 768 / 1280f, SpriteEffects.None, 0);
                }
                dialerCounter++;
            }
        }
    }
}