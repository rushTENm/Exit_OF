using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Exit_OF
{
    class IScreen
    {
        static public ScreenManager m_ScreenManager;

        public virtual void Init(ContentManager content) { }
        public virtual void Update(GameTime gameTIme) { }
        public virtual void Draw(SpriteBatch spriteBatch) { }
    }
}
