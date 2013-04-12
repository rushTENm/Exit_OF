using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Exit_OF
{
    class StageEScreen : IScreen
    {
        Texture2D m_BackgroundImage;

        MouseState mouseState;

        Button m_BackButton;
        Button m_HomeButton;

        Texture2D m_Where;
        Texture2D place;

        public override void Init(ContentManager content)
        {
            m_BackgroundImage = content.Load<Texture2D>(@"StageEScreen\stageBackground");

            m_BackButton = new Button();
            m_BackButton.Init(content, new Vector2(6, 6), @"StageEScreen\backNomal", @"StageEScreen\backHover");
            m_BackButton.UserEvent = OnHoverBackButton;

            m_HomeButton = new Button();
            m_HomeButton.Init(content, new Vector2(7, 202), @"StageEScreen\homeNomal", @"StageEScreen\homeHover");
            m_HomeButton.UserEvent = OnHoverHomeButton;

            m_Where = content.Load<Texture2D>(@"StageEScreen\where");

            place = content.Load<Texture2D>(@"StageEScreen\place");
        }

        private void OnHoverBackButton()
        {
            m_ScreenManager.SelectScreen(Mode.TitleScreen);
        }

        private void OnHoverHomeButton()
        {
            m_ScreenManager.SelectScreen(Mode.HomeEScreen);
        }

        public override void Update(GameTime gameTime)
        {
            mouseState = Mouse.GetState();

            m_BackButton.Update(mouseState);
            m_HomeButton.Update(mouseState);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(m_BackgroundImage, Vector2.Zero, Color.White);
            spriteBatch.Draw(m_Where, new Vector2(177, 44), Color.White);

            m_BackButton.Draw(spriteBatch);
            m_HomeButton.Draw(spriteBatch);

            spriteBatch.Draw(place, new Vector2(447, 202), Color.White);
            spriteBatch.Draw(place, new Vector2(859, 202), Color.White);

            spriteBatch.End();
        }
    }
}