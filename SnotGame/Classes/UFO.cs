using System.Drawing;
using System.Windows.Forms;

namespace SnotGame.Classes
{
    public class UFO : IPlayer
    {
        private Keys previousPressed;

        public UFO()
        {
            previousPressed = Keys.None;
        }

        public Picture MakePictureMoves()
        {
            var newPicture = GetNewPicture();
            previousPressed = Keys.None;
            return newPicture;
        }

        private Picture GetNewPicture()
        {
            switch(previousPressed)
            {
                case Keys.W: 
                    return Picture.UfoUp;
                case Keys.S: 
                    return Picture.UfoDown;
                case Keys.A: 
                    return Picture.UfoLeft;
                case Keys.D: 
                    return Picture.UfoRight;
                default: 
                    return Picture.UfoStock;
            };
        }

        public Point MakeMoves(bool candown)
        {
            switch (Controller.KeyPressed)
            {
                case Keys.W:
                    previousPressed = Keys.W;
                    return new Point(0, -50);
                case Keys.S:
                    previousPressed = Keys.S;
                    return new Point(0, 50);
                case Keys.A:
                    previousPressed = Keys.A;
                    return new Point(-60, 0);
                case Keys.D:
                    previousPressed = Keys.D;
                    return new Point(60, 0);
                default:
                    return new Point(0, 0);
            }
        }
    }
}
