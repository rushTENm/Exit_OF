using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Exit_OF
{
    class PauseScreen : IScreen
    {
        Texture2D m_BackgroundImage;

        MouseState mouseState;

        Button m_BackButton;
        Button m_ContinueButton;
        Button m_RestartButton;

        public bool IsPause = false;

        public override void Init(ContentManager content)
        {
            m_BackgroundImage = content.Load<Texture2D>(@"PauseScreen\pauseBackground");

            m_BackButton = new Button();
            m_BackButton.Init(content, new Vector2(876,283), @"PauseScreen\backNomal", @"PauseScreen\backHover");
            m_BackButton.UserEvent = OnHoverBackButton;

            m_ContinueButton = new Button();
            m_ContinueButton.Init(content, new Vector2(312,283), @"PauseScreen\continueNomal", @"PauseScreen\continueHover");
            m_ContinueButton.UserEvent = OnHoverContinueButton;

            m_RestartButton = new Button();
            m_RestartButton.Init(content, new Vector2(594,283), @"PauseScreen\restartNomal", @"PauseScreen\restartHover");
            m_RestartButton.UserEvent = OnHoverRestartButton;
        }

        private void OnHoverBackButton()
        {
            m_ScreenManager.SelectScreen(Mode.StageEScreen);
        }

        private void OnHoverContinueButton()
        {
            IsPause = false;
        }

        private void OnHoverRestartButton()
        {
        }

        public override void Update(GameTime gameTime)
        {
            mouseState = Mouse.GetState();

            m_BackButton.Update(mouseState);
            m_ContinueButton.Update(mouseState);
            m_RestartButton.Update(mouseState);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(m_BackgroundImage, Vector2.Zero, Color.White);

            m_BackButton.Draw(spriteBatch);
            m_ContinueButton.Draw(spriteBatch);
            m_RestartButton.Draw(spriteBatch);

            spriteBatch.End();
        }
    }
}