using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace SnotGame.Classes
{
    public class Fence
    {
        private readonly int FloorStartY;
        private readonly int RoofStartY;
        private List<Point> floor;
        private List<Point> roof;
        private int fenceMove;
        private readonly Picture picture;
        private readonly int Width;

        public Fence(Hero hero)
        {
            ChangeFence(hero);
            picture = Picture.Fence;
            Width = Drawer.GetWidth(picture);
            FloorStartY = Game.FloorTop;
            RoofStartY = Game.RoofBottom - Drawer.GetHeight(picture);
            floor = Enumerable.Range(0, Game.Width / Width + 1).Select(n => new Point(n * Width, FloorStartY)).ToList();
            roof = Enumerable.Range(0, Game.Width / Width + 1).Select(n => new Point(n * Width, RoofStartY)).ToList();
            foreach (var e in floor)
                Drawer.ToDraw.Add((picture, e));
            foreach (var e in roof)
                Drawer.ToDraw.Add((picture, e));
        }

        public void Move()
        {
            var last = floor.LastOrDefault();
            if (last.X < Game.Width)
            {
                floor.Add(new Point(last.X + Width, FloorStartY));
                roof.Add(new Point(last.X + Width, RoofStartY));
            }
            for (int i = 0; i < floor.Count; i++)
            {
                roof[i] = new Point(floor[i].X - Width, RoofStartY);
                floor[i] = new Point(floor[i].X - fenceMove, FloorStartY);
            }
            if (floor.First().X + Width < 0)
            {
                floor.RemoveAt(0);
                roof.RemoveAt(0);
            }
            foreach (var e in floor)
                Drawer.ToDraw.Add((picture, e));
            foreach (var e in roof)
                Drawer.ToDraw.Add((picture, e));
        }

        public void ChangeFence(Hero newHero)
        {
            switch (newHero)
            {
                case Hero.Suit:
                    fenceMove = Game.PipeMove; break;
                case Hero.UFO:
                    fenceMove = Game.FireballMove; break;
                case Hero.YellowSnot:
                    fenceMove = Game.VerticaLBrickMove; break;
                case Hero.GreenSnot:
                    fenceMove = Game.HorizontalBrickMove; break;
            }
        }
    }
}
