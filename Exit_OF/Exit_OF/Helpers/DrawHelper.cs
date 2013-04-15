using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;

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
        SoundEffect GoSFX;
        bool GoSFXPlayed = false;

        Texture2D dialer;
        Texture2D dialer9;
        Texture2D dialer91;
        Texture2D dialer911;
        Texture2D dialerCall;
        Texture2D dialerEnd;
        public bool Isdialed = false;
        public int dialerCounter = 0;
                SoundEffect phone;
        bool phonePlayed;

        Texture2D inventory;
        Texture2D barHP;
        Texture2D barHPEdge;
        Texture2D barBrave;
        Texture2D barBraveEdge;
        const int HPMax = 1000;
        public int HP = HPMax;
        public bool HPDown = false;
        const int braveMax = 2000;
        public int brave = braveMax;
        public bool braveDown = false;
        Texture2D itemEdge;
        Texture2D extinguisher2D;
        public bool IsExtinguisherGet;

        public Texture2D loseBrave;
        public Texture2D loseHP;
        public Texture2D MustCall;
        public Texture2D fireTooStr;

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
            barHP = content.Load<Texture2D>(@"HomeRScreen\barRed");
            barHPEdge = content.Load<Texture2D>(@"HomeRScreen\barEdge");
            barBrave = content.Load<Texture2D>(@"HomeRScreen\barGreen");
            barBraveEdge = content.Load<Texture2D>(@"HomeRScreen\barEdge");
            itemEdge = content.Load<Texture2D>(@"HomeRScreen\itemEdge");
            extinguisher2D = content.Load<Texture2D>(@"HomeRScreen\extinguisher2D");

            loseBrave = content.Load<Texture2D>(@"IsFireBrave");
            loseHP = content.Load<Texture2D>(@"IsFireHP");
            MustCall = content.Load<Texture2D>(@"CallTex");
            fireTooStr = content.Load<Texture2D>(@"fireTooStr");

            GoSFX = content.Load<SoundEffect>(@"HomeEScreen\GoSFX");
            phone = content.Load<SoundEffect>(@"HomeEScreen\phoneSFX");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            DrawGo(spriteBatch);

            spriteBatch.Draw(inventory, Vector2.Zero, Color.White);

            spriteBatch.Draw(barHP, new Vector2(1104,28),new Rectangle(0,0,barHP.Width * HP / HPMax, barHP.Height), Color.White);
            if (HP/30%2==0 && HPDown)
            {
                spriteBatch.Draw(barHPEdge, new Vector2(993, 23), Color.White);
            }
            spriteBatch.Draw(barBrave, new Vector2(1104,88), new Rectangle(0,0,barBrave.Width * brave /braveMax, barBrave.Height), Color.White);
            if (brave / 30 % 2 == 1 && braveDown)
            {
                spriteBatch.Draw(barBraveEdge, new Vector2(994, 83), Color.White);
            }
            if (IsExtinguisherGet)
            {
                spriteBatch.Draw(extinguisher2D, new Vector2(299, 24), Color.White);
                spriteBatch.Draw(itemEdge, new Vector2(299, 24), Color.White);
            }

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
                    if (!GoSFXPlayed)
                    {
                        GoSFX.Play();
                        GoSFXPlayed = true;
                    }
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

        public void DrawDialer(SpriteBatch spriteBatch)
        {
            if (Isdialed == false)
            {
                if (dialerCounter > 960)
                {
                    Isdialed = true;
                }
                else if (dialerCounter > 960)
                {
                    spriteBatch.Draw(dialerEnd, new Vector2(0, 149), null, Color.White, 0f, Vector2.Zero, 619 / 1280f, SpriteEffects.None, 0);
                }
                else if (dialerCounter > 240)
                {
                    spriteBatch.Draw(dialerCall, new Vector2(0, 149), null, Color.White, 0f, Vector2.Zero, 619 / 1280f, SpriteEffects.None, 0);
                    if (!phonePlayed)
                    {
                        phone.Play();
                        phonePlayed = true;
                    }
                }
                else if (dialerCounter > 180)
                {
                    spriteBatch.Draw(dialer911, new Vector2(0, 149), null, Color.White, 0f, Vector2.Zero, 619 / 1280f, SpriteEffects.None, 0);
                }
                else if (dialerCounter > 120)
                {
                    spriteBatch.Draw(dialer91, new Vector2(0, 149), null, Color.White, 0f, Vector2.Zero, 619 / 1280f, SpriteEffects.None, 0);
                }
                else if (dialerCounter > 60)
                {
                    spriteBatch.Draw(dialer9, new Vector2(0, 149), null, Color.White, 0f, Vector2.Zero, 619 / 1280f, SpriteEffects.None, 0);
                }
                else
                {
                    spriteBatch.Draw(dialer, new Vector2(0, 149), null, Color.White, 0f, Vector2.Zero, 619 / 1280f, SpriteEffects.None, 0);
                }
                dialerCounter++;
            }
        }
    }
}
