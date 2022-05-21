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
    internal class Goal : GameObject
    {
        public Cell? where;
        private Cell[][] prevMaze;

        public Goal(Texture2D sprite, Vector2 position) : base(sprite, position)
        {

        }

        public override void Update(GameTime gameTime)
        {
            Maze? maze = (Maze?)Rooms.MazeRoom.Maze;
            Player? player = (Player?)Rooms.MazeRoom.Player;

            if (maze != null)
            {
                if (maze.built)
                {
                    if (where == null) where = maze.mazeGrid[maze.mazeGrid.Length - 1][maze.mazeGrid[0].Length - 1];

                    if (maze.mazeGrid != prevMaze)
                        where = maze.mazeGrid[maze.mazeGrid.Length - 1][maze.mazeGrid[0].Length - 1];

                    /*bool cellFound = false;
                    foreach (Cell[] cells in maze.mazeGrid)
                    {
                        foreach (Cell cell in cells)
                        {
                            cellFound = (where == cell);
                        }
                    }

                    if (!cellFound) where = maze.mazeGrid[maze.mazeGrid.Length - 1][maze.mazeGrid[0].Length - 1];*/

                    if (player != null)
                    {
                        if (player.where != null)
                        {
                            if (player.where == where)
                            {
                                maze.BuildMaze(maze.width, maze.height, maze.cellSize);
                                player.where = maze.mazeGrid[0][0];
                                where = maze.mazeGrid[maze.mazeGrid.Length - 1][maze.mazeGrid[0].Length - 1];
                            }
                        }
                    }

                    if (where != null) position = where.position;
                    prevMaze = maze.mazeGrid;
                }
            }


            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Vector2 pos = position;
            pos.X += 1;
            pos.Y += 1;

            Rectangle rect = new Rectangle((int)pos.X, (int)pos.Y, 1, 1);
            if (where != null)
            {
                rect.Width = where.width - 1;
                rect.Height = where.height - 1;
            }

            boundingBox = rect;

            spriteBatch.Draw(sprite, pos, rect, Color.White);
            //base.Draw(gameTime, spriteBatch);
        }
    }
}
