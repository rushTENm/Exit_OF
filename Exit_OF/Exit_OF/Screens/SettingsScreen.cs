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
    class SettingsScreen : IScreen
    {
        Texture2D m_BackgroundImage;
        Texture2D check;
        Texture2D selectEdge;

        MouseState mouseState;

        Button m_BackButton;

        public override void Init(ContentManager content)
        {
            m_BackgroundImage = content.Load<Texture2D>(@"SettingsScreen\SettingBackground");
            check = content.Load<Texture2D>(@"SettingsScreen\check");
            selectEdge = content.Load<Texture2D>(@"settingsScreen\selectEdge");

            m_BackButton = new Button();
            m_BackButton.Init(content, new Vector2(6, 6), @"SettingsScreen\backNomal", @"SettingsScreen\backHover");
            m_BackButton.UserEvent = OnHoverBackButton;
        }

        private void OnHoverBackButton()
        {
            m_ScreenManager.SelectScreen(Mode.TitleScreen);
        }

        public override void Update(GameTime gameTime)
        {
            mouseState = Mouse.GetState();

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