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

namespace Exit_OF
{
    public class Exit_OF : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        ScreenManager m_ScreenManager;

        public Exit_OF()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 1366;
            graphics.PreferredBackBufferHeight = 768;
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            m_ScreenManager = new ScreenManager();

            IScreen.m_ScreenManager = m_ScreenManager;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            m_ScreenManager.AddScreen(Mode.TitleScreen, new TitleScreen(), Content);
            m_ScreenManager.AddScreen(Mode.SettingsScreen, new SettingsScreen(), Content);
            m_ScreenManager.AddScreen(Mode.StageEScreen, new StageEScreen(), Content);
            m_ScreenManager.AddScreen(Mode.ResultEScreen, new ResultEScreen(), Content);
            m_ScreenManager.AddScreen(Mode.ResultBadEScreen, new ResultBadEScreen(), Content);
            m_ScreenManager.AddScreen(Mode.HomeEScreen, new HomeEScreen(this, GraphicsDevice, Content), Content);

            m_ScreenManager.SelectScreen(Mode.TitleScreen);
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
            m_ScreenManager.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            GraphicsDevice.DepthStencilState = DepthStencilState.Default;

            m_ScreenManager.Draw(spriteBatch);

            base.Draw(gameTime);
        }
    }
}
