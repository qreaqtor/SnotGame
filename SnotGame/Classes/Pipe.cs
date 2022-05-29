using System;
using System.Drawing;

namespace SnotGame.Classes
{
    public class Pipe : IBarrier
    {
        private readonly Picture[] pipes;
        private readonly int length;
        private int firstPart;
        private readonly int interval;
        private Picture secondPartPicture;
        private readonly Random random;

        public Pipe()
        {
            random = new Random();
            pipes = new[]
            {
                Picture.Pipe1, Picture.Pipe2, Picture.Pipe3, Picture.Pipe4, Picture.Pipe5, Picture.Pipe6, Picture.Pipe7, Picture.Pipe8
            };
            length = pipes.Length;
            interval = 5;
            firstPart = random.Next(0, length);
        }

        public (Picture, Point) CreateBarrier(Point lastBarrier)
        {
            var firstPartPicture = pipes[firstPart];
            secondPartPicture = pipes[length - 1 - firstPart];
            firstPart += GetNextEmpty();
            var size = Drawer.GetSize(firstPartPicture);
            var startY = Game.FloorTop - size.Height;
            if (lastBarrier.IsEmpty)
                return (firstPartPicture, new Point(Game.Width, startY));
            return (firstPartPicture, new Point(lastBarrier.X + size.Width * interval, startY));
        }

        private int GetNextEmpty()
        {
            switch(firstPart)
            {
                case 0: 
                    return random.Next(1, 3);
                case 1: 
                    return random.Next(1, 3);
                case 6: 
                    return random.Next(-2, 0);
                case 7: 
                    return random.Next(-2, 0);
                default: 
                    return random.Next(-2, 3);
            };
        }

        public (Picture, Point) GetSecondPart(Point firstPart) => (secondPartPicture, new Point(firstPart.X, Game.RoofBottom));
    }
}
