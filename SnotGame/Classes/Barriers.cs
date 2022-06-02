using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace SnotGame.Classes
{
    public class Barriers
    {
        private List<(Picture Picture, Point Location)> barriers;
        private Hero hero;
        private int barrierMove;
        private readonly VerticalBrick verticalBrick;
        private readonly HorizontalBrick horizontalBrick;
        private readonly Pipe pipe;
        private readonly Fireball fireball;

        public Barriers(Hero hero)
        {
            barriers = new List<(Picture, Point)>();
            verticalBrick = new VerticalBrick();
            horizontalBrick = new HorizontalBrick();
            pipe = new Pipe();
            fireball = new Fireball();
            ChangeObstacle(hero);
        }

        public int GetLastRight()
        {
            var last = barriers.LastOrDefault();
            return last.Location.X + Drawer.GetWidth(last.Picture);
        }

        public IEnumerable<(Picture Picture, Point Location)> GetBarriers()
        {
            foreach (var e in barriers)
                yield return e;
        }

        public void ChangeObstacle(Hero newHero)
        {
            barriers.Clear();
            switch (newHero)
            {
                case Hero.Suit:
                    barrierMove = Game.PipeMove;
                    hero = Hero.Suit;
                    break;
                case Hero.UFO:
                    barrierMove = Game.FireballMove;
                    hero = Hero.UFO;
                    break;
                case Hero.GreenSnot:
                    barrierMove = Game.HorizontalBrickMove;
                    hero = Hero.GreenSnot;
                    break;
                case Hero.YellowSnot:
                    barrierMove = Game.VerticaLBrickMove;
                    hero = Hero.YellowSnot;
                    break;
            }
        }

        private void CreateBarriers()
        {
            var lastBarrier = barriers.LastOrDefault();
            var lastBarrierWidth = Drawer.GetWidth(lastBarrier.Picture);
            if (lastBarrier.Location.X + lastBarrierWidth < Game.Width)
            {
                if (lastBarrier.Location.X + lastBarrierWidth < Game.PlayerLeft)
                    lastBarrier.Location = new Point();
                switch (hero)
                {
                    case Hero.GreenSnot:
                        barriers.Add(horizontalBrick.CreateBarrier(lastBarrier.Location));
                        break;
                    case Hero.YellowSnot:
                        barriers.Add(verticalBrick.CreateBarrier(lastBarrier.Location));
                        break;
                    case Hero.Suit:
                        var firstPart = pipe.CreateBarrier(lastBarrier.Location);
                        barriers.Add(firstPart);
                        barriers.Add(pipe.GetSecondPart(firstPart.Item2));
                        break;
                    case Hero.UFO:
                        barriers.Add(fireball.CreateBarrier(lastBarrier.Location));
                        break;
                }
            }
        }

        private void DeleteBarriers()
        {
            while (barriers.Count != 0 && barriers[0].Location.X + Drawer.GetWidth(barriers[0].Picture) < 0)
                barriers.RemoveAt(0);
        }

        public void Move(bool create)
        {
            if (create)
                CreateBarriers();
            for (int i = 0; i < barriers.Count; i++)
                barriers[i] = (barriers[i].Picture, new Point(barriers[i].Location.X - barrierMove, barriers[i].Location.Y));
            DeleteBarriers();
            foreach (var e in barriers)
                Drawer.ToDraw.Add(e);
        }
    }
}
