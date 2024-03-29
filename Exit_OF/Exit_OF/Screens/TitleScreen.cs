﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Exit_OF
{
    class TitleScreen : IScreen
    {
        Texture2D m_BackgroundImage;
        Texture2D PreludeImage;
        int PreludeCounter = 0;

        Button m_DrillButton;
        Button m_ExitButton;
        Button m_SettingButton;

        MouseState mouseState;

        Song BGM;

        public override void Init(ContentManager content)
        {
            m_BackgroundImage = content.Load<Texture2D>(@"TitleScreen\titleBackground");
            PreludeImage = content.Load<Texture2D>(@"TitleScreen\preludeBackground");

            m_DrillButton = new Button();
            m_DrillButton.Init(content, new Vector2(350, 335), @"TitleScreen\drillNomal", @"TitleScreen\drillHover");
            m_DrillButton.UserEvent = OnHoverDrillButton;

            m_ExitButton = new Button();
            m_ExitButton.Init(content, new Vector2(350, 510), @"TitleScreen\exitNomal", @"TitleScreen\exitHover");
            m_ExitButton.UserEvent = OnHoverRescueButton;

            m_SettingButton = new Button();
            m_SettingButton.Init(content, new Vector2(20, 598), @"TitleScreen\settingsNomal", @"TitleScreen\settingsHover");
            m_SettingButton.UserEvent = OnHoverSettingButton;

            BGM = content.Load<Song>("BGM");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(BGM);
        }

        private void OnHoverDrillButton()
        {
        }

        private void OnHoverRescueButton()
        {
            m_ScreenManager.SelectScreen(Mode.StageEScreen);
        }

        private void OnHoverSettingButton()
        {
            m_ScreenManager.SelectScreen(Mode.SettingsScreen);
        }

        public override void Update(GameTime gameTime)
        {
            mouseState = Mouse.GetState();

            m_DrillButton.Update(mouseState);
            m_ExitButton.Update(mouseState);
            m_SettingButton.Update(mouseState);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            
            if (PreludeCounter < 60)
            {
                spriteBatch.Draw(PreludeImage, Vector2.Zero, Color.White);
                PreludeCounter++;
            }
            else
            {
                spriteBatch.Draw(m_BackgroundImage, Vector2.Zero, Color.White);

                m_DrillButton.Draw(spriteBatch);
                m_ExitButton.Draw(spriteBatch);
                m_SettingButton.Draw(spriteBatch);
            }

            spriteBatch.End();
        }
    }
}