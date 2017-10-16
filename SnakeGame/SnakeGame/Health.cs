using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    class Health
    {
        public Random Random { get; set; }

        private int MaxWidth { get; set; }
        private int MaxHeight { get; set; }
        public Coordinates HealthPosition { get; set; }

        public Health(int MaxWidth,int MaxHeight)
        {
            this.MaxWidth = MaxWidth - 1;
            this.MaxHeight = MaxHeight - 1;
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
                Helper.WriteAt(HealthPosition.CoordinateX, HealthPosition.CoordinateY, (char)215);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        public void DeleteHealth()
        {
            Helper.WriteAt(HealthPosition.CoordinateX, HealthPosition.CoordinateY,' ');
        }

    }
}
