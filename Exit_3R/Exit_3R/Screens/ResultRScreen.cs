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
    class ResultRScreen : IScreen
    {
        Texture2D m_GoodBackgroundImage;
        Texture2D m_BadackgroundImage;

        Button m_AnotherPlaceButton;

        public override void Init(ContentManager content)
        {
            m_GoodBackgroundImage = content.Load<Texture2D>(@"ResultRScreen\resultGoodBackground");
            m_BadackgroundImage = content.Load<Texture2D>(@"ResultRScreen\resultBadBackground");

            m_AnotherPlaceButton = new Button();
            m_AnotherPlaceButton.Init(content, new Vector2(483,294), @"ResultRScreen\anotherPlaceNomal", @"ResultRScreen\anotherPlaceHover");
            m_AnotherPlaceButton.UserEvent = OnHoverAnotherPlaceButton;
        }

        private void OnHoverAnotherPlaceButton()
        {
            m_ScreenManager.SelectScreen(Mode.StageRScreen);
        }

        public override void Update(MouseState mouseState)
        {
            m_AnotherPlaceButton.Update(mouseState);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(m_GoodBackgroundImage, Vector2.Zero, Color.White);

            m_AnotherPlaceButton.Draw(spriteBatch);

            spriteBatch.End();
        }
    }
}