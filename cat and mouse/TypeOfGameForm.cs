using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using cat_and_mouse.Domain;
using Timer = System.Windows.Forms.Timer;

namespace cat_and_mouse
{
    public sealed partial class TypeOfGameForm : Form
    {
        private Timer timer = new();
        public const int ElementSize = 32;

        public static readonly string MainPath =
            new DirectoryInfo(Directory.GetCurrentDirectory()).Parent?.Parent?.Parent?.ToString();

        public Cat CatPlayer;
        public Mouse MousePlayer;

        public static GameState currentGameState = GameState.MapChoose;

        private static readonly Bitmap Pause = new(MainPath + @"\Pictures\pause.png");
        private readonly Bitmap menu = new(MainPath + @"\Pictures\menu.png");
        private readonly Bitmap catWin = new(MainPath + @"\Pictures\catWin.png");
        private readonly Bitmap mouseWin = new(MainPath + @"\Pictures\mouseWin.png");
        
        private readonly Bitmap cat = new(MainPath + @"\Pictures\cat.png");
        private readonly Bitmap mouse = new(MainPath + @"\Pictures\mouse.png");
        private readonly Bitmap cheese = new(MainPath + @"\Pictures\cheese.png");
        
        private Size clientSize;
        private static string levelNumber;

        public TypeOfGameForm()
        {
            InitializeComponent();
            DoubleBuffered = true;
            timer.Interval = 20;
            timer.Tick += Update;
            MaximizeBox = false;
            clientSize = new Size(menu.Width, menu.Height);
            StartPosition = FormStartPosition.CenterScreen;
            WindowState = FormWindowState.Normal;
            KeyDown += OnPress;
            //KeyUp
        }

        public void OnPress(object sender, KeyEventArgs e)
        {
            Manipulator.OnClick(CatPlayer, cat, MousePlayer, mouse, e, ref clientSize, ElementSize);
            ClientSize = clientSize;
            StartPosition = FormStartPosition.CenterScreen;
            WindowState = FormWindowState.Normal;
        }

        private static void LoadLevels()
        {
            var text = new StreamReader(MainPath + @"\Levels\Level" + levelNumber + ".txt").ReadToEnd();
            var lines = text.Split(new[] {"\r", "\n"}, StringSplitOptions.RemoveEmptyEntries);
            Map.FromLines(lines);
        }

        public void Initialize()
        {
            CatPlayer = new Cat(Map.CatPosition.X, Map.CatPosition.Y);
            MousePlayer = new Mouse(Map.MousePosition.X, Map.MousePosition.Y);
            timer.Start();
        }

        public void Update(object sender, EventArgs e)
        {
            Invalidate();
        }
        
        private void ButtonOnClick(object sender, EventArgs eventArgs)
        {
            if (sender is Button button)
            {
                levelNumber = button.Name.Substring(5);
            }
            LoadLevels();
            ClientSize = new Size(Map.MapWidth * ElementSize, Map.MapHeight * ElementSize);
            StartPosition = FormStartPosition.CenterScreen;
            WindowState = FormWindowState.Normal;
            Initialize();
            clientSize = ClientSize;
            StartPosition = FormStartPosition.CenterScreen;
            WindowState = FormWindowState.Normal;
            currentGameState = GameState.Game;
            Controls.Clear();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            
            if (currentGameState == GameState.Game)
            {
                Map.DrawMap(e.Graphics);
                Location = new Point((Screen.PrimaryScreen.WorkingArea.Width - Width) / 2,
                    (Screen.PrimaryScreen.WorkingArea.Height - Height) / 2);
                cheese.SetResolution(e.Graphics.DpiX, e.Graphics.DpiY);
                cat.SetResolution(e.Graphics.DpiX, e.Graphics.DpiY);
                mouse.SetResolution(e.Graphics.DpiX, e.Graphics.DpiY);
                e.Graphics.DrawImage(cheese, Map.CheesePosition.X * ElementSize, Map.CheesePosition.Y * ElementSize);
                e.Graphics.DrawImage(cat, CatPlayer.Position.X * ElementSize, CatPlayer.Position.Y * ElementSize);
                e.Graphics.DrawImage(mouse, MousePlayer.Position.X * ElementSize, MousePlayer.Position.Y * ElementSize);
            }

            if (currentGameState == GameState.Pause)
            {
                ClientSize = new Size(Pause.Width, Pause.Height);
                Location = new Point((Screen.PrimaryScreen.WorkingArea.Width - Width) / 2,
                    (Screen.PrimaryScreen.WorkingArea.Height - Height) / 2);
                Pause.SetResolution(e.Graphics.DpiX, e.Graphics.DpiY);
                e.Graphics.DrawImage(Pause, 0, 0);
            }

            if (currentGameState == GameState.MapChoose)
            {
                ClientSize = new Size(menu.Width, menu.Height);
                Location = new Point((Screen.PrimaryScreen.WorkingArea.Width - Width) / 2,
                    (Screen.PrimaryScreen.WorkingArea.Height - Height) / 2);
                e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
                menu.SetResolution(e.Graphics.DpiX, e.Graphics.DpiY);
                e.Graphics.DrawImage(menu, 0, 0);
                var top = 540;
                var left = 300;

                for (var i = 0; i < 3; i++)
                {
                    var button = new Button();
                    button.Left = left;
                    button.Top = top;
                    button.Height = 40;
                    button.Width = 120;
                    button.Name = "level" + i;
                    button.Text = "level " + i;
                    button.Click += ButtonOnClick;

                    Controls.Add(button);
                    left += button.Width + 20;
                }
            }

            if (currentGameState == GameState.PlayerChoose)
            {
                ClientSize = new Size(menu.Width, menu.Height);
                Location = new Point((Screen.PrimaryScreen.WorkingArea.Width - Width) / 2,
                    (Screen.PrimaryScreen.WorkingArea.Height - Height) / 2);
                menu.SetResolution(e.Graphics.DpiX, e.Graphics.DpiY);
                e.Graphics.DrawImage(menu, 0, 0);
            }
            if (currentGameState == GameState.CatWin)
            {
                ClientSize = new Size(catWin.Width, catWin.Height);
                Location = new Point((Screen.PrimaryScreen.WorkingArea.Width - Width) / 2,
                    (Screen.PrimaryScreen.WorkingArea.Height - Height) / 2);
                catWin.SetResolution(e.Graphics.DpiX, e.Graphics.DpiY);
                e.Graphics.DrawImage(catWin, 0, 0);
                Button restart = new Button();
                GameLogics.CreateRestartButton(restart, Controls);

            }
            if (currentGameState == GameState.MouseWin)
            {
                ClientSize = new Size(mouseWin.Width, mouseWin.Height);
                Location = new Point((Screen.PrimaryScreen.WorkingArea.Width - Width) / 2,
                    (Screen.PrimaryScreen.WorkingArea.Height - Height) / 2);
                mouseWin.SetResolution(e.Graphics.DpiX, e.Graphics.DpiY);
                e.Graphics.DrawImage(mouseWin, 0, 0);
                Button restart = new Button();
                GameLogics.CreateRestartButton(restart, Controls);
            }
        }
    }
}