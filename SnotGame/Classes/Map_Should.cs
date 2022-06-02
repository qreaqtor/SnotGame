using NUnit.Framework;
using System.Linq.Expressions;
using System;

namespace SnotGame.Classes
{
    [TestFixture]
    public class Map_Should
    {
        [Test]
        public void ChangeMap()
        {
            var map = new Map();
            var evo = Expression.Field(Expression.Constant(map), "evolution");
            var newHero = Expression.Lambda<Func<Evolution>>(evo).Compile()().NewHero;
            map.ChangeMap();
            var hero = map.Hero;
            Assert.AreEqual(newHero, hero);
        }

        [Test]
        public void GameOver()
        {
            var map = new Map();
            while (true)
            {
                map.MakeMapMoves();
                if (map.GameOver)
                    break;
            }
        }
    }
}
