using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MazeGenerator
{
    internal class Cell
    {
        private Texture2D wallTexture;
        private SpriteFont spriteFont;
        public Vector2 position;

        public bool wallLeft { get; set; }
        public bool wallRight { get; set; }
        public bool wallUp { get; set; }
        public bool wallDown { get; set; }

        public int x;
        public int y;

        public int width;
        public int height;

        public Cell(Texture2D wallTexture, SpriteFont spriteFont, Vector2 position, int x, int y, int width, int height)
        {
            this.wallTexture = wallTexture;
            this.spriteFont = spriteFont;

            this.x = x;
            this.y = y;

            this.position = position;

            this.width = width;
            this.height = height;

            wallDown = true;
            wallLeft = true;
            wallRight = true;
            wallUp = true;
        }

        public void reset()
        {
            wallDown = true;
            wallUp = true;
            wallRight = true;
            wallLeft = true;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //draw walls
            if (wallLeft)   spriteBatch.Draw(wallTexture, new Rectangle((int)position.X, (int)position.Y, 1, height + 1), Color.White);
            if (wallRight)  spriteBatch.Draw(wallTexture, new Rectangle((int)position.X + width, (int)position.Y, 1, height + 1), Color.White);
            if (wallUp)     spriteBatch.Draw(wallTexture, new Rectangle((int)position.X, (int)position.Y, width + 1, 1), Color.White);
            if (wallDown)   spriteBatch.Draw(wallTexture, new Rectangle((int)position.X, (int)position.Y + height, width + 1, 1), Color.White);

            /*string str = x / width + "," + y / height;
            Vector2 origin = (spriteFont.MeasureString(str) * 0.5f);
            Vector2 pos = new Vector2(x + (width / 2), y + (height / 2));

            spriteBatch.DrawString(spriteFont, str, pos, Color.White, 0f, origin, 1f, SpriteEffects.None, 1f);*/
        }
    }
}
