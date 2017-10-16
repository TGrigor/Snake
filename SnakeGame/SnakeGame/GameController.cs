using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    class GameController
    {
        private int MaxConsoleWidth { get; set; }
        private int MaxConsoleHeight { get; set; }
        private Snake _Snake { get; set; }
        private Health _Health { get; set; } 

        public GameController()
        {
            MaxConsoleWidth = Console.WindowWidth;
            MaxConsoleHeight = Console.WindowHeight;
            Console.SetWindowSize(MaxConsoleWidth, MaxConsoleHeight);

            var startIndex = new Coordinates() { CoordinateX = 2, CoordinateY = 2 };
            _Snake = new Snake(3, 60, startIndex);
            _Health = new Health(MaxConsoleWidth, MaxConsoleHeight);

            PrintFrame();

            Console.ReadKey();
            RunControler();
        }


        public void RunControler()
        {
            var sakeController = Task.Factory.StartNew(() => { SnakeController(); });
            var setGameControl = Task.Factory.StartNew(() => { SetGameControl(); });
            var healthController = Task.Factory.StartNew(() => { HealthController(); });

            // Wait for ALL tasks to finish
            // Control will block here until all 3 finish in parallel
            Task.WaitAll(new[] { sakeController, setGameControl, healthController });
        }

        private void SnakeController()
        {
            while (true)
            {
                var charecterPosition = _Snake.GetNewPosition();
                _Snake.SnakeRun(charecterPosition);
            }

        }
        private void HealthController()
        {
            while (true)
            {
                if (_Health.HealthPosition == _Snake.SnakePosition)
                {
                    AddHeartToSnake();
                    _Health.DeleteHealth();
                    _Health.GetRandomPosition();
                    _Health.PrintHealth();
                }
            }
        }
        private void SetGameControl()
        {
            while (true)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                switch (keyInfo.Key)
                {
                    case ConsoleKey.RightArrow:
                        _Snake.Direction = Direction.Right;
                        break;
                    case ConsoleKey.UpArrow:
                        _Snake.Direction = Direction.Up;
                        break;
                    case ConsoleKey.LeftArrow:
                        _Snake.Direction = Direction.Left;
                        break;
                    case ConsoleKey.DownArrow:
                        _Snake.Direction = Direction.Down;
                        break;
                    default:
                        _Snake.Direction = Direction.Non;
                        break;
                }
            }
        }
        private void AddHeartToSnake()
        {
            var charecterPosition = _Snake.GetNewPosition();
            _Snake.AddNewCharecter(charecterPosition);
        }
        private void PrintFrame()
        {
            Console.ForegroundColor = ConsoleColor.Red;

            for (int i = 0; i < MaxConsoleWidth; i++)
            {
                Console.SetCursorPosition(i, 1);
                Console.Write("_");
            }
            for (int i = 0; i < MaxConsoleWidth; i++)
            {
                Console.SetCursorPosition(i, MaxConsoleHeight - 1);
                Console.Write("_");
            }
            for (int i = 0; i < MaxConsoleHeight; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write("|");
            }
            for (int i = 0; i < MaxConsoleHeight; i++)
            {
                Console.SetCursorPosition(MaxConsoleWidth - 1, i);
                Console.Write("|");
            }

            Console.ForegroundColor = ConsoleColor.White;
        }


    }
}
