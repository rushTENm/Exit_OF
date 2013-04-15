using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace Exit_OF
{
    class HomeEScreen : IScreen
    {
        KeyboardState lastKeyboardState = new KeyboardState();
        KeyboardState currentKeyboardState = new KeyboardState();

        MouseState mouseState;

        BasicEffect basicEffect;

        ParticleComponent particleComponent;

        SpriteAnimation fire;
        int fireStrength = 2000;

        ChaseTarget target;
        ChaseCamera camera;
        bool cameraSpringEnabled = true;

        HomeModelHelper homeModelHelper = new HomeModelHelper();
        GameModel SimpleMap = new GameModel();
        GameModel phone = new GameModel();

        GameModel fireIndicator = new GameModel();
        GameModel fireBrave = new GameModel();
        GameModel fireHP = new GameModel();
        bool IsFireNear = false;

        GameModel extinguisherNomal = new GameModel();
        GameModel extinguisherNear = new GameModel();
        bool IsExtinguisherGet = false;

        GameModel extinguisherUse = new GameModel();

        DrawHelper drawHelper = new DrawHelper();

        PauseScreen pauseScreen = new PauseScreen();

        BoundingBox bbESC = new BoundingBox(new Vector3(92, 20, -129), new Vector3(100, 80, -106));

        List<BoundingBox> boundingBs = new List<BoundingBox>();
        BoundingBox bbKitchen = new BoundingBox(new Vector3(-96, 20, -164), new Vector3(30, 80, -51));
        BoundingBox bbConnection = new BoundingBox(new Vector3(-144, 20, -51), new Vector3(-87, 80, 15));
        BoundingBox bbLivingroom = new BoundingBox(new Vector3(-87, 20, -72), new Vector3(98, 80, 132));
        BoundingBox bbDoor = new BoundingBox(new Vector3(38, 20, -145), new Vector3(98, 80, -81));
        BoundingBox bbLivingroomDoor = new BoundingBox(new Vector3(57, 20, -82), new Vector3(80, 80, -72));

        SoundEffect TickTock;
        bool TickTockNeed = false;
        bool TickTockPlayed = false;
        SoundEffect Cough;
        bool CoughPlayed = false;
        SoundEffect Eat;
        bool EatPlayed = false;

        public HomeEScreen(Exit_OF game, GraphicsDevice device, ContentManager content)
        {
            ParticleInit(game, device, content);
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
        }

        public override void Init(ContentManager content)
        {
            boundingBs.Add(bbConnection);
            boundingBs.Add(bbDoor);
            boundingBs.Add(bbKitchen);
            boundingBs.Add(bbLivingroom);
            boundingBs.Add(bbLivingroomDoor);

            target = new ChaseTarget();
            target.Init(content, 0.0002f, @"HomeRScreen\JustBall");
            target.Position = new Vector3(80, 50, 83);
            target.PositionTemp = new Vector3(80, 50, 83);

            camera = new ChaseCamera();
            UpdateCameraChaseTarget();
            camera.Reset();

            homeModelHelper.Init(content);
            phone.Init(content, 1f, 0f, new Vector3(38, 33, 83), @"HomeEScreen\phone");
            phone.boundingS.Radius = 5f;
            SimpleMap.Init(content, 1f, 0f, Vector3.Zero, @"HomeEScreen\Tutorial");

            fireIndicator.Init(content, 0.5f, 0f, new Vector3(-80, 40, 0), @"HomeRScreen\JustBall");
            fireIndicator.boundingS.Radius = 50;
            fireBrave.Init(content, 0.5f, 0f, new Vector3(-80, 40, 0), @"HomeRScreen\JustBall");
            fireBrave.boundingS.Radius = 40;
            fireHP.Init(content, 0.5f, 0f, new Vector3(-80, 40, 0), @"HomeRScreen\JustBall");
            fireHP.boundingS.Radius = 30;

            extinguisherNomal.Init(content, 0.05f, -45f, new Vector3(28, 20, -70), @"HomeEScreen\extinguisherNomal");
            extinguisherNomal.boundingS.Radius = 15f;
            extinguisherNear.Init(content, 0.5f, 0f, new Vector3(28, 20, -70), @"HomeRScreen\JustBall");

            extinguisherUse.Init(content, 0.02f, -45f, Vector3.Zero, @"HomeEScreen\extinguisherUse");

            drawHelper.Init(content);

            fire = new SpriteAnimation();
            fire.Init(content, @"HomeEScreen\fire", Vector2.Zero, 4, 4, 14, 3, 0.3f);

            TickTock = content.Load<SoundEffect>(@"HomeEScreen\TickTock");
            Cough = content.Load<SoundEffect>(@"HomeEScreen\Cough");
            Eat = content.Load<SoundEffect>(@"HomeEScreen\Eat");

            pauseScreen.Init(content);
        }

        public bool bbContains(List<BoundingBox> boundingBs, BoundingSphere boundingS)
        {
            foreach (var boundingB in boundingBs)
            {
                if (boundingB.Contains(boundingS) == ContainmentType.Contains)
                {
                    return true;
                }
            }
            return false;
        }

        public override void Update(GameTime gameTime)
        {
            pauseScreen.Update(gameTime);
            if (lastKeyboardState.IsKeyUp(Keys.P) && (currentKeyboardState.IsKeyDown(Keys.P)))
            {
                pauseScreen.IsPause = !pauseScreen.IsPause;
            }

            particleComponent.Update(gameTime);
            fire.Update();

            if (drawHelper.m_IsStarted)
            {
                PositionUpdate(gameTime);

                if (bbContains(boundingBs, target.boundingS))
                {
                    target.Position = target.PositionTemp;
                }
            }

            mouseState = Mouse.GetState();

            if (IntersectDistance(extinguisherNomal.boundingS, mouseState, camera.view, camera.projection, new Viewport(0, 0, 1366, 768)) != null &&
                target.boundingS.Intersects(extinguisherNear.boundingS) &&
                drawHelper.Isdialed)
            {
                IsExtinguisherGet = true;
                drawHelper.IsExtinguisherGet = true;
                if (!EatPlayed)
                {
                    Eat.Play();
                }
            }

            if (target.boundingS.Intersects(fireIndicator.boundingS) && IsExtinguisherGet)
            {
                IsFireNear = true;
                extinguisherUse.Position = target.Position - new Vector3(0, 9, 0);
            }
            else
            {
                IsFireNear = false;
            }

            if (target.boundingS.Intersects(fireBrave.boundingS))
            {
                drawHelper.brave--;
                drawHelper.braveDown = true;
                if (!CoughPlayed)
                {
                    Cough.Play();
                    CoughPlayed = true;
                }
            }
            else
            {
                drawHelper.braveDown = false;
            }

            if (target.boundingS.Intersects(fireHP.boundingS))
            {
                drawHelper.HP--;
                drawHelper.HPDown = true;
            }
            else
            {
                drawHelper.HPDown = false;
            }

            if (drawHelper.HP < 0 || drawHelper.brave < 0 || fireStrength > 7000)
            {
                m_ScreenManager.SelectScreen(Mode.ResultBadEScreen);
            }
            if (fireStrength <= 0)
            {
                m_ScreenManager.SelectScreen(Mode.ResultEScreen);
            }

            if (TickTockNeed && !TickTockPlayed)
            {
                TickTock.Play();
                TickTockPlayed = true;
            }

            if (bbESC.Contains(target.boundingS) == ContainmentType.Contains)
            {
                m_ScreenManager.SelectScreen(Mode.ResultEScreen);
            }
        }

        public Ray CalculateRay(MouseState mouseState, Matrix view, Matrix projection, Viewport viewport)
        {
            Vector3 nearPoint = viewport.Unproject(new Vector3(mouseState.X, mouseState.Y, 0.0f), projection, view, Matrix.Identity);
            Vector3 farPoint = viewport.Unproject(new Vector3(mouseState.X, mouseState.Y, 1.0f), projection, view, Matrix.Identity);

            Vector3 direction = farPoint - nearPoint;
            direction.Normalize();

            return new Ray(nearPoint, direction);
        }

        public float? IntersectDistance(BoundingSphere sphere, MouseState mouseState, Matrix view, Matrix projection, Viewport viewport)
        {
            Ray mouseRay = CalculateRay(mouseState, view, projection, viewport);
            return mouseRay.Intersects(sphere);
        }

        private void PositionUpdate(GameTime gameTime)
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

        private void UpdateCameraChaseTarget()
        {
            camera.ChasePosition = target.Position;
            camera.ChaseDirection = target.Direction;
            camera.Up = target.Up;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            target.DrawMeshes(camera);
            homeModelHelper.Draw(camera);
            phone.DrawMeshes(camera);
            // SimpleMap.DrawMeshes(camera);

            if (!IsExtinguisherGet)
            {
                extinguisherNomal.DrawMeshes(camera);
            }

            if (IsFireNear && fireStrength > 0)
            {
                extinguisherUse.DrawMeshes(camera);
                fireStrength -= 8;
            }
            else
            {
                fireStrength++;
            }

            particleComponent.Draw(spriteBatch, basicEffect, camera.projection, camera.view, fireStrength / 2);
            fire.Draw(spriteBatch, basicEffect, camera.projection, camera.View, fireStrength / 2);

            drawHelper.Draw(spriteBatch);

            spriteBatch.Begin();

            if (IntersectDistance(phone.boundingS, mouseState, camera.view, camera.projection, new Viewport(0, 0, 1366, 768)) != null)
            {
                drawHelper.DrawDialer(spriteBatch);
            }

            if (fireStrength > 6040)
            {
                spriteBatch.Draw(drawHelper.fireTooStr, Vector2.Zero, Color.White);
                TickTockNeed = true;
            }
            else if (!drawHelper.Isdialed)
            {
                spriteBatch.Draw(drawHelper.MustCall, Vector2.Zero, Color.White);
            }
            else if (target.boundingS.Intersects(fireBrave.boundingS))
            {
                spriteBatch.Draw(drawHelper.loseBrave, Vector2.Zero, Color.White);
            }
            else if (target.boundingS.Intersects(fireHP.boundingS))
            {
                spriteBatch.Draw(drawHelper.loseHP, Vector2.Zero, Color.White);
            }

            spriteBatch.End();

            pauseScreen.Draw(spriteBatch);
        }
    }
}
