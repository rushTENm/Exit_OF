using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Exit_OF
{
    class TitleScreen : IScreen
    {
        Texture2D m_BackgroundImage;

        Button m_DrillButton;
        Button m_RescueButton;
        Button m_SettingButton;

        MouseState mouseState;

        public override void Init(ContentManager content)
        {
            m_BackgroundImage = content.Load<Texture2D>(@"TitleScreen\titleBackground");

            m_DrillButton = new Button();
            m_DrillButton.Init(content, new Vector2(270, 300), @"TitleScreen\drillNomal", @"TitleScreen\drillHover");
            m_DrillButton.UserEvent = OnHoverDrillButton;

            m_RescueButton = new Button();
            m_RescueButton.Init(content, new Vector2(270, 465), @"TitleScreen\rescueNomal", @"TitleScreen\rescueHover");
            m_RescueButton.UserEvent = OnHoverRescueButton;

            m_SettingButton = new Button();
            m_SettingButton.Init(content, new Vector2(20, 598), @"TitleScreen\settingNomal", @"TitleScreen\settingHover");
            m_SettingButton.UserEvent = OnHoverSettingButton;
        }

        private void OnHoverDrillButton()
        {
        }

        private void OnHoverRescueButton()
        {
            m_ScreenManager.SelectScreen(Mode.StageRScreen);
        }

        private void OnHoverSettingButton()
        {
            m_ScreenManager.SelectScreen(Mode.SettingsScreen);
        }

        public override void Update(GameTime gameTime)
        {
            mouseState = Mouse.GetState();

            m_DrillButton.Update(mouseState);
            m_RescueButton.Update(mouseState);
            m_SettingButton.Update(mouseState);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(m_BackgroundImage, Vector2.Zero, Color.White);

            m_DrillButton.Draw(spriteBatch);
            m_RescueButton.Draw(spriteBatch);
            m_SettingButton.Draw(spriteBatch);

            spriteBatch.End();
        }
    }
}