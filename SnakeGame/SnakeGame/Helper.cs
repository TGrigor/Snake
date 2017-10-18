using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    enum Direction
    {
        Right,
        Up,
        Left,
        Down,
    }

    static class Helper
    {
        public static int MaxConsoleWidth { get; } = Console.WindowWidth;
        public static int MaxConsoleHeight { get; } = Console.WindowHeight;

        public static void WriteAt(Coordinates cord, char charecter = '*')
        {
           Console.SetCursorPosition(cord.CoordinateX, cord.CoordinateY);
           Console.Write(charecter);
        }
    }

    class Coordinates
    {
        public int CoordinateX { get; set; }
        public int CoordinateY { get; set; }
    }
}
