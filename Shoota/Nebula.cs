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
    public class Nebula
    {
        public Texture2D texture;
        public Vector2 bgPos1, bgPos2;
        public int speed;

        public Nebula()
        {
            texture = null;
            bgPos1 = new Vector2(0, 0);
            bgPos2 = new Vector2(0, -950);
            speed = 5;
        }

        public void LoadContent(ContentManager Content)
        {
            texture = Content.Load<Texture2D>("blunea");
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, bgPos1, Color.White);
            sb.Draw(texture, bgPos2, Color.White);
        }

        public void Update(GameTime gameTime)
        {
            bgPos1.Y = bgPos1.Y + speed;
            bgPos2.Y = bgPos2.Y + speed;

            if(bgPos1.Y >= 950)
            {
                bgPos1.Y = 0;
                bgPos2.Y = -950;
            }
        }
    }
}
