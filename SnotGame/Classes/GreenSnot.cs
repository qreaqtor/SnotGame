using System.Drawing;
using System.Windows.Forms;

namespace SnotGame.Classes
{
    public class GreenSnot : IPlayer
    {
        private readonly int jumpLength;
        private int jumpCount;
        private bool falling;
        private bool doubleJump;
        private bool jumping;
        private int boost;

        public GreenSnot()
        {
            jumpLength = 4;
            jumpCount = 0;
            boost = 0;
            falling = true;
            jumping = false;
            doubleJump = true;
        }

        public Point MakeMoves(bool canDown)
        {
            if (falling)
            {
                if (!canDown) 
                { 
                    doubleJump = true; 
                    boost = 0; 
                }
                else if (Controller.KeyPressed == Keys.Space && doubleJump) 
                    DoDoubleJump();
                else 
                    return new Point(0, boost += 10);
            }
            if (Controller.KeyPressed == Keys.Space)
            {
                jumping = true;
                falling = false;
            }
            if (jumpLength > jumpCount && jumping)
            {
                jumpCount++;
                return new Point(0, -35);
            }
            else if (jumpCount == jumpLength)
                jumpCount = 0;
            falling = true;
            jumping = false;
            return new Point(0, 0);
        }

        private void DoDoubleJump()
        {
            jumpCount = 0;
            doubleJump = false;
            falling = false;
            jumping = true;
            boost = 0;
        }

        public Picture MakePictureMoves()
        {
            switch(jumpCount)
            {
                case 2: 
                    return Picture.GreenSnotRight;
                case 3: 
                    return Picture.GreenSnotDown;
                case 4: 
                    return Picture.GreenSnotLeft;
                default: 
                    return Picture.GreenSnotStock;
            };
        }
    }
}
