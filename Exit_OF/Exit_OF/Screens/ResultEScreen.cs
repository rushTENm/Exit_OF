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
    class ResultEScreen : IScreen
    {
        MouseState mouseState;

        Texture2D m_GoodBackgroundImage;
        Texture2D m_BadackgroundImage;

        Button m_StageButton;
        Button m_TitleButton;
        Button m_RestartButton;

        public override void Init(ContentManager content)
        {
            m_GoodBackgroundImage = content.Load<Texture2D>(@"ResultEScreen\resultGoodBackground");
            m_BadackgroundImage = content.Load<Texture2D>(@"ResultEScreen\resultBadBackground");

            m_StageButton = new Button();
            m_StageButton.Init(content, new Vector2(770,400), @"ResultEScreen\stageNomal", @"ResultEScreen\stageHover");
            m_StageButton.UserEvent = OnHoverStageButton;

            m_TitleButton = new Button();
            m_TitleButton.Init(content, new Vector2(1040,400), @"ResultEScreen\titleNomal", @"ResultEScreen\titleHover");
            m_TitleButton.UserEvent = OnHoverTitleButton;

            m_RestartButton = new Button();
            m_RestartButton.Init(content, new Vector2(500,400), @"ResultEScreen\restartNomal", @"ResultEScreen\restartHover");
            m_RestartButton.UserEvent = OnHoverRestartButton;
        }

        private void OnHoverStageButton()
        {
            m_ScreenManager.SelectScreen(Mode.StageEScreen);
        }

        private void OnHoverTitleButton()
        {
            m_ScreenManager.SelectScreen(Mode.TitleScreen);
        }

        private void OnHoverRestartButton()
        {
            m_ScreenManager.SelectScreen(Mode.HomeEScreen);
        }

        public override void Update(GameTime gameTime)
        {
            mouseState = Mouse.GetState();

            m_StageButton.Update(mouseState);
            m_TitleButton.Update(mouseState);
            m_RestartButton.Update(mouseState);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(m_GoodBackgroundImage, Vector2.Zero, Color.White);

            m_StageButton.Draw(spriteBatch);
            m_TitleButton.Draw(spriteBatch);
            m_RestartButton.Draw(spriteBatch);

            spriteBatch.End();
        }
    }
}