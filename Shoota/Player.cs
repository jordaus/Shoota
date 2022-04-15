using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Shoota
{
    public class Player
    {
        public Rectangle boundingRectangle, healthRectangle;
        public Texture2D texture, laserTexture, healthTexture;
        public Vector2 position, healthbarPosition;
        public float laserDelay;
        public int speed, health;
        public bool isColliding;
        public List<Laser> laserList;

        public Player()
        {
            laserList = new List<Laser>();
            texture = null;
            position = new Vector2(300, 300);
            laserDelay = 5;
            speed = 10;
            isColliding = false;
            health = 200;
            healthbarPosition = new Vector2(50, 50);
        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("ship_4");
            laserTexture = content.Load<Texture2D>("star");
            healthTexture = content.Load<Texture2D>("Red 16px1");
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, position, Color.White);
            sb.Draw(healthTexture, healthRectangle, Color.White);
            foreach(Laser l in laserList)
            {
                l.Draw(sb);
            }
        }

        public void Update(GameTime gt)
        {
            KeyboardState keyState = Keyboard.GetState();

            //bounding box for our player ship
            boundingRectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);

            //set rectangle for health
            healthRectangle = new Rectangle((int)healthbarPosition.X, (int)healthbarPosition.Y, health, 32);

            //fire laser
            if (keyState.IsKeyDown(Keys.Space))
            {
                Shoot();
            }
            //ship controlls
            if (keyState.IsKeyDown(Keys.W))
            {
                position.Y = position.Y - speed;
            }
            if (keyState.IsKeyDown(Keys.A))
            {
                position.X = position.X - speed;
            }
            if (keyState.IsKeyDown(Keys.S))
            {
                position.Y = position.Y + speed;
            }
            if (keyState.IsKeyDown(Keys.D))
            {
                position.X = position.X + speed;
            }

            //bound check
            if (position.X <= 0) position.X = 0;
            if (position.X >= 800 - texture.Width) position.X = 800 - texture.Width;
            if (position.Y <= 0) position.Y = 0;
            if (position.Y >= 950 - texture.Height) position.Y = 950 - texture.Height;


            
        }

        public void Shoot()
        {
            if(laserDelay >= 0)
            {
                laserDelay--;
            }

            if(laserDelay <= 0)
            {
                Laser newLaser = new Laser(laserTexture);
                newLaser.position = new Vector2(position.X + 32 - newLaser.texture.Width / 2, position.Y + 30);

                newLaser.isVisible = true;

                if(laserList.Count < 20)
                {
                    laserList.Add(newLaser);
                }
            }

            if(laserDelay == 0)
            {
                laserDelay = 20;
            }
        }

        public void UpdateBullets()
        {
            foreach(Laser l in laserList)
            {
                l.boundingRectangle = new Rectangle((int)l.position.X, (int)l.position.Y, l.texture.Width, l.texture.Height);
                l.position.Y = l.position.Y - l.speed;

                if(l.position.Y <= 0)
                {
                    l.isVisible = false;
                }
            }

            for(int i = 0; i < laserList.Count; i++)
            {
                if (!laserList[i].isVisible)
                {
                    laserList.RemoveAt(i);
                    i--;
                }
            }
        }
    }
}
