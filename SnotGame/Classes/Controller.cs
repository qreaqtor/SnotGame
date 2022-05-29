using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SnotGame.Classes
{
    public static class Controller
    {
        private static Keys keyPressed;
        private static HashSet<Keys> pressedKeys = new HashSet<Keys>();
        private static readonly Keys[] possibleKeys = new[] { Keys.Space, Keys.W, Keys.A, Keys.S, Keys.D };
        private static readonly Dictionary<Keys, Hero> trainingChange = new Dictionary<Keys, Hero>
        {
            [Keys.D1] = Hero.GreenSnot,
            [Keys.D2] = Hero.YellowSnot,
            [Keys.D3] = Hero.Suit,
            [Keys.D4] = Hero.UFO
        };

        public static Keys KeyPressed => keyPressed;

        public static void OnKeyUp(Keys key)
        {
            if (IsPossible(key))
            {
                pressedKeys.Remove(key);
                keyPressed = pressedKeys.Any() ? pressedKeys.Min() : Keys.None;
            }
        }

        public static void OnKeyDown(Keys key)
        {
            if (IsPossible(key))
            {
                pressedKeys.Add(key);
                keyPressed = key;
            }
        }

        private static bool IsPossible(Keys key) => possibleKeys.Contains(key);

        public static void ResetKeyPressed()
        {
            keyPressed = Keys.None;
            pressedKeys.Clear();
        }

        public static bool CheckTrainingChange(KeyEventArgs e) => trainingChange.ContainsKey(e.KeyCode);

        public static Hero GetNewTrainingHero(KeyEventArgs e) => trainingChange[e.KeyCode];
    }
}
