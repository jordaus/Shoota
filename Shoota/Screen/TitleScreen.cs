﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Shoota.Screen
{
    public class TitleScreen : StateManagement.GameScreen
    {
        private Texture2D title;
        
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
            title = _content.Load<Texture2D>("sigma");
            
            cube = new Cube(game);
        }

        public override void HandleInput(GameTime gameTime, StateManagement.InputState input)
        {
            PlayerIndex player;
            if (input.IsKeyPressed(Keys.Enter, null, out player))
            {
                GameplayScreen liftOffGameplay = new GameplayScreen();
                ScreenManager.AddScreen(liftOffGameplay, player);
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
            
            ScreenManager.SpriteBatch.Draw(title, new Vector2(300, 300), Color.White);
            //ScreenManager.SpriteBatch.DrawString(impact, "Shoota!", new Vector2(350, 350), Color.White);
            ScreenManager.SpriteBatch.DrawString(impact, "Press Enter!", new Vector2(325, 500), Color.White);
            ScreenManager.SpriteBatch.DrawString(impact, "Try to beat the high score!", new Vector2(225, 550), Color.Yellow);
            cube.Draw();
            
            ScreenManager.SpriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
