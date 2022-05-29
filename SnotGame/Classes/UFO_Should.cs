using NUnit.Framework;
using System.Windows.Forms;
using System.Drawing;

namespace SnotGame.Classes
{
    [TestFixture]
    public class UFO_Should
    {
        [Test]
        public void MoveUp()
        {
            var ufo = new UFO();
            Controller.OnKeyDown(Keys.W);
            Assert.AreEqual(new Point(0, -50), ufo.MakeMoves(true));
        }

        [Test]
        public void MoveDown()
        {
            var ufo = new UFO();
            Controller.OnKeyDown(Keys.S);
            Assert.AreEqual(new Point(0, 50), ufo.MakeMoves(true));
            Controller.OnKeyUp(Keys.S);
        }

        [Test]
        public void MoveRight()
        {
            var ufo = new UFO();
            Controller.OnKeyDown(Keys.D);
            Assert.AreEqual(new Point(60, 0), ufo.MakeMoves(true));
            Controller.OnKeyUp(Keys.S);
        }

        [Test]
        public void MoveLeft()
        {
            var ufo = new UFO();
            Controller.OnKeyDown(Keys.A);
            Assert.AreEqual(new Point(-60, 0), ufo.MakeMoves(true));
            Controller.OnKeyUp(Keys.S);
        }
    }
}
