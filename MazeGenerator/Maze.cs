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
    public enum moves
    {
        up = 0,
        down = 1,
        left = 2,
        right = 3
    }

    internal class Maze : GameObject
    {
        public Cell[][] mazeGrid;
        public int width;
        public int height;
        public int cellSize;
        public bool built;

        private Texture2D wallTexture;
        private SpriteFont spriteFont;

        public Maze(Texture2D sprite, Vector2 position, int width, int height, int cellSize) : base(sprite, position)
        {
            this.width = width;
            this.height = height;
            this.cellSize = cellSize;

            wallTexture = GlobalAssets.Pixel;
            spriteFont = GlobalAssets.Font;

            mazeGrid = new Cell[width][];
            for (int i = 0; i < width; i++)
            {
                mazeGrid[i] = new Cell[height];
                
                for (int j = 0; j < height; j++)
                {
                    mazeGrid[i][j] = new Cell(wallTexture, spriteFont, new Vector2(position.X + (i * cellSize), position.Y + (j * cellSize)), i, j, cellSize, cellSize);
                }
            }

            built = false;
        }

        public void BuildMaze(int width, int height, int cellSize)
        {
            this.cellSize = cellSize;
            NewMaze(width, height, cellSize);

            built = true;
            if ((width == 0) || (height == 0))
                built = false;
        }

        private void NewMaze(int width, int height, int cellSize)
        {
            Random random = new Random();

            Cell[][] maze = new Cell[width][];
            for (int i = 0; i < width; i++)
            {
                maze[i] = new Cell[height];

                for (int j = 0; j < height; j++)
                {
                    maze[i][j] = new Cell(wallTexture, spriteFont, new Vector2(position.X + (i * cellSize), position.Y + (j * cellSize)), i, j, cellSize, cellSize);
                    //mazeGrid[i][j].reset();
                }
            }

            mazeGrid = maze;

            Cell cell = mazeGrid[(int)width / 2][(int) height / 2];
            moves move;
            Stack<Cell> cellStack = new Stack<Cell>();
            List<Cell> visitedCells = new List<Cell>();

            cellStack.Push(cell);
            visitedCells.Add(cell);

            while (visitedCells.Count < (width * height))
            {
                List<moves> movesInBounds = getMovesInBounds(cell);
                List<moves> validMoves = new List<moves>();
                foreach (moves dir in movesInBounds)
                {
                    if (!visitedCells.Contains(moveCell(cell, dir))) validMoves.Add(dir);
                }

                if (validMoves.Count > 0)
                {
                    move = validMoves[random.Next(validMoves.Count)];
                    cell = build(cell, move);
                    cellStack.Push(cell);
                    visitedCells.Add(cell);
                }
                else
                {
                    cell = cellStack.Pop();
                }
            }
        }

        /*public void BuildLabyrinthe()
        {
            //run through maze, keep track of all dead ends
            //on each dead end build through a random side
            Random random = new Random();

            Cell cell = mazeGrid[0][0];
            moves move;
            Stack<Cell> cellStack = new Stack<Cell>();
            List<Cell> visitedCells = new List<Cell>();
            List<Cell> deadEnds = new List<Cell>();

            cellStack.Push(cell);
            visitedCells.Add(cell);

            while (visitedCells.Count < (width * height))
            {
                List<moves> movesInBounds = getMovesInBounds(cell);
                List<moves> validMoves = new List<moves>();
                foreach (moves dir in movesInBounds)
                {
                    if (!visitedCells.Contains(moveCell(cell, dir))) validMoves.Add(dir);
                }

                if (validMoves.Count > 0)
                {
                    move = validMoves[random.Next(validMoves.Count)];
                    cell = build(cell, move);
                    cellStack.Push(cell);
                    visitedCells.Add(cell);
                }
                else
                {
                    cell = cellStack.Pop();
                }
            }
        }*/

        public bool validMove(Cell cell, moves move)
        {
            if (!moveInBounds(cell, move)) return false;

            switch (move)
            {
                case moves.up:
                    if (cell.wallUp) return false;
                    break;

                case moves.down:
                    if (cell.wallDown) return false;
                    break;

                case moves.left:
                    if (cell.wallLeft) return false;
                    break;

                case moves.right:
                    if (cell.wallRight) return false;
                    break;

                default:
                    break;
            }

            return true;
        }

        public bool moveInBounds(Cell cell, moves move)
        {
            switch (move)
            {
                case moves.up:
                    if (cell.y > 0) return true;
                    break;

                case moves.down:
                    if (cell.y < (mazeGrid[cell.x].Length - 1)) return true;
                    break;

                case moves.left:
                    if (cell.x > 0) return true;
                    break;

                case moves.right:
                    if (cell.x < (mazeGrid.Length - 1)) return true;
                    break;

                default:
                    break;
            }

            return false;
        }

        public List<moves> getMovesInBounds(Cell cell)
        {
            List<moves> list = new List<moves> { moves.up, moves.down, moves.left, moves.right};
            List<moves> ret = new List<moves>();

            foreach (moves move in list)
            {
                if (moveInBounds(cell, move)) ret.Add(move);
            }

            return ret;
        }

        public List<moves> getValidMoves(Cell cell)
        {
            List<moves> list = getMovesInBounds(cell);
            List<moves> ret = new List<moves>();

            foreach (moves move in list)
            {
                if (validMove(cell, move)) ret.Add(move);
            }

            return ret;
        }

        private Cell build(Cell cell, moves move)
        {
            if (!moveInBounds(cell, move)) return cell;

            switch (move)
            {
                case moves.up:
                    cell.wallUp = false;
                    cell = mazeGrid[cell.x][cell.y - 1];
                    cell.wallDown = false;
                    break;

                case moves.down:
                    cell.wallDown = false;
                    cell = mazeGrid[cell.x][cell.y + 1];
                    cell.wallUp = false;
                    break;

                case moves.left:
                    cell.wallLeft = false;
                    cell = mazeGrid[cell.x - 1][cell.y];
                    cell.wallRight = false;
                    break;

                case moves.right:
                    cell.wallRight = false;
                    cell = mazeGrid[cell.x + 1][cell.y];
                    cell.wallLeft = false;
                    break;

                default:
                    break;
            }

            return cell;
        }

        public Cell moveCell(Cell cell, moves move)
        {
            if (!moveInBounds(cell, move)) return cell;

            switch (move)
            {
                case moves.up:
                    cell = mazeGrid[cell.x][cell.y - 1];
                    break;

                case moves.down:
                    cell = mazeGrid[cell.x][cell.y + 1];
                    break;

                case moves.left:
                    cell = mazeGrid[cell.x - 1][cell.y];
                    break;

                case moves.right:
                    cell = mazeGrid[cell.x + 1][cell.y];
                    break;

                default:
                    break;
            }

            return cell;
        }

        public override void Update(GameTime gameTime)
        {
            if (Input.KeyPressed(Keys.B))
                BuildMaze(width, height, cellSize);

            if (Input.KeyPressed(Keys.L))
                BuildMaze(width, height, cellSize);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (Cell[] cell in mazeGrid)
            {
                foreach (Cell cell2 in cell)
                {
                    cell2.Draw(gameTime, spriteBatch);
                }
            }
        }
    }
}
