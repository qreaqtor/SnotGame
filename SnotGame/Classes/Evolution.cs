using System;
using System.Drawing;
using System.Linq;

namespace SnotGame.Classes
{
    public class Evolution
    {
        private readonly Hero[] evolutions = new[] { Hero.Suit, Hero.UFO, Hero.GreenSnot, Hero.YellowSnot };
        private Point location;
        private Hero newHero;
        private Picture picture;
        private int evoMove;
        private readonly int length;
        private bool active;
        private readonly Random random;

        public bool Active => active;
        public Point Location => location;
        public Size Size => Drawer.GetSize(picture);
        public Hero NewHero => newHero;

        public Evolution(Hero hero)
        {
            random = new Random();
            active = false;
            length = evolutions.Length;
            newHero = hero;
            ChangeEvolution();
            SetPicture();
        }

        private Hero GetNewHero(Hero hero) => evolutions.Where(n => n != hero).ToArray()[random.Next(0, length - 1)];

        private void SetPicture()
        {
            switch (newHero)
            {
                case Hero.Suit: picture = Picture.Evo1; break;
                case Hero.UFO: picture = Picture.Evo2; break;
                case Hero.GreenSnot: picture = Picture.Evo3; break;
                case Hero.YellowSnot: picture = Picture.Evo4; break;
            }
        }

        public void SetNewEvolution(Hero hero)
        {
            newHero = GetNewHero(hero);
            SetPicture();
        }

        public void Move()
        {
            if (location.X + Size.Width > 0 && active)
            {
                location.X -= evoMove;
                Drawer.ToDraw.Add((picture, location));
            }
            else active = false;
        }

        public void Activate(int lastBarrierRight)
        {
            active = true;
            location = new Point(lastBarrierRight + Game.EvolutionDistanceAfterBarrier, Game.FloorTop - Drawer.GetHeight(picture));
        }

        public void ChangeEvolution()
        {
            switch (newHero)
            {
                case Hero.Suit: evoMove = Game.PipeMove; break;
                case Hero.UFO: evoMove = Game.FireballMove; break;
                case Hero.YellowSnot: evoMove = Game.VerticaLBrickMove; break;
                case Hero.GreenSnot: evoMove = Game.HorizontalBrickMove; break;
            }
            newHero = GetNewHero(newHero);
            SetPicture();
            active = false;
        }
    }
}
