using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace Exit_3R
{
    class Button
    {
        Texture2D m_Normal;
        Texture2D m_Hover;

        Rectangle m_ButtonRec;

        int m_ButtonCounter = 0;
        const int m_TargetTime = 90;

        public delegate void HoverEvent();

        public HoverEvent UserEvent;

        public void Init(ContentManager content, Vector2 position, string normalTextureName, string hoverTextureName)
        {
            m_Normal = content.Load<Texture2D>(normalTextureName);
            m_Hover = content.Load<Texture2D>(hoverTextureName);

            m_ButtonRec = new Rectangle((int)position.X, (int)position.Y, m_Normal.Width, m_Normal.Height);
        }

        public void Update(MouseState mouseState)
        {
            if (m_ButtonRec.Contains((int)mouseState.X, (int)mouseState.Y))
            {
                m_ButtonCounter++;
                if (m_ButtonCounter > m_TargetTime)
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
            spriteBatch.Draw(m_Hover, new Vector2(m_ButtonRec.X, m_ButtonRec.Y + m_Hover.Height - (m_Hover.Height * m_ButtonCounter / m_TargetTime)), new Rectangle(0, m_Hover.Height - (m_Hover.Height * m_ButtonCounter / m_TargetTime), m_Hover.Width, m_Hover.Height * m_ButtonCounter / m_TargetTime), Color.White);
        }
    }
}