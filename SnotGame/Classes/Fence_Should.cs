using NUnit.Framework;
using System.Drawing;
using System.Linq.Expressions;
using System;
using System.Collections.Generic;

namespace SnotGame.Classes
{
    [TestFixture]
    public class Fence_Should
    {
        [Test]
        public void CheckChange()
        {
            var nextHeroes = new[]
{
                (Hero.GreenSnot, Game.HorizontalBrickMove), (Hero.YellowSnot, Game.VerticaLBrickMove),
                (Hero.Suit, Game.PipeMove), (Hero.UFO, Game.FireballMove)
            };
            var fence = new Fence(Hero.GreenSnot);
            foreach(var hero in nextHeroes)
            {
                fence.ChangeFence(hero.Item1);
                var fenceMove = Expression.Field(Expression.Constant(fence), "fenceMove");
                Assert.AreEqual(hero.Item2, Expression.Lambda<Func<int>>(fenceMove).Compile()());
            }
        }

        [TestCase(Hero.GreenSnot, Game.HorizontalBrickMove)]
        [TestCase(Hero.YellowSnot, Game.VerticaLBrickMove)]
        [TestCase(Hero.Suit, Game.PipeMove)]
        [TestCase(Hero.UFO, Game.FireballMove)]
        public void CheckRoofMove(Hero hero, int move)
        {
            var fence = new Fence(hero);
            var roof = Expression.Field(Expression.Constant(fence), "roof");
            var location = Expression.Lambda<Func<List<Point>>>(roof).Compile()();
            fence.Move();
            var newLocation = Expression.Lambda<Func<List<Point>>>(roof).Compile()();
            for (var i = 0; i < newLocation.Count; i++)
                newLocation[i] = new Point(newLocation[i].X - move, newLocation[i].Y);
            Assert.AreEqual(location, newLocation);
        }
    }
}
