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
    internal class GameObjectManager
    {
        private List<GameObject> gameObjects;
        private List<GameObject> gameObjectsToAdd;
        public bool isUpdating { get; private set; }

        public GameObjectManager()
        {
            gameObjects = new List<GameObject>();
            gameObjectsToAdd = new List<GameObject>();
            isUpdating = false;
        }

        public void Add(GameObject gameObject)
        {
            if (!isUpdating)
                gameObjects.Add(gameObject);
            else
                gameObjectsToAdd.Add(gameObject);
        }

        public void Update(GameTime gameTime)
        {
            //can't modify gameObject list while running foreach, add objects later
            isUpdating = true;

            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.Update(gameTime);
            }

            isUpdating = false;

            foreach (GameObject gameObject in gameObjectsToAdd)
            {
                Add(gameObject);
            }

            gameObjectsToAdd.Clear();

            //remove disposed objects from list
            gameObjects = gameObjects.Where(x => !x.toDispose).ToList();
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.Draw(gameTime, spriteBatch);
            }
        }
    }
}
