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
        MouseState mouseState;

        KeyboardState lastKeyboardState = new KeyboardState();
        KeyboardState currentKeyboardState = new KeyboardState();

        ParticleComponent particleComponent;
        BasicEffect basicEffect;
        SpriteAnimation fire;

        ChaseTarget target;
        ChaseCamera camera;
        bool cameraSpringEnabled = true;

        // HomeModelHelper homeModelHelper = new HomeModelHelper();
        GameModel phone = new GameModel();
        GameModel tutorial = new GameModel();

        DrawHelper drawHelper = new DrawHelper();

        PauseScreen pauseScreen = new PauseScreen();

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

            fire = new SpriteAnimation();
            fire.Init(content, "fire", Vector2.Zero, 4, 4, 14, 3, 2f);
        }

        public override void Init(ContentManager content)
        {
            target = new ChaseTarget();
            target.Init(content, 0.0002f, @"HomeRScreen\JustBall");
            target.Position = new Vector3(0, 50, 0);

            camera = new ChaseCamera();

            UpdateCameraChaseTarget();
            camera.Reset();

            // homeModelHelper.Init(content);
            phone.Init(content, 1f, new Vector3(0,50,0), @"HomeEScreen\phone");
            tutorial.Init(content, 1f, Vector3.Zero, @"HomeEScreen\Tutorial");

            drawHelper.Init(content);
            pauseScreen.Init(content);
        }

        public override void Update(GameTime gameTime)
        {
            PositionUpdate(gameTime);

            pauseScreen.Update(gameTime);
            if (lastKeyboardState.IsKeyUp(Keys.P) && (currentKeyboardState.IsKeyDown(Keys.P)))
            {
                pauseScreen.IsPause = !pauseScreen.IsPause;
            }
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

            // homeModelHelper.Draw(camera);
            phone.DrawMeshes(camera);
            tutorial.DrawMeshes(camera);

            drawHelper.Draw(spriteBatch);

            if (pauseScreen.IsPause)
            {
                pauseScreen.Draw(spriteBatch);
            }
        }
    }
}
