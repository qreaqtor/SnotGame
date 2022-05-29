using NUnit.Framework;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnotGame.Classes
{
    public static class Game
    {
        public static int Width = Screen.PrimaryScreen.WorkingArea.Width;

        public const int PlayerLeft = 400;
        public const int RoofBottom = 40;
        public const int FloorTop = 1040;

        public const int SnotBlunder = 30;
        public const int SuitBlunder = 40;
        public const int SuitTopLeftBlunder = 121;
        public const int UfoTopBlunder = 950;
        public const int UfoBottomBlunder = 340;

        public const int VerticaLBrickMove = 33;
        public const int HorizontalBrickMove = 24;
        public const int PipeMove = 15;
        public const int FireballMove = 50;

        public const int DistanceForEvolution = 150;
        public const int EvolutionDistanceAfterBarrier = 300;

        public const int DistanceForEvolutionIfTraining = 50;

        public const Hero StartHero = Hero.GreenSnot;
    }
}
