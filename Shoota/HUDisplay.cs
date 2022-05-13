using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Shoota
{
    public class HUDisplay
    {
        public int score;
        public int highscore;
        public int width;
        public int height;
        public SpriteFont scoreFont;
        public Vector2 scorePos;
        public Vector2 highScorePos;
        public bool showDisplay;

        public HUDisplay()
        {
            highscore = 4000;
            score = 0;
            showDisplay = true;
            height = 950;
            width = 800;
            scoreFont = null;
            scorePos = new Vector2(width / 2, 50);
            highScorePos = new Vector2(width / 2, 80);
        }

        public void LoadContent(ContentManager content)
        {
            scoreFont = content.Load<SpriteFont>("Impact");
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (showDisplay)
            {
                spriteBatch.DrawString(scoreFont, "Score - " + score, scorePos, Color.Orange);
                spriteBatch.DrawString(scoreFont, "HighScore - " + highscore, highScorePos, Color.Lime);
            }
                
            
        }
    }
}
