using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Kinect;
using Microsoft.Xna.Framework.Input;

namespace Exit_2R
{
    class ThemeScreen : IScreen
    {
        Texture2D m_BackgroundImage;

        Button m_BackButton;
        Button m_FireButton;
        Texture2D m_SirenBlack;

        Texture2D temp;

        public override void Init(ContentManager content)
        {
            m_BackgroundImage = content.Load<Texture2D>(@"ThemeScreen\themeBackground");

            m_BackButton = new Button();
            m_BackButton.Init(content, new Vector2(30, 30), @"ThemeScreen\backNomal", @"ThemeScreen\backOver");
            m_BackButton.UserEvent = OnClickBackButton;

            m_FireButton = new Button();
            m_FireButton.Init(content, new Vector2(59, 200), @"ThemeScreen\sirenNomal", @"ThemeScreen\sirenOver");
            m_FireButton.UserEvent = OnClickFireButton;

            m_SirenBlack = content.Load < Texture2D > (@"ThemeScreen\sirenBlack");

            temp = content.Load<Texture2D>("temp");
        }

        private void OnClickBackButton()
        {
            m_ScreenManager.SelectScreen(Mode.TitleScreen);
        }

        private void OnClickFireButton()
        {
            m_ScreenManager.SelectScreen(Mode.LivingRoomScreen);
        }

        public override void Update(MouseState mouseState)
        {
            m_BackButton.Update(mouseState);
            m_FireButton.Update(mouseState);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(m_BackgroundImage, Vector2.Zero, Color.White);

            spriteBatch.Draw(temp, new Rectangle(0, 0, 350, 200), Color.White);

            m_BackButton.Draw(spriteBatch);
            m_FireButton.Draw(spriteBatch);

            spriteBatch.Draw(m_SirenBlack, new Vector2(487, 192), Color.White);
            spriteBatch.Draw(m_SirenBlack, new Vector2(907,192), Color.White);
        }
    }
}
