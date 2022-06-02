using System.Drawing;
using System.Windows.Forms;

namespace SnotGame.Classes
{
    public class YellowSnot : IPlayer
    {
        private bool jump;
        private int jumpCount;
        private int boostStep;
        private int boost;
        private readonly int jumpLength;

        public YellowSnot()
        {
            jump = false;
            jumpLength = 9;
            jumpCount = 0;
            boostStep = -20;
            boost = 10;
        }

        public Point MakeMoves(bool canDown)
        {
            if (Controller.KeyPressed == Keys.Space)
            {
                if (!jump)
                {
                    if (canDown)
                        boostStep = 20;
                    else boostStep = -20;
                    boost = boostStep > 0 ? 20 : -20;
                    jump = true;
                    jumpCount = 0;
                }
            }
            if (jump && jumpCount < jumpLength)
            {
                jumpCount++;
                boost += boostStep;
                return new Point(0, boost);
            }
            jump = false;
            return new Point(0, 0);
        }

        public Picture MakePictureMoves()
        {
            if (boostStep < 0)
                return DoJumpUp();
            return DoJumpDown();
        }

        private Picture DoJumpUp()
        {
            switch(jumpCount)
            {
                case 0: 
                    return Picture.YellowSnotStock;
                case 1: 
                    return Picture.YellowSnotStock;
                case 2: 
                    return Picture.YellowSnotRight;
                case 4: 
                    return Picture.YellowSnotLeft;
                case 5: 
                    return Picture.YellowSnotStock;
                case 6: 
                    return Picture.YellowSnotRight;
                default: 
                    return Picture.YellowSnotDown;
            };
        }

        private Picture DoJumpDown()
        {
            switch(jumpCount)
            {
                case 0: 
                    return Picture.YellowSnotDown;
                case 1: 
                    return Picture.YellowSnotDown;
                case 2: 
                    return Picture.YellowSnotLeft;
                case 4: 
                    return Picture.YellowSnotRight;
                case 5: 
                    return Picture.YellowSnotDown;
                case 6: 
                    return Picture.YellowSnotLeft;
                default: 
                    return Picture.YellowSnotStock;
            };
        }
    }
}
