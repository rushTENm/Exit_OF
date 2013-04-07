using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Kinect;
using Microsoft.Xna.Framework;

namespace Exit_2R
{
    public enum Mode { TitleScreen, ThemeScreen, LivingRoomScreen }

    class ScreenManager
    {
        Dictionary<Mode, IScreen> m_DicScreen = new Dictionary<Mode, IScreen>();

        IScreen m_ActiveScreen = null;

        public void AddScreen(Mode mode, IScreen screen, ContentManager content)
        {
            screen.Init(content);
            m_DicScreen.Add(mode, screen);
        }

        public void SelectScreen(Mode mode)
        {
            m_ActiveScreen = m_DicScreen[mode];
        }

        public void Update(MouseState mouseState)
        {
            if (m_ActiveScreen != null)
                m_ActiveScreen.Update(mouseState);
        }

        public void Update(KeyboardState keyboardState)
        {
            if (m_ActiveScreen != null)
                m_ActiveScreen.Update(keyboardState);
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
