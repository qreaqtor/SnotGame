using NUnit.Framework;
using System.Windows.Forms;
using System.Drawing;

namespace SnotGame.Classes
{
    [TestFixture]
    public class GreenSnot_Should
    {
        [Test]
        public void Jump()
        {
            var jumpLength = 4;
            Controller.OnKeyDown(Keys.Space);
            var greenSnot = new GreenSnot();
            for (var i = 0; i < jumpLength; i++)
            {
                var move = greenSnot.MakeMoves(i == 0);
                Assert.AreEqual(new Point(0, -35), move);
            }
            Controller.ResetKeyPressed();
        }

        [Test]
        public void DoubleJump()
        {
            var jumpLength = 4;
            Controller.OnKeyDown(Keys.Space);
            var greenSnot = new GreenSnot();
            Assert.AreEqual(new Point(0, -35), greenSnot.MakeMoves(false));
            for (var i = 1; i < jumpLength; i++)
            {
                var move = greenSnot.MakeMoves(true);
                Assert.AreEqual(new Point(0, -35), move);
            }
            Assert.AreEqual(new Point(0, 0), greenSnot.MakeMoves(true));
            for (var i = 0; i < jumpLength; i++)
            {
                var move = greenSnot.MakeMoves(true);
                Assert.AreEqual(new Point(0, -35), move);
            }
            Controller.ResetKeyPressed();
        }

        [Test]
        public void FallWithBoostAfterJump()
        {
            var jumpLength = 4;
            Controller.OnKeyDown(Keys.Space);
            var greenSnot = new GreenSnot();
            var boost = 0;
            for (var i = 0; i < jumpLength; i++)
            {
                var move = greenSnot.MakeMoves(i == 0);
                Assert.AreEqual(new Point(0, -35), move);
            }
            Controller.OnKeyUp(Keys.Space);
            Assert.AreEqual(new Point(0, 0), greenSnot.MakeMoves(true));
            for (int i =0; i<7; i++)
            {
                var move = greenSnot.MakeMoves(true);
                Assert.AreEqual(new Point(0, boost += 10), move);
            }
        }
    }
}
