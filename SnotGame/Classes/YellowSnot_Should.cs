using NUnit.Framework;
using System.Windows.Forms;
using System.Drawing;

namespace SnotGame.Classes
{
    [TestFixture]
    public class YellowSnot_Should
    {
        [Test]
        public void JumpUp()
        {
            var length = 9;
            var yellowSnot = new YellowSnot();
            var boost = -20;
            var boostStep = -20;
            Controller.OnKeyDown(Keys.Space);
            yellowSnot.MakeMoves(false);
            boost += boostStep;
            Controller.OnKeyUp(Keys.Space);
            for(int i=1; i<length; i++)
            {
                var move = yellowSnot.MakeMoves(i < length);
                Assert.AreEqual(new Point(0, boost += boostStep), move);
            }
            Controller.ResetKeyPressed();
        }

        [Test]
        public void Jumpdown()
        {
            var length = 9;
            var yellowSnot = new YellowSnot();
            var boost = 20;
            var boostStep = 20;
            Controller.OnKeyDown(Keys.Space);
            yellowSnot.MakeMoves(true);
            boost += boostStep;
            Controller.OnKeyUp(Keys.Space);
            for (int i = 1; i < length; i++)
            {
                var move = yellowSnot.MakeMoves(i < length);
                Assert.AreEqual(new Point(0, boost += boostStep), move);
            }
            Controller.ResetKeyPressed();
        }
    }
}
