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
    internal class MazeRoom : GameRoom
    {
        public static GameObject? Maze { get; private set; }
        public static GameObject? Player { get; private set; }
        public static GameObject? Goal { get; private set; }

        public MazeRoom(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            //add room objects
            Maze = new Maze(GlobalAssets.Null, Vector2.Zero, 20, 10, 40);
            gameObjectManager?.Add(Maze);

            Player = new Player(GlobalAssets.Player, Vector2.Zero);
            gameObjectManager?.Add(Player);

            Goal = new Goal(GlobalAssets.Goal, Vector2.Zero);
            gameObjectManager?.Add(Goal);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Dispose()
        {
            Maze?.Dispose();
            Maze = null;
            base.Dispose();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
        }
    }
}
