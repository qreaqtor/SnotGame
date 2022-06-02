using NUnit.Framework;
using System.Windows.Forms;
using System.Drawing;

namespace SnotGame.Classes
{
    [TestFixture]
    public class Player_Should
    {
        [Test]
        public void CheckChange()
        {
            var expectedHeroes = new[] { Hero.GreenSnot, Hero.YellowSnot, Hero.Suit, Hero.UFO };
            var player = new Player(Hero.GreenSnot);
            foreach (var hero in expectedHeroes)
            {
                player.ChangeHero(hero, new Point(0, 0));
                Assert.AreEqual(hero, player.Hero);
            }
            Controller.ResetKeyPressed();
        }

        [Test]
        public void SuitCantUpIfFence()
        {
            var player = new Player(Hero.Suit);
            Controller.OnKeyDown(Keys.W);
            while (true)
            {
                var previousLocation = player.Location;
                player.Move();
                if (previousLocation == player.Location)
                    break;
            }
            Assert.AreEqual(new Point(Game.PlayerLeft, Game.RoofBottom), player.Location);
            Controller.ResetKeyPressed();
        }

        [TestCase(Hero.Suit, Keys.W)]
        [TestCase(Hero.UFO, Keys.W)]
        public void PlayerCantUpIfFence(Hero hero, Keys key)
        {
            var player = new Player(hero);
            Controller.OnKeyDown(key);
            while (true)
            {
                var previousLocation = player.Location;
                player.Move();
                if (previousLocation == player.Location)
                    break;
            }
            Assert.AreEqual(new Point(Game.PlayerLeft, Game.RoofBottom), player.Location);
            Controller.ResetKeyPressed();
        }


        [TestCase(Hero.GreenSnot, Keys.None)]
        [TestCase(Hero.YellowSnot, Keys.None)]
        [TestCase(Hero.Suit, Keys.None)]
        [TestCase(Hero.UFO, Keys.S)]
        public void PlayerCantDownIfFence(Hero hero, Keys key)
        {
            var player = new Player(hero);
            Controller.OnKeyDown(key);
            while (true)
            {
                var previousLocation = player.Location;
                player.Move();
                if (previousLocation == player.Location)
                    break;
            }
            Assert.AreEqual(new Point(Game.PlayerLeft, Game.FloorTop - player.Height), player.Location);
            Controller.ResetKeyPressed();
        }

        [Test]
        public void UfoCantRightIfFence()
        {
            var player = new Player(Hero.UFO);
            Controller.OnKeyDown(Keys.D);
            while (true)
            {
                var previousLocation = player.Location;
                player.Move();
                if (previousLocation == player.Location)
                    break;
            }
            Assert.AreEqual(new Point(Game.Width - player.Width, Game.FloorTop - player.Height), player.Location);
            Controller.ResetKeyPressed();
        }

        [Test]
        public void UfoCantLeftIfFence()
        {
            var player = new Player(Hero.UFO);
            Controller.OnKeyDown(Keys.A);
            while (true)
            {
                var previousLocation = player.Location;
                player.Move();
                if (previousLocation == player.Location)
                    break;
            }
            Assert.AreEqual(new Point(0, Game.FloorTop - player.Height), player.Location);
            Controller.ResetKeyPressed();
        }
    }
}
