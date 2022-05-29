using System.Drawing;

namespace SnotGame.Classes
{
    public interface IPlayer
    {
        Point MakeMoves(bool canDown);
        Picture MakePictureMoves();
    }
}
