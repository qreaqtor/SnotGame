using System;
using System.Drawing;

namespace SnotGame.Classes
{
    public class VerticalBrick : IBarrier
    {
        private readonly Picture[] bricks;
        private readonly int[] possibleLocations;
        private readonly int length;
        private int lastY;
        private int count;
        private readonly int maxCount;
        private readonly int interval;
        private readonly Random random;

        public VerticalBrick()
        {
            random = new Random();
            bricks = new[]
            {
                Picture.Brick, Picture.BrickVertical2, Picture.BrickVertical3,
                Picture.BrickVertical4, Picture.BrickVertical5, Picture.BrickVertical6
            };
            possibleLocations = new[] { Game.RoofBottom, Game.FloorTop };
            length = bricks.Length;
            count = 0;
            maxCount = 3;
            interval = 3;
        }

        public (Picture, Point) CreateBarrier(Point lastBarrier)
        {
            var picture = bricks[random.Next(0, length)];
            var size = Drawer.GetSize(picture);
            var startY = GetLocationY(size.Height);
            if (lastBarrier.IsEmpty)
                return (picture, new Point(Game.Width, startY));
            return (picture, new Point(lastBarrier.X + size.Width * interval, startY));
        }

        private int GetLocationY(int height)
        {
            var index = random.Next(0, 2);
            var startY = possibleLocations[index];
            if (startY == lastY)
                count++;
            else
                count = 0;
            if (count == maxCount)
            {
                count = 0;
                startY = possibleLocations[1 - index];
            }
            lastY = startY;
            if (startY == Game.FloorTop)
                startY -= height;
            return startY;
        }
    }
}
