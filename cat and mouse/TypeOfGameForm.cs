using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;
using cat_and_mouse.Domain;
using Timer = System.Windows.Forms.Timer;

namespace cat_and_mouse
{
    //менюшка
    public sealed partial class TypeOfGameForm : Form
    {
        private Timer timer = new();
        public const int ElementSize = 32;

        public static readonly string MainPath =
            new DirectoryInfo(Directory.GetCurrentDirectory()).Parent?.Parent?.Parent?.ToString();

        public static Cat CatPlayer;
        public static Mouse MousePlayer;

        public static GameState currentGameState = GameState.PlayerChoose;
        public static PlayerState currentPlayerState; 
        
        private static readonly Bitmap Pause = new(MainPath + @"\Pictures\pause.png");
        private readonly Bitmap menu = new(MainPath + @"\Pictures\menu.png");
        private readonly Bitmap catWin = new(MainPath + @"\Pictures\catWin.png");
        private readonly Bitmap mouseWin = new(MainPath + @"\Pictures\mouseWin.png");
        private static readonly Bitmap PlayerChoise = new(MainPath + @"\Pictures\playerChoice.png");


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
            KeyDown += OnPress;
            KeyUp += OnKeyUp;
        }
        
        public void OnKeyUp(object sender, KeyEventArgs e)
        {
            if (currentGameState == GameState.Game)
                Manipulator.KeyUp(e);
        }

        public void OnPress(object sender, KeyEventArgs e)
        {
            if (currentGameState == GameState.Game || currentGameState == GameState.Pause)
            {
                Manipulator.OnClick(e, ref clientSize, ElementSize);
                if (currentPlayerState == PlayerState.MouseBot && currentGameState == GameState.Game)
                {
                    Manipulator.CatMove(CatPlayer, cat, e);
                    PhysicsMap.IsCollide(CatPlayer);
                    Manipulator.autoWay?.RemoveAt(0);
                }
                if (currentPlayerState == PlayerState.CatBot&& currentGameState == GameState.Game)
                {
                    Manipulator.MouseMove(MousePlayer, mouse, e);
                    PhysicsMap.IsCollide(MousePlayer);
                    Manipulator.autoWay?.RemoveAt(0);
                }
                if(currentPlayerState == PlayerState.NoBot&& currentGameState == GameState.Game)
                {
                    Manipulator.CatMove(CatPlayer, cat, e);
                    PhysicsMap.IsCollide(CatPlayer);
                    Manipulator.MouseMove(MousePlayer, mouse, e);
                    PhysicsMap.IsCollide(MousePlayer);
                }

                if (currentGameState == GameState.Game)
                {
                    Manipulator.ChangePosition();
                    ClientSize = clientSize;
                    Cat.StateCheck(CatPlayer, MousePlayer);
                    Mouse.StateCheck(MousePlayer);
                    if (currentPlayerState == PlayerState.CatBot)
                    {
                        Manipulator.autoWay?.Clear();
                        Manipulator.AutoMove(CatPlayer);
                    }
                }
            }
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
            if(currentPlayerState == PlayerState.MouseBot)
                Manipulator.AutoMove(MousePlayer);
            if(currentPlayerState == PlayerState.CatBot)
                Manipulator.AutoMove(CatPlayer);
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
            Initialize();
            clientSize = ClientSize;
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
                ClientSize = new Size(PlayerChoise.Width, PlayerChoise.Height);
                Location = new Point((Screen.PrimaryScreen.WorkingArea.Width - Width) / 2,
                    (Screen.PrimaryScreen.WorkingArea.Height - Height) / 2);
                PlayerChoise.SetResolution(e.Graphics.DpiX, e.Graphics.DpiY);
                e.Graphics.DrawImage(PlayerChoise, 0, 0);
                var top = 540;
                var left = 300;
                var ButtonsName = new[] {
                    "MouseBot",
                    "NoBot",
                    "CatBot"
                };
                var ButtonsText = new[]
                {
                    "Mouse Bot",
                    "Two Players",
                    "Cat Bot"
                };
                for (var i = 0; i < 3; i++)
                {
                    var button = new Button();
                    button.Left = left;
                    button.Top = top;
                    button.Height = 40;
                    button.Width = 120;
                    button.Name = ButtonsName[i];
                    button.Text = ButtonsText[i];
                    button.Click += PlayerChoiceButtonOnClick;

                    Controls.Add(button);
                    left += button.Width + 20;
                }
            }

            if (currentGameState == GameState.CatWin)
            {
                Manipulator.autoWay.Clear();
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
                Manipulator.autoWay.Clear();
                ClientSize = new Size(mouseWin.Width, mouseWin.Height);
                Location = new Point((Screen.PrimaryScreen.WorkingArea.Width - Width) / 2,
                    (Screen.PrimaryScreen.WorkingArea.Height - Height) / 2);
                mouseWin.SetResolution(e.Graphics.DpiX, e.Graphics.DpiY);
                e.Graphics.DrawImage(mouseWin, 0, 0);
                Button restart = new Button();
                GameLogics.CreateRestartButton(restart, Controls);
            }
        }
        private void PlayerChoiceButtonOnClick(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                if (button.Name == "MouseBot")
                    currentPlayerState = PlayerState.MouseBot;
                if (button.Name == "NoBot")
                    currentPlayerState = PlayerState.NoBot;
                if (button.Name == "CatBot")
                    currentPlayerState = PlayerState.CatBot;
            }
            
            Controls.Clear();
            currentGameState = GameState.MapChoose;
            timer.Start();

        }
    }
}