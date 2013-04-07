using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace Exit_2R
{
    class Button
    {
        Texture2D m_Normal;
        Texture2D m_Over;

        Rectangle m_ButtonRec;

        Vector2 m_Position;

        int m_ButtonCounter = 0;

        public delegate void ClickEvent();

        public ClickEvent UserEvent;

        public void Init(ContentManager content, Vector2 position, string normalTextureName, string pressTextureName)
        {
            m_Normal = content.Load<Texture2D>(normalTextureName);
            m_Over = content.Load<Texture2D>(pressTextureName);

            m_Position = position;

            m_ButtonRec = new Rectangle((int)position.X, (int)position.Y, m_Normal.Width, m_Normal.Height);
        }

        public void Update(MouseState mouseState)
        {
            if (m_ButtonRec.Contains((int)mouseState.X, (int)mouseState.Y))
            {
                m_ButtonCounter++;
                if (m_ButtonCounter >= 90)
                {
                    UserEvent();
                    m_ButtonCounter = 0;
                }
            }
            else
            {
                m_ButtonCounter = 0;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(m_Normal, m_ButtonRec, Color.White);
            spriteBatch.Draw(m_Over, m_Position, new Rectangle(0, 0, m_Over.Width * m_ButtonCounter / 90 * 100 / 100, m_Over.Height), Color.White);
        }
    }
}
