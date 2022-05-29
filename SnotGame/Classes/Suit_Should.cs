using NUnit.Framework;
using System.Windows.Forms;
using System.Drawing;

namespace SnotGame.Classes
{
    [TestFixture]
    public class Suit_Should
    {
        [Test]
        public void Fly()
        {
            Controller.OnKeyDown(Keys.W);
            var suit = new Suit();
            Assert.AreEqual(new Point(0, -10), suit.MakeMoves(false));
            Controller.ResetKeyPressed();
        }

        [Test]
        public void FallWithBoostAfterFly()
        {
            Controller.OnKeyDown(Keys.W);
            var suit = new Suit();
            var boost = 0;
            for(int i=0; i<4; i++)
                Assert.AreEqual(new Point(0, -10), suit.MakeMoves(false));
            Controller.OnKeyUp(Keys.W);
            for (int i = 0; i < 4; i++)
                Assert.AreEqual(new Point(0, boost += 4), suit.MakeMoves(true));
        }
    }
}
