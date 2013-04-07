using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Kinect;

namespace Exit_2R
{
    public class Exit_2R : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        ScreenManager m_ScreenManager;

        Kinect m_Kinect;

        public Exit_2R()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1366;
            graphics.PreferredBackBufferHeight = 768;
            Content.RootDirectory = "Content";

            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            m_ScreenManager = new ScreenManager();
            m_Kinect = new Kinect();

            IScreen.m_ScreenManager = m_ScreenManager;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            m_ScreenManager.AddScreen(Mode.TitleScreen, new TitleScreen(), Content);
            m_ScreenManager.AddScreen(Mode.ThemeScreen, new ThemeScreen(), Content);
            m_ScreenManager.AddScreen(Mode.LivingRoomScreen, new LivingRoomScreen(), Content);

            m_ScreenManager.SelectScreen(Mode.TitleScreen);

            // m_Kinect.Init(Content);
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            m_ScreenManager.Update(Mouse.GetState());
            m_ScreenManager.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            GraphicsDevice.BlendState = BlendState.Opaque;
            GraphicsDevice.DepthStencilState = DepthStencilState.Default;

            m_ScreenManager.Draw(spriteBatch);
            m_Kinect.Draw(spriteBatch);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}