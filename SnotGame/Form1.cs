using SnotGame.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnotGame
{
    public partial class Form1 : Form
    {
        private readonly FontFamily buttonFontFamily = FontFamily.Families[189];

        private readonly Button play;
        private readonly Button learning;
        private readonly Button records;
        private readonly Button exit;
        private readonly Button resume;
        private readonly Button back;
        private readonly Button playAgain;

        private bool training;
        private bool newRecord;
        private bool drawHints;
        private bool drawTrainingMessage;
        private bool drawRecords;
        private bool drawScore;
        private bool drawEndScore;
        private bool isActive;

        private readonly Timer timer;
        private Map map;

        public Form1()
        {
            map = new Map();
            timer = new Timer { Interval = 70 };
            timer.Tick += new EventHandler(MoveObjects);
            WindowState = FormWindowState.Maximized;
            FormBorderStyle = FormBorderStyle.None;
            DoubleBuffered = true;
            BackgroundImage = Drawer.Backgorund;

            play = new Button();
            play.Text = "ИГРАТЬ";
            play.Click += new EventHandler(PlayClick);
            play.Font = new Font(buttonFontFamily, 90, FontStyle.Bold, GraphicsUnit.Pixel);
            play.AutoSize = true;
            play.TextAlign = ContentAlignment.MiddleCenter;
            play.ForeColor = Color.Gray;
            play.BackColor = Color.Black;
            play.FlatAppearance.BorderSize = 8;
            play.FlatStyle = FlatStyle.Flat;
            play.GotFocus += (s, e) => { play.ForeColor = Color.DarkGoldenrod; };
            play.LostFocus += (s, e) => { play.ForeColor = Color.Gray; };
            Controls.Add(play);
            play.Location = new Point((Game.Width - play.Width) / 2, 350);

            learning = new Button();
            learning.Text = "ОБУЧЕНИЕ";
            learning.Click += new EventHandler(TrainingClick);
            learning.Font = new Font(buttonFontFamily, 50, FontStyle.Bold, GraphicsUnit.Pixel);
            learning.AutoSize = true;
            learning.TextAlign = ContentAlignment.MiddleCenter;
            learning.ForeColor = Color.Gray;
            learning.BackColor = Color.Black;
            learning.FlatAppearance.BorderSize = 5;
            learning.FlatStyle = FlatStyle.Flat;
            learning.GotFocus += (s, e) => { learning.ForeColor = Color.DarkGoldenrod; };
            learning.LostFocus += (s, e) => { learning.ForeColor = Color.Gray; };
            Controls.Add(learning);
            learning.Location = new Point(play.Location.X + (play.Width - learning.Width) / 2, play.Location.Y + play.Height + 20);

            records = new Button();
            records.Text = "РЕКОРДЫ";
            records.Click += new EventHandler(RecordsClick);
            records.Font = new Font(buttonFontFamily, 50, FontStyle.Bold, GraphicsUnit.Pixel);
            records.AutoSize = true;
            records.TextAlign = ContentAlignment.MiddleCenter;
            records.ForeColor = Color.Gray;
            records.BackColor = Color.Black;
            records.FlatAppearance.BorderSize = 5;
            records.FlatStyle = FlatStyle.Flat;
            records.GotFocus += (s, e) => { records.ForeColor = Color.DarkGoldenrod; };
            records.LostFocus += (s, e) => { records.ForeColor = Color.Gray; };
            Controls.Add(records);
            records.Location = new Point(learning.Location.X + (learning.Width - records.Width) / 2, learning.Location.Y + learning.Height + 20);

            exit = new Button();
            exit.Text = "ВЫЙТИ";
            exit.Click += new EventHandler(ExitClick);
            exit.Font = new Font(buttonFontFamily, 50, FontStyle.Bold, GraphicsUnit.Pixel);
            exit.AutoSize = true;
            exit.TextAlign = ContentAlignment.MiddleCenter;
            exit.ForeColor = Color.Gray;
            exit.BackColor = Color.Black;
            exit.FlatAppearance.BorderSize = 5;
            exit.FlatStyle = FlatStyle.Flat;
            exit.GotFocus += (s, e) => { exit.ForeColor = Color.DarkGoldenrod; };
            exit.LostFocus += (s, e) => { exit.ForeColor = Color.Gray; };
            Controls.Add(exit);
            exit.Location = new Point(records.Location.X + (records.Width - exit.Width) / 2, records.Location.Y + records.Height + 20);

            resume = new Button();
            resume.Text = "ПРОДОЛЖИТЬ";
            resume.Click += new EventHandler(ResumeClick);
            resume.Font = new Font(buttonFontFamily, 50, FontStyle.Bold, GraphicsUnit.Pixel);
            resume.AutoSize = true;
            resume.TextAlign = ContentAlignment.MiddleCenter;
            resume.ForeColor = Color.Gray;
            resume.BackColor = Color.Black;
            resume.FlatAppearance.BorderSize = 5;
            resume.FlatStyle = FlatStyle.Flat;
            resume.GotFocus += (s, e) => { resume.ForeColor = Color.DarkGoldenrod; };
            resume.LostFocus += (s, e) => { resume.ForeColor = Color.Gray; };

            back = new Button();
            back.Text = "НАЗАД";
            back.Click += new EventHandler(BackClick);
            back.Font = new Font(buttonFontFamily, 50, FontStyle.Bold, GraphicsUnit.Pixel);
            back.AutoSize = true;
            back.TextAlign = ContentAlignment.MiddleCenter;
            back.ForeColor = Color.Gray;
            back.BackColor = Color.Black;
            back.FlatAppearance.BorderSize = 5;
            back.FlatStyle = FlatStyle.Flat;
            back.GotFocus += (s, e) => { back.ForeColor = Color.DarkGoldenrod; };
            back.LostFocus += (s, e) => { back.ForeColor = Color.Gray; };

            playAgain = new Button();
            playAgain.Text = "ИГРАТЬ СНОВА";
            playAgain.Click += new EventHandler(PlayAgainClick);
            playAgain.Font = new Font(buttonFontFamily, 50, FontStyle.Bold, GraphicsUnit.Pixel);
            playAgain.AutoSize = true;
            playAgain.TextAlign = ContentAlignment.MiddleCenter;
            playAgain.ForeColor = Color.Gray;
            playAgain.BackColor = Color.Black;
            playAgain.FlatAppearance.BorderSize = 5;
            playAgain.FlatStyle = FlatStyle.Flat;
            Controls.Add(playAgain);
            playAgain.Location = new Point((Game.Width - playAgain.Width) / 2, 500);
            playAgain.GotFocus += (s, e) => { playAgain.ForeColor = Color.DarkGoldenrod; };
            playAgain.LostFocus += (s, e) => { playAgain.ForeColor = Color.Gray; };

            Cursor.Hide();
            Controls.Remove(playAgain);
            InitializeComponent();
        }

        private void MoveObjects(object sender, EventArgs e)
        {
            map.MakeMapMoves();
            if (map.GameOver)
            {
                if (!training)
                {
                    CheckRecords();
                    drawScore = false;
                    drawEndScore = true;
                }
                timer.Stop();
                ShowEndMenu();
                Controller.ResetKeyPressed();
            }
            MakeInvalidate();
        }

        private void MakeInvalidate() => Invalidate();

        private void CheckRecords()
        {
            var streamReader = new StreamReader(@"..\..\Records.txt");
            var rec = streamReader.ReadToEnd();
            var records = rec.Split('\r')
                .Where(n => n != "\n")
                .Select(int.Parse).ToList();
            streamReader.Close();
            records.Add(map.Score);
            records.Sort();
            records.Reverse();
            if (map.Score != records.LastOrDefault())
                newRecord = true;
            else newRecord = false;
            File.Delete(@"..\..\Records.txt");
            File.Create(@"..\..\Records.txt").Close();
            var streamWriter = new StreamWriter(@"..\..\Records.txt");
            foreach (var s in records.Take(3))
                streamWriter.WriteLine(s);
            streamWriter.Close();
        }

        private void PlayClick(object sender, EventArgs e)
        {
            timer.Start();
            drawScore = true;
            drawEndScore = false;
            training = false;
            training = false;
            isActive = true;
            map = new Map();
            HideStartMenu();
        }

        private void PlayAgainClick(object sender, EventArgs e)
        {
            if (training)
            {
                map = new Map(Game.StartHero);
                drawScore = false;
            }
            else
            {
                map = new Map();
                drawScore = true;
            }
            timer.Start();
            drawEndScore = false;
            isActive = true;
            HideEndMenu();
        }

        private void ExitClick(object sender, EventArgs e)
        {
            Close();
        }

        private void TrainingClick(object sender, EventArgs e)
        {
            drawScore = false;
            drawEndScore = false;
            training = true;
            isActive = true;
            drawTrainingMessage = true;
            map = new Map(Game.StartHero);
            HideStartMenu();
            Controls.Add(resume);
            resume.Focus();
            resume.Location = new Point((Game.Width - resume.Width) / 2, 700);
            MakeInvalidate();
        }

        private void RecordsClick(object sender, EventArgs e)
        {
            HideStartMenu();
            drawRecords = true;
            Controls.Add(back);
            back.Focus();
            back.Location = new Point((Game.Width - back.Width) / 2, 800);
            MakeInvalidate();
        }

        private void ResumeClick(object sender, EventArgs e)
        {
            timer.Start();
            isActive = true;
            drawHints = false;
            drawTrainingMessage = false;
            HidePauseMenu();
        }

        private void BackClick(object sender, EventArgs e)
        {
            drawEndScore = false;
            drawRecords = false;
            drawScore = false;
            drawHints = false;
            if (Controls.Contains(playAgain))
                Controls.Remove(playAgain);
            HidePauseMenu();
            ShowStartMenu();
        }

        private void ShowStartMenu()
        {
            Drawer.Clear();
            Controls.Add(play);
            Controls.Add(learning);
            Controls.Add(records);
            Controls.Add(exit);
            play.Focus();
            MakeInvalidate();
        }

        private void HideStartMenu()
        {
            Controls.Remove(play);
            Controls.Remove(learning);
            Controls.Remove(records);
            Controls.Remove(exit);
        }

        public void ShowPauseMenu()
        {
            Controller.ResetKeyPressed();
            Controls.Add(resume);
            resume.Focus();
            Controls.Add(back);
            resume.Location = new Point((Game.Width - resume.Width) / 2, 500);
            back.Location = new Point(resume.Location.X + (resume.Width - back.Width) / 2, resume.Location.Y + resume.Height + 20);
        }

        private void HidePauseMenu()
        {
            Controls.Remove(resume);
            Controls.Remove(back);
        }

        private void ShowEndMenu()
        {
            Controls.Add(back);
            Controls.Add(playAgain);
            back.Location = new Point(playAgain.Location.X + (playAgain.Width - back.Width) / 2, playAgain.Location.Y + playAgain.Height + 20);
            playAgain.Focus();
        }

        private void HideEndMenu()
        {
            Controls.Remove(playAgain);
            Controls.Remove(back);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && isActive)
            {
                timer.Stop();
                isActive = false;
                ShowPauseMenu();
                if (training)
                {
                    drawHints = true;
                    MakeInvalidate();
                }
            }
            if (training && Controller.CheckTrainingChange(e))
            {
                var newHero = Controller.GetNewTrainingHero(e);
                if (map.Hero != newHero)
                    map = new Map(newHero);
            }
            else
                Controller.OnKeyDown(e.KeyCode);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            Controller.OnKeyUp(e.KeyCode);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Drawer.DrawMap(e);
            if (drawScore)
                Drawer.DrawScore(e, map.Score.ToString(), map.Scorestep.ToString());
            if (drawEndScore)
                Drawer.DrawEndScore(e, map.Score.ToString(), newRecord);
            if (drawRecords)
            {
                var streamReader = new StreamReader(@"..\..\Records.txt");
                var records = streamReader.ReadToEnd().Split('\n');
                Drawer.DrawRecords(e, records);
                streamReader.Close();
            }
            if (drawHints)
                Drawer.DrawHints(e, map.Hero);
            if (drawTrainingMessage)
                Drawer.DrawTrainingMessage(e);
        }
    }
}
