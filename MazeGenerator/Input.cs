using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Linq;

namespace MazeGenerator
{
	static class Input
	{
		private static KeyboardState keyboardState, lastKeyboardState;
		private static MouseState mouseState, lastMouseState;
		private static GamePadState gamepadState, lastGamepadState;

		public static void Update()
		{
			lastKeyboardState = keyboardState;
			lastMouseState = mouseState;
			lastGamepadState = gamepadState;

			keyboardState = Keyboard.GetState();
			mouseState = Mouse.GetState();
			gamepadState = GamePad.GetState(PlayerIndex.One);
		}

		public static bool KeyPressed(Keys key)
		{
			return (!lastKeyboardState.IsKeyDown(key) && keyboardState.IsKeyDown(key));
		}

		public static bool KeyDown(Keys key)
        {
			return keyboardState.IsKeyDown(key);
		}

		public static bool KeyUp(Keys key)
		{
			return keyboardState.IsKeyUp(key);
		}

		public static bool ButtonPressed(Buttons button)
		{
			return (!lastGamepadState.IsButtonDown(button) && gamepadState.IsButtonDown(button));
		}
	}
}