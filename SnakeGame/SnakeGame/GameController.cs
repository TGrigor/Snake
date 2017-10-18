using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    class GameController
    {
        private int HeartCount = 3;
        private List<Coordinates> FrameList;
        private Snake _Snake;
        private Health _Health;

        public GameController()
        {
            Console.CursorVisible = false;
            Console.SetWindowSize(Helper.MaxConsoleWidth, Helper.MaxConsoleHeight);

            FrameList = new List<Coordinates>();

            var startIndex = new Coordinates() { CoordinateX = 2, CoordinateY = 2 };
            _Snake = new Snake(HeartCount, 100, startIndex);
            _Health = new Health();

            PrintFrame();
            PrintPointCount();

            Console.ReadKey();
            StartGame();
        }

        public void StartGame()
        {
            var sakeController = Task.Factory.StartNew(() => { SnakeController(); });
            var setGameControl = Task.Factory.StartNew(() => { SetGameControl(); });
            var healthController = Task.Factory.StartNew(() => { EventController(); });

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

        private bool EventController()
        {
            while (true)
            {
                if (_Health.HealthPosition.CoordinateX == _Snake.SnakePosition.CoordinateX 
                 && _Health.HealthPosition.CoordinateY == _Snake.SnakePosition.CoordinateY)
                {
                    HeartCount += 1;

                    _Snake.AddHeartToSnake();
                    _Snake.AddSpeed();
                    _Health.DeleteHealth();
                    _Health.GetRandomPosition();
                    _Health.PrintHealth();
                    PrintPointCount();
                }

                if (FrameList.Where(f => f.CoordinateX == _Snake.SnakePosition.CoordinateX && f.CoordinateY == _Snake.SnakePosition.CoordinateY).ToList().Any())
                {
                    Console.Clear();
                    return false;
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
                        _Snake.Direction = Direction.Right;
                        break;
                }
            }
        }

        private void PrintFrame()
        {
            Console.ForegroundColor = ConsoleColor.Red;

            for (int i = 0; i < Helper.MaxConsoleWidth; i++)
            {
                FrameList.Add(new Coordinates() { CoordinateX = i, CoordinateY = 1 });
                Console.SetCursorPosition(i, 1);
                Console.Write("_");
            }
            for (int i = 0; i < Helper.MaxConsoleWidth; i++)
            {
                FrameList.Add(new Coordinates() { CoordinateX = i, CoordinateY = Helper.MaxConsoleHeight - 1 });
                Console.SetCursorPosition(i, Helper.MaxConsoleHeight - 1);
                Console.Write("_");
            }
            for (int i = 0; i < Helper.MaxConsoleHeight; i++)
            {
                FrameList.Add(new Coordinates() { CoordinateX = 0, CoordinateY = i });
                Console.SetCursorPosition(0, i);
                Console.Write("|");
            }
            for (int i = 0; i < Helper.MaxConsoleHeight; i++)
            {
                FrameList.Add(new Coordinates() { CoordinateX = Helper.MaxConsoleWidth - 1, CoordinateY = i });
                Console.SetCursorPosition(Helper.MaxConsoleWidth - 1, i);
                Console.Write("|");
            }

            Console.ForegroundColor = ConsoleColor.White;
        }

        private void PrintPointCount()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Helper.WriteAt(new Coordinates() { CoordinateX = 5, CoordinateY = 0 }, ' ');
            Console.Write(" Your heart count is - ");
            Console.Write(HeartCount);
            Console.ForegroundColor = ConsoleColor.White;
        }

    }
}
