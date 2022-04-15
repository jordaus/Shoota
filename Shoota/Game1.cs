using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shoota
{
    public class Game1 : Game
    {

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Song backgroundMusic;
        Random random = new Random();
        public int enemyBulletDamage;

        Player player = new Player();
        Nebula nebula = new Nebula();
        HUDisplay HUD = new HUDisplay();

        //List of our meteors
        List<Meteor> meteorList = new List<Meteor>();
        List<Enemy> enemyList = new List<Enemy>();

        private StateManagement.ScreenManager screenManager;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.IsFullScreen = false;
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 950;
            this.Window.Title = "Shoota";
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            enemyBulletDamage = 10;
            screenManager = new StateManagement.ScreenManager(this);
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            screenManager.Initialize();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            /*player.LoadContent(Content);
            nebula.LoadContent(Content);
            HUD.LoadContent(Content);
            backgroundMusic = Content.Load<Song>("B R U H");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(backgroundMusic);*/
            StateManagement.GameScreen titleScreen = new Screen.TitleScreen();
            screenManager.AddScreen(titleScreen, PlayerIndex.One);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            screenManager.Update(gameTime);

            /*foreach(Enemy e in enemyList)
            {
                if (e.boundingRectangle.Intersects(player.boundingRectangle))
                {
                    player.health -= 40;
                    e.isVisible = false;
                }

                for(int i = 0; i < e.laserList.Count; i++)
                {
                    if (player.boundingRectangle.Intersects(e.laserList[i].boundingRectangle))
                    {
                        player.health -= enemyBulletDamage;
                        e.laserList[i].isVisible = false;
                    }
                }

                for(int i = 0; i < player.laserList.Count; i++)
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
                for(int i = 0; i < player.laserList.Count; i++)
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


            // TODO: Add your update logic here
            player.Update(gameTime);
            player.UpdateBullets();
            nebula.Update(gameTime);
            LoadMeteo();
            LoadEnemy();*/


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            screenManager.Draw(gameTime);
            /*_spriteBatch.Begin();
            nebula.Draw(_spriteBatch);
            HUD.Draw(_spriteBatch);
            foreach(Meteor m in meteorList)
            {
                m.Draw(_spriteBatch);
            }
            foreach(Enemy e in enemyList)
            {
                e.Draw(_spriteBatch);
            }
            player.Draw(_spriteBatch);


            _spriteBatch.End();*/

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        public void LoadMeteo()
        {
            int randY = random.Next(-600, -50);
            int randX = random.Next(0, 750);

            if(meteorList.Count < 5)
            {
                meteorList.Add(new Meteor(Content.Load<Texture2D>("planet_4"), new Vector2(randX, randY)));
            }

            for(int i = 0; i < meteorList.Count; i++)
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
                enemyList.Add(new Enemy(Content.Load<Texture2D>("ship_2"), new Vector2(randX, randY), Content.Load<Texture2D>("star")));
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
    }
}
