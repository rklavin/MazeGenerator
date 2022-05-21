using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace MazeGenerator.Rooms
{
    abstract internal class GameRoom : IDisposable
    {
        public ContentManager Content { get; private set; }
        public static GameObjectManager? gameObjectManager { get; private set; }

        public GameRoom(IServiceProvider serviceProvider)
        {
            Content = new ContentManager(serviceProvider, "Content");
            gameObjectManager = new GameObjectManager();
        }

        public virtual void Update(GameTime gameTime)
        {
            gameObjectManager.Update(gameTime);
        }

        public virtual void Dispose()
        {
            Content.Unload();
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            gameObjectManager.Draw(gameTime, spriteBatch);
            spriteBatch.End();
        }
    }
}
