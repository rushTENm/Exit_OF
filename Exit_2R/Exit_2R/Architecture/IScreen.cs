using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Kinect;

namespace Exit_2R
{
    class IScreen
    {
        static public ScreenManager m_ScreenManager;

        public virtual void Init(ContentManager content) { }
        public virtual void Update(MouseState mouseState) { }
        public virtual void Update(KeyboardState keyboardState) { }
        public virtual void Update(GameTime gameTime) { }
        public virtual void Draw(SpriteBatch spriteBatch) { }
    }
}
