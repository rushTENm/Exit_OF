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
    class TitleScreen : IScreen
    {
        Texture2D m_BackgroundImage;

        Button m_RescueButton;
        Button m_SettingButton;

        public override void Init(ContentManager content)
        {
            m_BackgroundImage = content.Load<Texture2D>(@"TitleScreen\titleBackground");

            m_RescueButton = new Button();
            m_RescueButton.Init(content, new Vector2(170, 344), @"TitleScreen\rescueNomal", @"TitleScreen\rescueOver");
            m_RescueButton.UserEvent = OnClickRescueButton;

            m_SettingButton = new Button();
            m_SettingButton.Init(content, new Vector2(182, 344 + 150), @"TitleScreen\settingNomal", @"TitleScreen\settingOver");
            m_SettingButton.UserEvent = OnClickSettingButton;
        }

        private void OnClickRescueButton()
        {
            m_ScreenManager.SelectScreen(Mode.ThemeScreen);
        }

        private void OnClickSettingButton()
        {
        }

        public override void Update(MouseState mouseState)
        {
            m_RescueButton.Update(mouseState);
            m_SettingButton.Update(mouseState);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(m_BackgroundImage, Vector2.Zero, Color.White);

            m_RescueButton.Draw(spriteBatch);
            m_SettingButton.Draw(spriteBatch);
        }
    }
}
