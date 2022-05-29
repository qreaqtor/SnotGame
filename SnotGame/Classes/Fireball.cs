using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace SnotGame.Classes
{
    public class Fireball : IBarrier
    {
        private readonly Picture[] fireballs;
        private readonly int length;
        private Picture lastBarrierPicture;
        private readonly Random random;

        public Fireball()
        {
            random = new Random();
            fireballs = Create(Picture.Fireball1, 25)
                .Concat(Create(Picture.Fireball2, 5))
                .Concat(Create(Picture.Fireball3, 1))
                .ToArray();
            length = fireballs.Length;
        }

        private IEnumerable<Picture> Create(Picture picture, int count) => Enumerable.Range(0, count).Select(n => picture);

        public (Picture, Point) CreateBarrier(Point lastBarrier)
        {
            var picture = fireballs[random.Next(0, length)];
            var size = Drawer.GetSize(picture);
            var startY = Game.FloorTop - size.Height;
            var line = random.Next(0, Game.FloorTop / size.Height);
            if (!lastBarrier.IsEmpty)
            {
                var width = Drawer.GetWidth(lastBarrierPicture);
                lastBarrierPicture = picture;
                return (picture, new Point(lastBarrier.X + width, startY - size.Height * line));
            }
            lastBarrierPicture = picture;
            return (picture, new Point(Game.Width, startY));
        }
    }
}
