using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Shoota.Screen
{
    public class GameOverScreen : StateManagement.GameScreen
    {
        private ContentManager _content;
        private SpriteFont impact;
        Cube cube;

        public override void Activate()
        {
            Game1 game = ScreenManager.Game as Game1;
            LoadContent();
        }

        protected void LoadContent()
        {
            Game1 game = ScreenManager.Game as Game1;
            _content = new ContentManager(ScreenManager.Game.Services, "Content");
            impact = _content.Load<SpriteFont>("Impact");
            cube = new Cube(game);
        }

        public override void HandleInput(GameTime gameTime, StateManagement.InputState input)
        {
            PlayerIndex player;
            if (input.IsKeyPressed(Keys.Enter, null, out player))
            {
                GameplayScreen Gameplay = new GameplayScreen();
                ScreenManager.AddScreen(Gameplay, player);
            }
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            Game1 game = ScreenManager.Game as Game1;
            cube.Update(gameTime);
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }

        public override void Draw(GameTime gameTime)
        {
            ScreenManager.SpriteBatch.Begin();
            ScreenManager.SpriteBatch.DrawString(impact, "Game Over!", new Vector2(350, 350), Color.White);
            ScreenManager.SpriteBatch.DrawString(impact, "Press Esc to Quit!", new Vector2(250, 450), Color.White);
            cube.Draw();
            ScreenManager.SpriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
