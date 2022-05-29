using System.Drawing;
using System.Windows.Forms;

namespace SnotGame.Classes
{
    public class Suit : IPlayer
    {
        private bool falling;
        private int boost;

        public Suit()
        {
            falling = true;
            boost = 0;
        }

        public Point MakeMoves(bool canDown)
        {
            if (falling && Controller.KeyPressed != Keys.W && canDown)
                return new Point(0, boost += 4);
            if (Controller.KeyPressed == Keys.W)
            {
                falling = true;
                boost = 0;
                return new Point(0, -10);
            }
            if (!canDown)
                boost = 0;
            return new Point(0, 0);
        }

        public Picture MakePictureMoves()
        {
            if (Controller.KeyPressed == Keys.W)
                return Picture.Lights;
            return Picture.SuitStock;
        }
    }
}
