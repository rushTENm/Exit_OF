using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Exit_3R
{
    class IScreen
    {
        static public ScreenManager m_ScreenManager;

        public virtual void Init(ContentManager content) { }
        public virtual void Update(MouseState mouseState) { }
        public virtual void Update(GameTime gameTIme) { }
        public virtual void Draw(SpriteBatch spriteBatch) { }
    }
}