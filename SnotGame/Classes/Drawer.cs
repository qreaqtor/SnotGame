using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SnotGame.Classes
{
    public static class Drawer
    {
        public static HashSet<(Picture, Point)> ToDraw = new HashSet<(Picture, Point)>();
        private static HashSet<(Picture, Point)> draw = new HashSet<(Picture, Point)>();
        public static readonly Image Backgorund = Image.FromFile(@"..\..\images\background.png");
        private static readonly Dictionary<Picture, Image> Pictures = new Dictionary<Picture, Image>
        {
            [Picture.GreenSnotStock] = Image.FromFile(@"..\..\images\greenSnot.png"),
            [Picture.GreenSnotRight] = Image.FromFile(@"..\..\images\greenSnot1.png"),
            [Picture.GreenSnotDown] = Image.FromFile(@"..\..\images\greenSnot2.png"),
            [Picture.GreenSnotLeft] = Image.FromFile(@"..\..\images\greenSnot3.png"),
            [Picture.YellowSnotStock] = Image.FromFile(@"..\..\images\yellowSnot.png"),
            [Picture.YellowSnotRight] = Image.FromFile(@"..\..\images\yellowSnot1.png"),
            [Picture.YellowSnotDown] = Image.FromFile(@"..\..\images\yellowSnot2.png"),
            [Picture.YellowSnotLeft] = Image.FromFile(@"..\..\images\yellowSnot3.png"),
            [Picture.SuitStock] = Image.FromFile(@"..\..\images\suit.png"),
            [Picture.Lights] = Image.FromFile(@"..\..\images\lights.png"),
            [Picture.UfoStock] = Image.FromFile(@"..\..\images\ufo.png"),
            [Picture.UfoDown] = Image.FromFile(@"..\..\images\ufo2.png"),
            [Picture.UfoUp] = Image.FromFile(@"..\..\images\ufo1.png"),
            [Picture.UfoLeft] = Image.FromFile(@"..\..\images\ufo3.png"),
            [Picture.UfoRight] = Image.FromFile(@"..\..\images\ufo4.png"),
            [Picture.Brick] = Image.FromFile(@"..\..\images\brick.png"),
            [Picture.BrickVertical2] = Image.FromFile(@"..\..\images\brickVertical2.png"),
            [Picture.BrickVertical3] = Image.FromFile(@"..\..\images\brickVertical3.png"),
            [Picture.BrickVertical4] = Image.FromFile(@"..\..\images\brickVertical4.png"),
            [Picture.BrickVertical5] = Image.FromFile(@"..\..\images\brickVertical5.png"),
            [Picture.BrickVertical6] = Image.FromFile(@"..\..\images\brickVertical6.png"),
            [Picture.BrickHorizontal2] = Image.FromFile(@"..\..\images\brickHorizontal2.png"),
            [Picture.BrickHorizontal3] = Image.FromFile(@"..\..\images\brickHorizontal3.png"),
            [Picture.BrickHorizontal4] = Image.FromFile(@"..\..\images\brickHorizontal4.png"),
            [Picture.Pipe1] = Image.FromFile(@"..\..\images\pipe.png"),
            [Picture.Pipe2] = Image.FromFile(@"..\..\images\pipe2.png"),
            [Picture.Pipe3] = Image.FromFile(@"..\..\images\pipe3.png"),
            [Picture.Pipe4] = Image.FromFile(@"..\..\images\pipe4.png"),
            [Picture.Pipe5] = Image.FromFile(@"..\..\images\pipe5.png"),
            [Picture.Pipe6] = Image.FromFile(@"..\..\images\pipe6.png"),
            [Picture.Pipe7] = Image.FromFile(@"..\..\images\pipe7.png"),
            [Picture.Pipe8] = Image.FromFile(@"..\..\images\pipe8.png"),
            [Picture.Fireball1] = Image.FromFile(@"..\..\images\fireball.png"),
            [Picture.Fireball2] = Image.FromFile(@"..\..\images\fireball2.png"),
            [Picture.Fireball3] = Image.FromFile(@"..\..\images\fireball3.png"),
            [Picture.Fence] = Image.FromFile(@"..\..\images\fence.png"),
            [Picture.Evo1] = Image.FromFile(@"..\..\images\evo1.png"),
            [Picture.Evo2] = Image.FromFile(@"..\..\images\evo2.png"),
            [Picture.Evo3] = Image.FromFile(@"..\..\images\evo3.png"),
            [Picture.Evo4] = Image.FromFile(@"..\..\images\evo4.png"),
        };

        public static int GetWidth(Picture picture) => Pictures[picture].Width;

        public static int GetHeight(Picture picture) => Pictures[picture].Height;

        public static Size GetSize(Picture picture) => Pictures[picture].Size;

        private static readonly Dictionary<Hero, string> hints = new Dictionary<Hero, string>
        {
            [Hero.GreenSnot] = "Spase - прыжок\nДвойное нажатие - двойной прыжок",
            [Hero.YellowSnot] = "Space - прыжок\nУдержать - вернуться обратно",
            [Hero.Suit] = "W - вверх",
            [Hero.UFO] = "W - вверх\nA - влево\nS - вниз\nD - вправо"
        };
        private static readonly string trainingMessage =
            "Обучение - это режим в котором\n" +
            "ты можешь тренироваться игре за каждого персонажа.\n\n" +
            "Используй кнопки 1, 2, 3, 4\n" +
            "для переключения между персонажами.\n\n" +
            "Enter - пауза, во время которой в режиме\n" +
            "обучения появляются подсказки по управлению.";

        public static readonly FontFamily scoreFontFamily = FontFamily.Families[164];
        private static readonly Font scoreFont = new Font(scoreFontFamily, 50, FontStyle.Italic);
        private static readonly Font scoreStepFont = new Font(scoreFontFamily, 25, FontStyle.Italic);
        private static readonly Brush scoreBrush = Brushes.DarkGoldenrod;

        private static readonly Font endScoreFont = new Font(scoreFontFamily, 150, FontStyle.Italic);
        private static readonly Brush endBrush = Brushes.Gold;

        private static readonly Font firstRecordFont = new Font(scoreFontFamily, 240, FontStyle.Italic, GraphicsUnit.Pixel);
        private static readonly Font secondRecordFont = new Font(scoreFontFamily, 160, FontStyle.Italic, GraphicsUnit.Pixel);
        private static readonly Font thirdRecordFont = new Font(scoreFontFamily, 80, FontStyle.Italic, GraphicsUnit.Pixel);

        private static readonly Font newRecordFont = new Font(FontFamily.Families[189], 50, FontStyle.Bold);
        private static readonly Point newRecordLocation = new Point((int)(Game.Width - 13 * newRecordFont.Size) / 2, 100);

        private static readonly Font hintsFont = new Font(FontFamily.Families[189], 30, FontStyle.Bold);
        private static readonly Brush hintsBrush = Brushes.LightSeaGreen;
        private static readonly int hintsBottom = 400;

        public static void DrawMap(PaintEventArgs e)
        {
            if (ToDraw.Count != 0)
                draw = new HashSet<(Picture, Point)>(ToDraw);
            foreach (var entity in draw)
            {
                if (entity.Item2.X < Game.Width)
                {
                    var image = Pictures[entity.Item1];
                    e.Graphics.DrawImage(image, new Rectangle(entity.Item2, image.Size));
                }
            }
            ToDraw.Clear();
        }

        public static void Clear() => draw.RemoveWhere(n => n.Item1 != Picture.Fence);

        public static void DrawScore(PaintEventArgs e, string score, string scoreStep)
        {
            e.Graphics.DrawString(score, scoreFont, scoreBrush, new Point(Game.Width - score.Length * (int)scoreFont.Size - 30, 70));
            e.Graphics.DrawString("X" + scoreStep, scoreStepFont, scoreBrush, new Point(Game.Width - scoreStep.Length * (int)scoreFont.Size - 30, 140));
        }

        public static void DrawEndScore(PaintEventArgs e, string score, bool newRecord)
        {
            var width = (int)(score.Length * endScoreFont.Size + endScoreFont.Size * Math.Cos(1.2));
            var location = new Point((Game.Width - width) / 2, 250);
            e.Graphics.FillRectangle(endBrush, new Rectangle(new Point(location.X - 12, location.Y - 12), new Size(width + 24, endScoreFont.Height + 24)));
            e.Graphics.FillRectangle(Brushes.Black, new Rectangle(location, new Size(width, endScoreFont.Height)));
            e.Graphics.DrawString(score, endScoreFont, endBrush, location);
            if (newRecord)
            {
                var newRecordWidth = 13 * (int)newRecordFont.Size;
                e.Graphics.FillRectangle(hintsBrush, new Rectangle(new Point(newRecordLocation.X - 10, newRecordLocation.Y - 10), new Size(newRecordWidth + 20, newRecordFont.Height + 20)));
                e.Graphics.FillRectangle(Brushes.Black, new Rectangle(newRecordLocation, new Size(newRecordWidth, newRecordFont.Height)));
                e.Graphics.DrawString(" НОВЫЙ РЕКОРД!", newRecordFont, hintsBrush, newRecordLocation);
            }
        }

        public static void DrawRecords(PaintEventArgs e, string[] records)
        {
            var firstRecord = records[0];
            var secondRecord = records[1];
            var thirdRecord = records[2];
            var firstWidth = (int)((firstRecord.Length + Math.Cos(0.5)) * firstRecordFont.Size) / 2;
            var secondWidth = (int)((secondRecord.Length + Math.Cos(0.5)) * secondRecordFont.Size) / 2;
            var thirdWidth = (int)((thirdRecord.Length + Math.Cos(0.5)) * thirdRecordFont.Size) / 2;
            var firstRecordLocation = new Point((Game.Width - firstWidth) / 2, 150);
            var secondRecordLocation = new Point((Game.Width - secondWidth) / 2, firstRecordLocation.Y + firstRecordFont.Height + 30);
            var thirdRecordLocation = new Point((Game.Width - thirdWidth) / 2, secondRecordLocation.Y + secondRecordFont.Height + 30);
            var height = thirdRecordLocation.Y + thirdRecordFont.Height - firstRecordLocation.Y + 20;
            e.Graphics.FillRectangle(endBrush, new Rectangle(new Point(firstRecordLocation.X - 10, firstRecordLocation.Y - 10), new Size(firstWidth + 20, height + 20)));
            e.Graphics.FillRectangle(Brushes.Black, new Rectangle(firstRecordLocation, new Size(firstWidth, height)));
            e.Graphics.DrawString(firstRecord, firstRecordFont, endBrush, firstRecordLocation);
            e.Graphics.DrawString(secondRecord, secondRecordFont, endBrush, secondRecordLocation);
            e.Graphics.DrawString(thirdRecord, thirdRecordFont, endBrush, thirdRecordLocation);
        }

        public static void DrawHints(PaintEventArgs e, Hero hero)
        {
            var hint = hints[hero];
            var text = hint.Split('\n');
            var width = text.Max().Count() * (int)(hintsFont.Size - 3);
            var height = text.Length * hintsFont.Height;
            var hintsRight = (Game.Width - width) / 2;
            e.Graphics.FillRectangle(hintsBrush, new Rectangle(new Point(hintsRight - 30, hintsBottom - height - 30), new Size(width + 60, height + 60)));
            e.Graphics.FillRectangle(Brushes.Black, new Rectangle(new Point(hintsRight - 20, hintsBottom - height - 20), new Size(width + 40, height + 40)));
            e.Graphics.DrawString(hint, hintsFont, hintsBrush, new Point(hintsRight, hintsBottom - height));
        }

        public static void DrawTrainingMessage(PaintEventArgs e)
        {
            var s = trainingMessage.Split('\n');
            var messageWidth = s.Max().Length * (int)hintsFont.Size - 300;
            var messageHeight = s.Count() * hintsFont.Height;
            var messageLocation = new Point((Game.Width - messageWidth) / 2, 200);
            e.Graphics.FillRectangle(hintsBrush, new Rectangle(new Point(messageLocation.X - 30, messageLocation.Y - 30), new Size(messageWidth + 60, messageHeight + 60)));
            e.Graphics.FillRectangle(Brushes.Black, new Rectangle(new Point(messageLocation.X - 20, messageLocation.Y - 20), new Size(messageWidth + 40, messageHeight + 40)));
            e.Graphics.DrawString(trainingMessage, hintsFont, hintsBrush, messageLocation);
        }
    }
}
