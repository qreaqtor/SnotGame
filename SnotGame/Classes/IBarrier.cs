using System.Drawing;

namespace SnotGame.Classes
{
    public interface IBarrier
    {
        (Picture, Point) CreateBarrier(Point lastBarrier);
    }
}
