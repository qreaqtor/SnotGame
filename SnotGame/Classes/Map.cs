using System;
using System.Drawing;
using System.Linq;

namespace SnotGame.Classes
{
    public class Map
    {
        private readonly Player player;
        private readonly Barriers barriers;
        private readonly Fence fence;
        private readonly Evolution evolution;
        private readonly bool training;
        private readonly int distanceForEvolution;
        private int distance;
        private int score;
        private int scoreStep;
        private bool gameOver;

        public bool GameOver => gameOver;
        public int Score => distance + score;
        public int Scorestep => scoreStep;
        public Hero Hero => player.Hero;

        public Map()
        {
            var startHero = Game.StartHero;
            player = new Player(startHero);
            fence = new Fence(startHero);
            barriers = new Barriers(startHero);
            evolution = new Evolution(startHero);
            gameOver = false;
            training = false;
            scoreStep = 1;
            score = 0;
            distance = 0;
            distanceForEvolution = Game.DistanceForEvolution;
        }

        public Map(Hero hero)
        {
            player = new Player(hero);
            fence = new Fence(hero);
            barriers = new Barriers(hero);
            evolution = new Evolution(hero);
            gameOver = false;
            training = true;
            scoreStep = 1;
            score = 0;
            distance = 0;
        }

        private void CalculateScore()
        {
            score += distance;
            distance = 0;
        }

        public void ChangeMap()
        {
            var newHero = evolution.NewHero;
            player.ChangeHero(newHero, evolution.Location);
            barriers.ChangeObstacle(newHero);
            fence.ChangeFence(newHero);
            evolution.ChangeEvolution();
            CalculateScore();
            scoreStep++;
        }

        private bool PlayerDead()
        {
            var hero = player.Hero;
            var playerTopLeft = player.Location;
            var playerSize = player.Size;
            var playerLBottomRight = new Point(playerTopLeft.X + playerSize.Width, playerTopLeft.Y + playerSize.Height);
            var comparer = GetComparer(hero);
            return barriers.GetBarriers()
                .Where(n => IsNear(playerTopLeft.X, playerSize.Width, n.Location.X, Drawer.GetWidth(n.Picture)))
                .Any(n => IsCrash(playerTopLeft, playerLBottomRight, n.Location,
                    new Point(n.Location.X + Drawer.GetWidth(n.Picture), n.Location.Y + Drawer.GetHeight(n.Picture)), comparer));
        }

        Func<int, int, int, int, bool> IsNear = (playerX, playerWidth, barrierX, barrierWidth)
            => playerX + playerWidth >= barrierX && playerX <= barrierX + barrierWidth;

        Func<Point, Point, Point, Point, Func<int, bool, bool>, bool> IsCrash = (playerUpperLeft, playerLowerRight, objectUpperLeft, objectLowerRight, comparer) =>
        {
            var right = playerLowerRight.X < objectLowerRight.X ? playerLowerRight.X : objectLowerRight.X;
            var bottom = playerLowerRight.Y < objectLowerRight.Y ? playerLowerRight.Y : objectLowerRight.Y;
            var left = playerUpperLeft.X > objectUpperLeft.X ? playerUpperLeft.X : objectUpperLeft.X;
            var top = playerUpperLeft.Y > objectUpperLeft.Y ? playerUpperLeft.Y : objectUpperLeft.Y;
            var width = right - left;
            var height = bottom - top;
            if (height > 0 && width > 0)
            {
                var e = playerUpperLeft.X * playerUpperLeft.X + playerUpperLeft.Y * playerUpperLeft.Y;
                var g = objectUpperLeft.X * objectUpperLeft.X + objectUpperLeft.Y * objectUpperLeft.Y;
                return comparer(height * width, e >= g);
            }
            return false;
        };

        Func<Hero, Func<int, bool, bool>> GetComparer = hero =>
        {
            switch (hero)
            {
                case Hero.GreenSnot: return (square, edge) => square >= Game.SnotBlunder;
                case Hero.YellowSnot: return (square, edge) => square >= Game.SnotBlunder;
                case Hero.Suit:
                    return (square, edge) => 
                    {
                        if (edge)
                            return square >= Game.SuitTopLeftBlunder;
                        return square >= Game.SuitBlunder;
                    };
                case Hero.UFO:
                    return (square, edge) =>
                    {
                        if (edge)
                            return square >= Game.UfoTopBlunder;
                        return square >= Game.UfoBottomBlunder;
                    };
                default: return (square, edge) => square > 0;
            };
        };

        public void MakeMapMoves()
        {
            if (!training && !evolution.Active && distance != 0 && distance >= distanceForEvolution * scoreStep)
                evolution.Activate(barriers.GetLastRight());
            player.Move();
            fence.Move();
            barriers.Move(!evolution.Active || evolution.Location.X + evolution.Size.Width < player.Location.X && player.Hero != Hero.UFO);
            if (evolution.Active)
                MoveEvolution();
            if (PlayerDead())
            {
                CalculateScore();
                gameOver = true;
                return;
            }
            distance += scoreStep;
        }

        private void MoveEvolution()
        {
            evolution.Move();
            var playerTopLeft = player.Location;
            var playerSize = player.Size;
            var playerLBottomRight = new Point(playerTopLeft.X + playerSize.Width, playerTopLeft.Y + playerSize.Height);
            var stuffTopLeft = evolution.Location;
            var stuffSize = evolution.Size;
            var stuffBottomRight = new Point(stuffTopLeft.X + stuffSize.Width, stuffTopLeft.Y + stuffSize.Height);
            if (IsCrash(playerTopLeft, playerLBottomRight, stuffTopLeft, stuffBottomRight, GetComparer(player.Hero)))
                ChangeMap();
            else if (!evolution.Active)
            {
                evolution.SetNewEvolution(player.Hero);
                CalculateScore();
            }
        }
    }
}
