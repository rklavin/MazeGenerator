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
    internal class Player : GameObject
    {
        public Cell? where;

        private int keyDelay = 0;
        private const int keyDelayTime = 6;

        public Player(Texture2D sprite, Vector2 position) : base(sprite, position)
        {

        }

        public override void Update(GameTime gameTime)
        {
            Maze? maze = (Maze?)Rooms.MazeRoom.Maze;

            if (keyDelay > 0) keyDelay -= 1;

            if (maze != null)
            {
                if (maze.built)
                {
                    if (where == null) where = maze.mazeGrid[0][0];
                    where = maze.mazeGrid[where.x][where.y];

                    if (Input.KeyDown(Keys.Up))
                    {
                        if (keyDelay <= 0)
                        {
                            keyDelay = keyDelayTime;
                            if (maze.validMove(where, moves.up))
                                where = maze.moveCell(where, moves.up);
                        }
                    }
                    else if (Input.KeyDown(Keys.Down))
                    {
                        if (keyDelay <= 0)
                        {
                            keyDelay = keyDelayTime;
                            if (maze.validMove(where, moves.down))
                                where = maze.moveCell(where, moves.down);
                        }
                    }
                    else if (Input.KeyDown(Keys.Left))
                    {
                        if (keyDelay <= 0)
                        {
                            keyDelay = keyDelayTime;
                            if (maze.validMove(where, moves.left))
                                where = maze.moveCell(where, moves.left);
                        }
                    }
                    else if (Input.KeyDown(Keys.Right))
                    {
                        if (keyDelay <= 0)
                        {
                            keyDelay = keyDelayTime;
                            if (maze.validMove(where, moves.right))
                                where = maze.moveCell(where, moves.right);
                        }
                    }
                    else
                    {
                        keyDelay = 0;
                    }

                    if (where != null) position = where.position;
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
