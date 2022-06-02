using System.Drawing;

namespace SnotGame.Classes
{
    public class Player
    {
        private Hero hero;
        private Picture picture;
        private Point location;
        private readonly GreenSnot greenSnot;
        private readonly YellowSnot yellowSnot;
        private readonly Suit suit;
        private readonly UFO ufo;

        public Player(Hero hero)
        {
            this.hero = hero;
            greenSnot = new GreenSnot();
            yellowSnot = new YellowSnot();
            suit = new Suit();
            ufo = new UFO();
            picture = MakePictureMoves();
            location = new Point(Game.PlayerLeft, Game.FloorTop - Height);
        }

        public int Height => Drawer.GetHeight(picture);
        public int Width => Drawer.GetWidth(picture);
        public Point Location => location;
        public Size Size => new Size(Width, Height);
        public Hero Hero => hero;

        private Point MakeMoves(bool canDown)
        {
            switch(hero)
            {
                case Hero.GreenSnot: 
                    return greenSnot.MakeMoves(canDown);
                case Hero.YellowSnot: 
                    return yellowSnot.MakeMoves(canDown);
                case Hero.Suit: 
                    return suit.MakeMoves(canDown);
                case Hero.UFO: 
                    return ufo.MakeMoves(canDown);
                default:
                    return new Point(0, 0);
            };
        }

        private Picture MakePictureMoves()
        {
            switch(hero)
            {
                case Hero.GreenSnot: 
                    return greenSnot.MakePictureMoves();
                case Hero.YellowSnot: 
                    return yellowSnot.MakePictureMoves();
                case Hero.Suit: 
                    return suit.MakePictureMoves();
                case Hero.UFO: 
                    return ufo.MakePictureMoves();
                default: 
                    return picture;
            };
        }

        public void ChangeHero(Hero newHero, Point newLocation)
        {
            location = newLocation;
            switch (newHero)
            {
                case Hero.Suit:
                    hero = Hero.Suit;
                    picture = suit.MakePictureMoves();
                    break;
                case Hero.UFO:
                    hero = Hero.UFO;
                    picture = ufo.MakePictureMoves();
                    break;
                case Hero.GreenSnot:
                    hero = Hero.GreenSnot;
                    picture = greenSnot.MakePictureMoves();
                    break;
                case Hero.YellowSnot:
                    hero = Hero.YellowSnot;
                    picture = yellowSnot.MakePictureMoves();
                    break;
            }
        }

        public void Move()
        {
            var move = MakeMoves(location.Y + Height < Game.FloorTop);
            var newLocation = new Point(location.X, location.Y);
            if (newLocation.Y + move.Y > Game.RoofBottom)
            {
                if (newLocation.Y + Height + move.Y < Game.FloorTop)
                    newLocation.Y += move.Y;
                else
                    newLocation.Y = Game.FloorTop - Height;
            }
            else newLocation.Y = Game.RoofBottom;
            if (newLocation.X + move.X > 0)
            {
                if (newLocation.X + Width + move.X < Game.Width)
                    newLocation.X += move.X;
                else
                    newLocation.X = Game.Width - Width;
            }
            else newLocation.X = 0;
            location = newLocation;
            picture = MakePictureMoves();
            if (picture == Picture.Lights)
            {
                Drawer.ToDraw.Add((picture, new Point(location.X, location.Y + 47)));
                picture = Picture.SuitStock;
            }
            Drawer.ToDraw.Add((picture, location));
        }
    }
}
