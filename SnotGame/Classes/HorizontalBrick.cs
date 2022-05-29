using System;
using System.Drawing;

namespace SnotGame.Classes
{
    public class HorizontalBrick : IBarrier
    {
        private readonly Picture[] bricks;
        private readonly int length;
        private readonly int brickWidth;
        private int lastWidth;
        private readonly Random random;

        public HorizontalBrick()
        {
            random = new Random();
            bricks = new[]
            {
                Picture.Brick, Picture.BrickHorizontal2, Picture.BrickHorizontal3, Picture.BrickHorizontal4
            };
            length = bricks.Length;
            brickWidth = Drawer.GetWidth(Picture.Brick);
            lastWidth = 0;
        }

        public (Picture, Point) CreateBarrier(Point lastBarrier)
        {
            var picture = bricks[random.Next(0, length)];
            var size = Drawer.GetSize(picture);
            var startY = Game.FloorTop - size.Height;
            var width = lastWidth;
            lastWidth = size.Width;
            if (lastBarrier.IsEmpty)
                return (picture, new Point(Game.Width, startY));
            var interval = random.Next(3, 6);
            return (picture, new Point(lastBarrier.X + width + interval * brickWidth, startY));
        }
    }
}
