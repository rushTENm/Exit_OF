﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Exit_OF
{
    public enum Mode { TitleScreen, StageEScreen, SettingsScreen, HomeEScreen, ResultEScreen, ResultBadEScreen, PauseScreen }

    class ScreenManager
    {
        Dictionary<Mode, IScreen> m_DicScreen = new Dictionary<Mode, IScreen>();

        IScreen m_ActiveScreen = null;

        public void AddScreen(Mode mode, IScreen screen, ContentManager content)
        {
            screen.Init(content);
            m_DicScreen.Add(mode, screen);
        }

        public void RemoveScreen(Mode mode)
        {
            m_DicScreen[mode] = null;
        }

        public void SelectScreen(Mode mode)
        {
            m_ActiveScreen = m_DicScreen[mode];
        }

        public void Update(GameTime gameTime)
        {
            if (m_ActiveScreen != null)
                m_ActiveScreen.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (m_ActiveScreen != null)
                m_ActiveScreen.Draw(spriteBatch);
        }
    }
}