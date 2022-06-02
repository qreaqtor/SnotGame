using NUnit.Framework;

namespace SnotGame.Classes
{
    [TestFixture]
    public class Evolution_Should
    {
        [Test]
        public void Activate()
        {
            var evo = new Evolution(Hero.GreenSnot);
            evo.Activate(Game.Width);
            Assert.IsTrue(evo.Active);
        }

        [Test]
        public void CheckChange()
        {
            var evo = new Evolution(Hero.GreenSnot);
            for (var i = 0; i < 100; i++)
            { 
                var hero = evo.NewHero;
                evo.ChangeEvolution();
                Assert.IsTrue(evo.NewHero != hero);
            }
        }


        [TestCase(Hero.GreenSnot)]
        [TestCase(Hero.YellowSnot)]
        [TestCase(Hero.Suit)]
        [TestCase(Hero.UFO)]
        public void CheckMove(Hero hero)
        {
            var evo = new Evolution(hero);
            evo.Activate(Game.Width);
            var expectedMove = GetMove(hero);
            var location = evo.Location;
            evo.Move();
            Assert.AreEqual(expectedMove, location.X - evo.Location.X);
        }

        private int GetMove(Hero hero)
        {
            switch(hero)
            {
                case Hero.GreenSnot: return Game.HorizontalBrickMove;
                case Hero.YellowSnot: return Game.VerticaLBrickMove;
                case Hero.Suit: return Game.PipeMove;
                case Hero.UFO: return Game.FireballMove;
            }
            return 0;
        }
    }
}
