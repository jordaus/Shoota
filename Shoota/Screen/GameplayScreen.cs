using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace Shoota.Screen
{
    public class GameplayScreen : StateManagement.GameScreen
    {
        private SpriteBatch _spriteBatch;
        private Song backgroundMusic;
        Random random = new Random();
        public int enemyBulletDamage;
        private ContentManager _content;

        Player player;
        Nebula nebula;
        HUDisplay HUD;

        //List of our meteors
        List<Meteor> meteorList = new List<Meteor>();
        List<Enemy> enemyList = new List<Enemy>();

        public override void Activate()
        {
            player = new Player();
            nebula = new Nebula();
            HUD = new HUDisplay();
            enemyBulletDamage = 10;
            LoadContent();
        }

        protected void LoadContent()
        {
            _content = new ContentManager(ScreenManager.Game.Services, "Content");
            player.LoadContent(_content);
            nebula.LoadContent(_content);
            HUD.LoadContent(_content);
            backgroundMusic = _content.Load<Song>("B R U H");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(backgroundMusic);
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {

            foreach (Enemy e in enemyList)
            {
                if (e.boundingRectangle.Intersects(player.boundingRectangle))
                {
                    player.health -= 40;
                    e.isVisible = false;
                }

                for (int i = 0; i < e.laserList.Count; i++)
                {
                    if (player.boundingRectangle.Intersects(e.laserList[i].boundingRectangle))
                    {
                        player.health -= enemyBulletDamage;
                        e.laserList[i].isVisible = false;
                    }
                }

                for (int i = 0; i < player.laserList.Count; i++)
                {
                    if (player.laserList[i].boundingRectangle.Intersects(e.boundingRectangle))
                    {
                        HUD.score += 50;
                        player.laserList[i].isVisible = false;
                        e.isVisible = false;
                    }
                }
                e.Update(gameTime);
            }

            foreach (Meteor m in meteorList)
            {
                if (m.boundingRectangle.Intersects(player.boundingRectangle))
                {
                    player.health -= 20;
                    m.isVisible = false;
                }
                for (int i = 0; i < player.laserList.Count; i++)
                {
                    if (m.boundingRectangle.Intersects(player.laserList[i].boundingRectangle))
                    {
                        HUD.score += 25;
                        m.isVisible = false;
                        player.laserList[i].isVisible = false;
                    }
                }
                m.Update(gameTime);
            }

            if(player.health <= 0)
            {
                if(HUD.score < HUD.highscore)
                {
                    GameOverScreen gameOver = new GameOverScreen();
                    ScreenManager.AddScreen(gameOver, ControllingPlayer);
                }
                else
                {
                    GameOverScreenGood goodOver = new GameOverScreenGood();
                    ScreenManager.AddScreen(goodOver, ControllingPlayer);
                }
                
            }

            if(HUD.score >= HUD.highscore)
            {
                HUD.highscore = HUD.score;
            }


            // TODO: Add your update logic here
            player.Update(gameTime);
            player.UpdateBullets();
            nebula.Update(gameTime);
            LoadMeteo();
            LoadEnemy();
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }

        public void LoadMeteo()
        {
            int randY = random.Next(-600, -50);
            int randX = random.Next(0, 750);

            if (meteorList.Count < 5)
            {
                meteorList.Add(new Meteor(_content.Load<Texture2D>("planet_5"), new Vector2(randX, randY)));
            }

            for (int i = 0; i < meteorList.Count; i++)
            {
                if (!meteorList[i].isVisible)
                {
                    meteorList.RemoveAt(i);
                    i--;
                }
            }
        }

        public void LoadEnemy()
        {
            int randY = random.Next(-600, -50);
            int randX = random.Next(0, 750);

            if (enemyList.Count < 3)
            {
                enemyList.Add(new Enemy(_content.Load<Texture2D>("ship_2"), new Vector2(randX, randY), _content.Load<Texture2D>("star")));
            }

            for (int i = 0; i < enemyList.Count; i++)
            {
                if (!enemyList[i].isVisible)
                {
                    enemyList.RemoveAt(i);
                    i--;
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            ScreenManager.SpriteBatch.Begin();
            nebula.Draw(ScreenManager.SpriteBatch);
            HUD.Draw(ScreenManager.SpriteBatch);
            foreach (Meteor m in meteorList)
            {
                m.Draw(ScreenManager.SpriteBatch);
            }
            foreach (Enemy e in enemyList)
            {
                e.Draw(ScreenManager.SpriteBatch);
            }
            player.Draw(ScreenManager.SpriteBatch);


            ScreenManager.SpriteBatch.End();
        }
    }
}
