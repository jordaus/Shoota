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
    public class Laser
    {
        public Rectangle boundingRectangle;
        public Texture2D texture;
        public Vector2 position;
        public Vector2 origin;
        public bool isVisible;
        public float speed;

        public Laser(Texture2D newTexture)
        {
            speed = 10;
            texture = newTexture;
            isVisible = false;
        }

        public void Update(GameTime gt)
        {
            float t = gt.ElapsedGameTime.Seconds;
            position.Y -= 100 * t;
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, position, Color.White);
        }
    }
}
