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
    class SettingScreen : IScreen
    {
        Texture2D m_BackgroundImage;

        Button m_BackButton;

        public override void Init(ContentManager content)
        {
            m_BackgroundImage = content.Load<Texture2D>(@"SettingScreen\SettingBackground");

            m_BackButton = new Button();
            m_BackButton.Init(content, new Vector2(6, 6), @"SettingScreen\backNomal", @"SettingScreen\backHover");
            m_BackButton.UserEvent = OnHoverBackButton;
        }

        private void OnHoverBackButton()
        {
            m_ScreenManager.SelectScreen(Mode.TitleScreen);
        }

        public override void Update(MouseState mouseState)
        {
            m_BackButton.Update(mouseState);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(m_BackgroundImage, Vector2.Zero, Color.White);

            m_BackButton.Draw(spriteBatch);

            spriteBatch.End();
        }
    }
}