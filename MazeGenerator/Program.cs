using System;
using System.Collections.Generic;
using System.Linq;

namespace MazeGenerator
{
    public static class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            using (var game = new MazeRunner())
                game.Run();
        }

    }
}