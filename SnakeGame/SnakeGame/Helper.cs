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
        Non
    }

    static class Helper
    {
        public static void WriteAt(int x, int y, char charecter = '*')
        {
            try
            {
                Console.SetCursorPosition(x, y);
                Console.Write(charecter);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
            }
        }
    }
    class Coordinates
    {
        public int CoordinateX { get; set; }
        public int CoordinateY { get; set; }
    }
}
