using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SnakeGame
{
    class Snake
    {
        private int Length;
        private int Speed;
        private List<Coordinates> SnakeCoordinate;
        public Coordinates SnakePosition { get; set; }
        public Direction Direction { get; set; }

        public Snake(int snakeLength, int snakeSpeed, Coordinates startIndex)
        {
            SnakeCoordinate = new List<Coordinates>();
            SnakePosition = startIndex;

            Length = snakeLength;
            Speed = snakeSpeed;
            int setX;
            int setY;

            for (int i = 0; i < snakeLength; i++)
            {
                setX = startIndex.CoordinateX + i;
                setY = startIndex.CoordinateY;
                SnakeCoordinate.Add(new Coordinates() { CoordinateX = setX, CoordinateY = setY });
                Helper.WriteAt(new Coordinates() { CoordinateX = setX, CoordinateY = setY } );
            }
        }

        public Coordinates GetNewPosition()
        {
            var newCharecterPosition = SnakeCoordinate.Last();
            Coordinates cord = new Coordinates()
            {
                CoordinateX = newCharecterPosition.CoordinateX,
                CoordinateY = newCharecterPosition.CoordinateY
            };

            switch (Direction)
            {
                case SnakeGame.Direction.Right:
                        cord.CoordinateX++;
                    break;
                case SnakeGame.Direction.Up:
                        cord.CoordinateY--;
                    break;
                case SnakeGame.Direction.Left:
                        cord.CoordinateX--;
                    break;
                case SnakeGame.Direction.Down:
                        cord.CoordinateY++;
                    break;
                default:
                        cord.CoordinateX++;
                    break;
            }

            SnakePosition = cord;
            return cord;
        }
       
        public void SnakeRun(Coordinates newCharecter)
        {
            Thread.Sleep(Speed);

            //Add new charecter
            Helper.WriteAt(newCharecter);
            SnakeCoordinate.Add(newCharecter);

            //Delete first charecter
            Helper.WriteAt(SnakeCoordinate[0], ' ');
            SnakeCoordinate.RemoveAt(0);            
        }

        public void AddNewCharecter(Coordinates newCharecter)
        {
            //Add new charecter
            Helper.WriteAt(newCharecter);
            SnakeCoordinate.Add(newCharecter);
        }

        public void AddHeartToSnake()
        {
            var charecterPosition = GetNewPosition();
            AddNewCharecter(charecterPosition);
        }

        public void AddSpeed(int speed = 10)
        {
            this.Speed += speed;
        }
    }    
}
