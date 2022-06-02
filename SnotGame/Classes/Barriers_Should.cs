using NUnit.Framework;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SnotGame.Classes
{
    [TestFixture]
    public class Barriers_Should
    {
        [Test]
        public void CheckChange()
        {
            var nextHeroes = new[]
            {
                (Hero.GreenSnot, Game.HorizontalBrickMove), (Hero.YellowSnot, Game.VerticaLBrickMove),
                (Hero.Suit, Game.PipeMove), (Hero.UFO, Game.FireballMove)
            };
            var barriers = new Barriers(Hero.GreenSnot);
            foreach(var hero in nextHeroes)
            {
                barriers.ChangeObstacle(hero.Item1);
                var barriersMove = Expression.Field(Expression.Constant(barriers), "barrierMove");
                Assert.AreEqual(hero.Item2, Expression.Lambda<Func<int>>(barriersMove).Compile()());
            }
        }


        [TestCase(Hero.GreenSnot, Game.HorizontalBrickMove)]
        [TestCase(Hero.YellowSnot, Game.VerticaLBrickMove)]
        [TestCase(Hero.Suit, Game.PipeMove)]
        [TestCase(Hero.UFO, Game.FireballMove)]
        public void CheckMove(Hero hero, int expectedMove)
        {
            var barriers = new Barriers(hero);
            barriers.Move(true);
            var location = barriers.GetLastRight();
            barriers.Move(false);
            Assert.AreEqual(expectedMove, location - barriers.GetLastRight());
        }

        [Test]
        public void CreateBarriers()
        {
            var barriers = new Barriers(Hero.GreenSnot);
            barriers.Move(true);
            while (barriers.GetBarriers().Count() < 2)
                barriers.Move(true);
            Assert.AreEqual(2, barriers.GetBarriers().Count());
        }

        [Test]
        public void DeleteBarriers()
        {
            var barriers = new Barriers(Hero.GreenSnot);
            barriers.Move(true);
            while (barriers.GetBarriers().Any())
                barriers.Move(false);
            Assert.AreEqual(0, barriers.GetBarriers().Count());
        }
    }
}
