using System;
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

        Exit_OF m_Game;
        GraphicsDevice m_GraphicsDevice;
        ContentManager m_Content;

        public ScreenManager(Exit_OF game, GraphicsDevice graphicsDevice, ContentManager content)
        {
            m_Game = game;
            m_GraphicsDevice = graphicsDevice;
            m_Content = content;
        }

        public void AddScreen(Mode mode, IScreen screen, ContentManager content)
        {
            screen.Init(content);
            m_DicScreen.Add(mode, screen);
        }

        public void ResetHEScreen()
        {
            m_ActiveScreen = m_DicScreen[Mode.StageEScreen];

            m_DicScreen[Mode.HomeEScreen] = null;
            HomeEScreen reset = new HomeEScreen(m_Game, m_GraphicsDevice, m_Content);
            reset.Init(m_Content);
            m_DicScreen[Mode.HomeEScreen] = reset;

            m_ActiveScreen = m_DicScreen[Mode.HomeEScreen];

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