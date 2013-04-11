using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace Exit_OF
{
    class HomeRScreen : IScreen
    {
        MouseState mouseState;

        KeyboardState lastKeyboardState = new KeyboardState();
        KeyboardState currentKeyboardState = new KeyboardState();

        DrawHelper drawHelper = new DrawHelper();
        Texture2D pause;

        Vector3 cameraPosition = new Vector3(0, 50, 50);
        Vector3 cameraFront = new Vector3(0, 0, -1);

        BasicEffect basicEffect;

        ParticleComponent particleComponent;
        SpriteAnimation fire;

        ChaseTarget target;
        ChaseCamera camera;

        GameModel smallHome;
        GameModel extinguisher;
        GameModel extinguisherUse;
        GameModel fireBall;
        GameModel phone;

        bool IsExtinguisherGet = false;

        bool IsFireNear = false;
        int fireCounter = 0;

        bool cameraSpringEnabled = true;

        public HomeRScreen(Exit_OF game, GraphicsDevice device, ContentManager content)
        {
            ParticleInit(game, device, content);
        }

        private void Reset()
        {
            target.Position = Vector3.Zero;
            drawHelper.m_IsStarted = false;
            IsExtinguisherGet = false;
            IsFireNear = false;
            drawHelper.m_GoCounter = 0;
            drawHelper.dialerCounter = 0;
            fireCounter = 0;
        }

        public override void Init(ContentManager content)
        {
            drawHelper.Init(content);

            pause = content.Load<Texture2D>(@"PauseScreen\pauseBackground");

            target = new ChaseTarget();
            target.Init(content, 0.02f, @"HomeRScreen\JustBall");
            target.Position = new Vector3(0, 350, 0);

            smallHome = new GameModel();
            smallHome.Init(content, 10f, new Vector3(-500, -50, 0), @"HomeRScreen\homeSmall");

            extinguisher = new GameModel();
            extinguisher.Init(content, 0.5f, new Vector3(-1000, 0, 250), @"HomeRScreen\extinguisherNomal");

            extinguisherUse = new GameModel();
            extinguisherUse.Init(content, 0.5f, target.Position, @"HomeRScreen\extinguisherUse");

            fireBall = new GameModel();
            fireBall.Init(content, 1f, new Vector3(-1050, 150, 1100), @"HomeRScreen\JustBall");

            phone = new GameModel();
            phone.Init(content, 0.5f, new Vector3(-800, 300, -100), @"HomeRScreen\JustBall");

            camera = new ChaseCamera();

            UpdateCameraChaseTarget();
            camera.Reset();
        }

        public void ParticleInit(Exit_OF exit_OF, GraphicsDevice graphicsDevice, ContentManager content)
        {

            particleComponent = new ParticleComponent(exit_OF);

            basicEffect = new BasicEffect(graphicsDevice)
            {
                TextureEnabled = true,
                VertexColorEnabled = true,
            };

            particleComponent.LoadContent(content);

            fire = new SpriteAnimation();
            fire.Init(content, "fire", Vector2.Zero, 4, 4, 14, 3, 2f);
        }

        public override void Update(GameTime gameTime)
        {
            mouseState = Mouse.GetState();

            if (mouseState.RightButton == ButtonState.Pressed)
            {
                // m_ScreenManager.SelectScreen(Mode.PauseScreen);
            }

            CheckExtingGet();

            CheckFireNear();

            CheckEnd();

            CheckDialer();

            PositionUpdate(gameTime);

            particleComponent.Update(gameTime);

            fire.Update();
        }

        private void CheckExtingGet()
        {
            if (extinguisher.boundingS.Intersects(target.boundingS) && drawHelper.Isdialer)
            {
                IsExtinguisherGet = true;
            }
        }

        private void CheckFireNear()
        {
            if (fireBall.boundingS.Contains(target.boundingS) == ContainmentType.Intersects && IsExtinguisherGet)
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
                m_ScreenManager.SelectScreen(Mode.ResultEScreen);
            }
        }

        private void CheckDialer()
        {
            if (phone.boundingS.Intersects(target.boundingS))
            {
                drawHelper.Isdialer = false;
            }
        }

        private void PositionUpdate(GameTime gameTime)
        {
            if (drawHelper.m_IsStarted)
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

            if (IsFireNear)
            {
                extinguisherUse.DrawMeshes(camera);
            }

            particleComponent.Draw(spriteBatch, basicEffect, camera.projection, camera.view);

            if (fireCounter < 90)
            {
                fire.Draw(spriteBatch, basicEffect, camera, cameraFront);
            }

            drawHelper.Draw(spriteBatch);

            if (mouseState.RightButton == ButtonState.Pressed)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(pause, Vector2.Zero, Color.White);
                spriteBatch.End();
            }
        }
    }
}