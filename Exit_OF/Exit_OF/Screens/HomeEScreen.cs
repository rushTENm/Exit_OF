using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Exit_OF
{
    class HomeEScreen : IScreen
    {
        KeyboardState lastKeyboardState = new KeyboardState();
        KeyboardState currentKeyboardState = new KeyboardState();

        ParticleComponent particleComponent;
        BasicEffect basicEffect;
        SpriteAnimation fire;
        int fireStr = 1000;

        ChaseTarget target;
        ChaseCamera camera;
        bool cameraSpringEnabled = true;

        HomeModelHelper homeModelHelper = new HomeModelHelper();
        GameModel tutorial = new GameModel();
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
        Texture2D fireBraveTex;
        Texture2D fireHPTex;
        Texture2D MushCall;

        PauseScreen pauseScreen = new PauseScreen();

        BoundingBox bbKitchen = new BoundingBox(new Vector3(-96, 20, -164), new Vector3(30, 80, -51));
        BoundingBox bbConnection = new BoundingBox(new Vector3(-144, 20, -51), new Vector3(-87, 80, 15));
        BoundingBox bbLivingromm = new BoundingBox(new Vector3(-87, 20, -72), new Vector3(98, 80, 132));

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
            target = new ChaseTarget();
            target.Init(content, 0.0002f, @"HomeRScreen\JustBall");
            target.Position = new Vector3(80,50,83);

            camera = new ChaseCamera();

            UpdateCameraChaseTarget();
            camera.Reset();

            homeModelHelper.Init(content);
            phone.Init(content, 1f, 0f, new Vector3(38, 33, 83), @"HomeEScreen\phone");
            phone.boundingS.Radius = 5f;
            tutorial.Init(content, 1f, 0f, Vector3.Zero, @"HomeEScreen\Tutorial");
            extinguisherNomal.Init(content, 0.05f,-45f, new Vector3(28, 20, -70), @"HomeEScreen\extinguisherNomal");
            extinguisherNomal.boundingS.Radius = 15f;
            extinguisherNear.Init(content, 0.5f, 0f, new Vector3(28, 20, -70), @"HomeRScreen\JustBall");
            fireIndicator.boundingS.Radius = 70;
            fireIndicator.Init(content, 0.5f, 0f, new Vector3(-80, 40, 0), @"HomeRScreen\JustBall");
            fireIndicator.boundingS.Radius = 50;
            fireBrave.Init(content, 0.5f, 0f, new Vector3(-80, 40, 0), @"HomeRScreen\JustBall");
            fireBrave.boundingS.Radius = 40;
            fireHP.Init(content, 0.5f, 0f, new Vector3(-80, 40, 0), @"HomeRScreen\JustBall");
            fireHP.boundingS.Radius = 30;
            extinguisherUse.Init(content, 0.02f,-45f, Vector3.Zero, @"HomeEScreen\extinguisherUse");

            drawHelper.Init(content);
            pauseScreen.Init(content);

            fire = new SpriteAnimation();
            fire.Init(content, "fire", Vector2.Zero, 4, 4, 14, 3, 0.3f);

            fireBraveTex = content.Load<Texture2D>(@"IsFireBrave");
            fireHPTex = content.Load<Texture2D>(@"IsFireHP");
            MushCall = content.Load<Texture2D>(@"CallTex");
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
            }

            Vector2 mouseLocation = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);

            if (IntersectDistance(extinguisherNomal.boundingS, mouseLocation, camera.view, camera.projection, new Viewport(0, 0, 1366, 768)) != null &&
                target.boundingS.Intersects(extinguisherNear.boundingS) &&
                drawHelper.Isdialer)
            {
                IsExtinguisherGet = true;
                drawHelper.IsExtinguisherGet = true;
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

            if (target.boundingS.Intersects(fireBrave.boundingS) && IsExtinguisherGet)
            {
                drawHelper.brave--;
            }

            if (target.boundingS.Intersects(fireHP.boundingS) && IsExtinguisherGet)
            {
                drawHelper.HP--;
            }

            if (drawHelper.HP < 0 || drawHelper.brave < 0 || fireStr >3500)
            {
                m_ScreenManager.SelectScreen(Mode.ResultBadEScreen);
            }
            if (fireStr<=0)
            {
                m_ScreenManager.SelectScreen(Mode.ResultEScreen);
            }
        }

        public Ray CalculateRay(Vector2 mouseLocation, Matrix view,
                Matrix projection, Viewport viewport)
        {
            Vector3 nearPoint = viewport.Unproject(new Vector3(mouseLocation.X,
                    mouseLocation.Y, 0.0f),
                    projection,
                    view,
                    Matrix.Identity);

            Vector3 farPoint = viewport.Unproject(new Vector3(mouseLocation.X,
                    mouseLocation.Y, 1.0f),
                    projection,
                    view,
                    Matrix.Identity);

            Vector3 direction = farPoint - nearPoint;
            direction.Normalize();

            return new Ray(nearPoint, direction);
        }

        public float? IntersectDistance(BoundingSphere sphere, Vector2 mouseLocation,
            Matrix view, Matrix projection, Viewport viewport)
        {
            Ray mouseRay = CalculateRay(mouseLocation, view, projection, viewport);
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
            tutorial.DrawMeshes(camera);
            if (!IsExtinguisherGet)
            {
                extinguisherNomal.DrawMeshes(camera);
            }
            if (IsFireNear && fireStr >0)
            {
                extinguisherUse.DrawMeshes(camera);
                fireStr -= 3;
            }
            else
            {
               fireStr++;
            }
            particleComponent.Draw(spriteBatch, basicEffect, camera.projection, camera.view, fireStr);
            fire.Draw(spriteBatch, basicEffect, camera.projection, camera.View, fireStr);

            drawHelper.Draw(spriteBatch);
            Vector2 mouseLocation = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);

            if (IntersectDistance(phone.boundingS, mouseLocation, camera.view, camera.projection, new Viewport(0, 0, 1366, 768)) != null)
            {
                spriteBatch.Begin();
                drawHelper.DrawDialer(spriteBatch);
                spriteBatch.End();
            }

            spriteBatch.Begin();
            if (target.boundingS.Intersects(fireBrave.boundingS) && IsExtinguisherGet)
            {
                spriteBatch.Draw(fireBraveTex, Vector2.Zero, Color.White);
            }

            if (target.boundingS.Intersects(fireHP.boundingS) && IsExtinguisherGet)
            {
                spriteBatch.Draw(fireHPTex, Vector2.Zero, Color.White);
            }
            if (!drawHelper.Isdialer)
            {
                spriteBatch.Draw(MushCall, Vector2.Zero, Color.White);
            }
            spriteBatch.End();
            
            if (pauseScreen.IsPause)
            {
                pauseScreen.Draw(spriteBatch);
            }
        }
    }
}
