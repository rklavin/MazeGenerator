using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MazeGenerator
{
    internal class GlobalAssets
	{
		public static Texture2D Player { get; private set; }
		public static Texture2D Goal { get; private set; }
		public static Texture2D Null { get; private set; }
		public static Texture2D Pixel { get; private set; }     // a single white pixel
		public static SpriteFont Font { get; private set; }

		public static void Load(ContentManager content)
		{
			Player = content.Load<Texture2D>("Wall");

			Null = new Texture2D(Player.GraphicsDevice, 1, 1);
			Null.SetData(new[] { Color.Transparent });

			Pixel = new Texture2D(Player.GraphicsDevice, 1, 1);
			Pixel.SetData(new[] { Color.White });

			Goal = new Texture2D(Player.GraphicsDevice, 1, 1);
			Goal.SetData(new[] { Color.Red });

			Player = new Texture2D(Player.GraphicsDevice, 1, 1);
			Player.SetData(new[] { Color.Black });

			Font = content.Load<SpriteFont>("MazeFont");
		}
	}
}
