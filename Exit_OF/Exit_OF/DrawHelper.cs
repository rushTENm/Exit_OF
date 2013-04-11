using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Exit_OF
{
    class DrawHelper
    {
        Vector2 m_GoPosition = new Vector2(433, 234);
        Texture2D m_Gogo;
        Texture2D m_Go1;
        Texture2D m_Go2;
        Texture2D m_Go3;
        public bool m_IsStarted = false;
        public int m_GoCounter = 0;

        Texture2D dialer;
        Texture2D dialer9;
        Texture2D dialer91;
        Texture2D dialer911;
        Texture2D dialerCall;
        Texture2D dialerEnd;
        public bool Isdialer = true;
        public int dialerCounter = 0;

        Texture2D inventory;
        Texture2D barRed;
        Texture2D barGreen;

        public void Init(ContentManager content)
        {
            m_Gogo = content.Load<Texture2D>(@"HomeRScreen\gogo");
            m_Go1 = content.Load<Texture2D>(@"HomeRScreen\go1");
            m_Go2 = content.Load<Texture2D>(@"HomeRScreen\go2");
            m_Go3 = content.Load<Texture2D>(@"HomeRScreen\go3");

            dialer = content.Load<Texture2D>(@"HomeRScreen\dialer");
            dialer9 = content.Load<Texture2D>(@"HomeRScreen\dialer9");
            dialer91 = content.Load<Texture2D>(@"HomeRScreen\dialer91");
            dialer911 = content.Load<Texture2D>(@"HomeRScreen\dialer911");
            dialerCall = content.Load<Texture2D>(@"HomeRScreen\dialerCall");
            dialerEnd = content.Load<Texture2D>(@"HomeRScreen\dialerEnd");

            inventory = content.Load<Texture2D>(@"HomeRScreen\inventory");
            barRed = content.Load<Texture2D>(@"HomeRScreen\barRed");
            barGreen = content.Load<Texture2D>(@"HomeRScreen\barGreen");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            DrawGo(spriteBatch);

            DrawDialer(spriteBatch);

            spriteBatch.Draw(inventory, Vector2.Zero, Color.White);
            spriteBatch.Draw(barRed, new Vector2(1104,28),Color.White);
            spriteBatch.Draw(barGreen, new Vector2(1104,88), Color.White);

            spriteBatch.End();
        }

        private void DrawGo(SpriteBatch spriteBatch)
        {
            if (m_IsStarted == false)
            {
                if (m_GoCounter > 240)
                {
                    m_IsStarted = true;
                }
                else if (m_GoCounter > 180)
                {
                    spriteBatch.Draw(m_Gogo, m_GoPosition, Color.White);
                }
                else if (m_GoCounter > 120)
                {
                    spriteBatch.Draw(m_Go1, m_GoPosition, Color.White);
                }
                else if (m_GoCounter > 60)
                {
                    spriteBatch.Draw(m_Go2, m_GoPosition, Color.White);
                }
                else
                {
                    spriteBatch.Draw(m_Go3, m_GoPosition, Color.White);
                }

                m_GoCounter++;
            }
        }

        private void DrawDialer(SpriteBatch spriteBatch)
        {
            if (Isdialer == false)
            {
                if (dialerCounter > 360)
                {
                    Isdialer = true;
                }
                else if (dialerCounter > 300)
                {
                    spriteBatch.Draw(dialerEnd, new Vector2(0, 0), null, Color.White, 0f, Vector2.Zero, 768 / 1280f, SpriteEffects.None, 0);
                }
                else if (dialerCounter > 240)
                {
                    spriteBatch.Draw(dialerCall, new Vector2(0, 0), null, Color.White, 0f, Vector2.Zero, 768 / 1280f, SpriteEffects.None, 0);
                }
                else if (dialerCounter > 180)
                {
                    spriteBatch.Draw(dialer911, new Vector2(0, 0), null, Color.White, 0f, Vector2.Zero, 768 / 1280f, SpriteEffects.None, 0);

                }
                else if (dialerCounter > 120)
                {
                    spriteBatch.Draw(dialer91, new Vector2(0, 0), null, Color.White, 0f, Vector2.Zero, 768 / 1280f, SpriteEffects.None, 0);
                }
                else if (dialerCounter > 60)
                {
                    spriteBatch.Draw(dialer9, new Vector2(0, 0), null, Color.White, 0f, Vector2.Zero, 768 / 1280f, SpriteEffects.None, 0);
                }
                else
                {
                    spriteBatch.Draw(dialer, new Vector2(0, 0), null, Color.White, 0f, Vector2.Zero, 768 / 1280f, SpriteEffects.None, 0);
                }
                dialerCounter++;
            }
        }
    }
}
