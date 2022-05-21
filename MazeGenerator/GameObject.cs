using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace MazeGenerator
{
    abstract internal class GameObject
    {
        public Texture2D sprite;
        public Vector2 position;
        public Rectangle boundingBox;
        public bool toDispose;      //lets a GameObjectManager know this can be discarded

        public GameObject(Texture2D sprite, Vector2 position)
        {
            this.sprite = sprite;
            this.position = position;
            boundingBox = sprite.Bounds;
        }

        public virtual void Update(GameTime gameTime)
        {
            boundingBox.X = (int)position.X;
            boundingBox.Y = (int)position.Y;
        }

        public virtual void Dispose()
        {
            toDispose = true;
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, position, Color.White);
        }
    }
}
