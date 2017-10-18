using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    class Health
    {
        private int MaxWidth;
        private int MaxHeight;

        public Coordinates HealthPosition { get; set; }
        public static Random Random;

        public Health()
        {
            this.MaxWidth = Helper.MaxConsoleWidth - 5;
            this.MaxHeight = Helper.MaxConsoleHeight - 5;
            HealthPosition = new Coordinates();
            Random = new Random();

            GetRandomPosition();
            PrintHealth();
        }

        public void GetRandomPosition()
        {
            HealthPosition = new Coordinates() { CoordinateX = Random.Next(1, MaxWidth), CoordinateY = Random.Next(1, MaxHeight) };
        }

        public void PrintHealth()
        {
            if (HealthPosition != null)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Helper.WriteAt(HealthPosition, (char)215);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        public void DeleteHealth()
        {
            Helper.WriteAt(HealthPosition,' ');
        }

    }
}
