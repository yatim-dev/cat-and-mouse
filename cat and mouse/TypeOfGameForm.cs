using System;
using System.Drawing;
using System.IO;
using System.Media;
using System.Windows.Forms;
using cat_and_mouse.Domain;

namespace cat_and_mouse
{
    public sealed partial class TypeOfGameForm : Form
    {
        public const int ElementSize = 32;

        public static readonly string MainPath =
            new DirectoryInfo(Directory.GetCurrentDirectory()).Parent?.Parent?.Parent?.ToString();

        public static Cat CatPlayer;
        public static Mouse MousePlayer;

        public static GameState CurrentGameState = GameState.Start;
        public static PlayerState CurrentPlayerState;

        private static readonly Bitmap Pause = new(MainPath + @"\Pictures\pause.png");
        private static readonly Bitmap PlayerChoice = new(MainPath + @"\Pictures\playerChoice.png");
        private static readonly Bitmap Start = new(MainPath + @"\Pictures\start.png");
        private static readonly Bitmap Draw = new(MainPath + @"\Pictures\draw.png");
        private static string levelNumber;
        public static readonly SoundPlayer mainSound = new(MainPath + @"\Music\MainGameTheme.wav");


        private readonly Bitmap cat = new(MainPath + @"\Pictures\cat.png");
        private readonly Bitmap catWin = new(MainPath + @"\Pictures\catWin.png");
        private readonly Bitmap cheese = new(MainPath + @"\Pictures\cheese.png");
        private readonly Bitmap menu = new(MainPath + @"\Pictures\menu.png");
        private readonly Bitmap mouse = new(MainPath + @"\Pictures\mouse.png");
        private readonly Bitmap mouseWin = new(MainPath + @"\Pictures\mouseWin.png");
        private readonly Timer timer = new();

        private Size clientSize;

        public TypeOfGameForm()
        {
            InitializeComponent();
            DoubleBuffered = true;
            timer.Interval = 20;
            timer.Tick += Update;
            MaximizeBox = false;
            mainSound.PlayLooping();
            KeyDown += OnPress;
            KeyUp += OnKeyUp;
        }

        private static void LoadLevels()
        {
            var text = new StreamReader(MainPath + @"\Levels\Level" + levelNumber + ".txt").ReadToEnd();
            var lines = text.Split(new[] {"\r", "\n"}, StringSplitOptions.RemoveEmptyEntries);
            Map.FromLines(lines);
        }

        private static void Initialize()
        {
            CatPlayer = new Cat(Map.CatPosition.X, Map.CatPosition.Y);
            MousePlayer = new Mouse(Map.MousePosition.X, Map.MousePosition.Y);
            if (CurrentPlayerState == PlayerState.MouseBot) GameLogics.AutoMove(MousePlayer);
            if (CurrentPlayerState == PlayerState.CatBot) GameLogics.AutoMove(CatPlayer);
        }

        private void Update(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            if (CurrentGameState == GameState.Game) Manipulator.KeyUp(e);
        }

        private void OnPress(object sender, KeyEventArgs e)
        {
            if (CurrentGameState == GameState.Game || CurrentGameState == GameState.Pause)
            {
                Manipulator.OnClick(e, ref clientSize, ElementSize);
                if (CurrentPlayerState == PlayerState.MouseBot && CurrentGameState == GameState.Game)
                {
                    Manipulator.CatMove(CatPlayer, cat, e);
                    PhysicsMap.IsCollide(CatPlayer);
                    GameLogics.AutoWay?.RemoveAt(0);
                }

                if (CurrentPlayerState == PlayerState.CatBot && CurrentGameState == GameState.Game)
                {
                    Manipulator.MouseMove(MousePlayer, mouse, e);
                    PhysicsMap.IsCollide(MousePlayer);
                    GameLogics.AutoWay?.RemoveAt(0);
                }

                if (CurrentPlayerState == PlayerState.NoBot && CurrentGameState == GameState.Game)
                {
                    Manipulator.CatMove(CatPlayer, cat, e);
                    PhysicsMap.IsCollide(CatPlayer);
                    Manipulator.MouseMove(MousePlayer, mouse, e);
                    PhysicsMap.IsCollide(MousePlayer);
                }

                if (CurrentGameState == GameState.Game)
                {
                    Manipulator.ChangePosition();
                    ClientSize = clientSize;
                    PositionCheck.StateCatWinCheck(CatPlayer, MousePlayer);
                    PositionCheck.StateMouseWinCheck(MousePlayer);
                    PositionCheck.StateCheck(CatPlayer, MousePlayer);
                    if (CurrentPlayerState == PlayerState.CatBot)
                    {
                        GameLogics.AutoWay?.Clear();
                        GameLogics.AutoMove(CatPlayer);
                    }
                }
            }
        }

        private void ChoiceMapButtonOnClick(object sender, EventArgs eventArgs)
        {
            if (sender is Button button)
                levelNumber = button.Name.Substring(5);

            LoadLevels();
            ClientSize = new Size(Map.MapWidth * ElementSize, Map.MapHeight * ElementSize);
            Initialize();
            clientSize = ClientSize;
            CurrentGameState = GameState.Game;
            Controls.Clear();
        }

        private void PlayerChoiceButtonOnClick(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                if (button.Name == "MouseBot")
                    CurrentPlayerState = PlayerState.MouseBot;
                if (button.Name == "NoBot")
                    CurrentPlayerState = PlayerState.NoBot;
                if (button.Name == "CatBot")
                    CurrentPlayerState = PlayerState.CatBot;
            }

            Controls.Clear();
            CurrentGameState = GameState.MapChoose;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (CurrentGameState == GameState.Start)
            {
                ClientSize = new Size(Start.Width, Start.Height);
                Location = new Point((Screen.PrimaryScreen.WorkingArea.Width - Width) / 2,
                    (Screen.PrimaryScreen.WorkingArea.Height - Height) / 2);
                Start.SetResolution(e.Graphics.DpiX, e.Graphics.DpiY);
                e.Graphics.DrawImage(Start, 0, 0);
                var start = new Button();
                GameLogics.CreateStartButton(start, Controls);
                timer.Start();
            }
            
            if (CurrentGameState == GameState.Game)
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

            if (CurrentGameState == GameState.Pause)
            {
                ClientSize = new Size(Pause.Width, Pause.Height);
                Location = new Point((Screen.PrimaryScreen.WorkingArea.Width - Width) / 2,
                    (Screen.PrimaryScreen.WorkingArea.Height - Height) / 2);
                Pause.SetResolution(e.Graphics.DpiX, e.Graphics.DpiY);
                e.Graphics.DrawImage(Pause, 0, 0);
            }

            if (CurrentGameState == GameState.MapChoose)
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
                    var button = new Button
                    {
                        Left = left,
                        Top = top,
                        Height = 40,
                        Width = 150,
                        Name = "level" + i,
                        Text = "level " + (i + 1)
                    };
                    button.Click += ChoiceMapButtonOnClick;

                    Controls.Add(button);
                    left += button.Width + 20;
                }
            }

            if (CurrentGameState == GameState.PlayerChoose)
            {
                ClientSize = new Size(PlayerChoice.Width, PlayerChoice.Height);
                Location = new Point((Screen.PrimaryScreen.WorkingArea.Width - Width) / 2,
                    (Screen.PrimaryScreen.WorkingArea.Height - Height) / 2);
                PlayerChoice.SetResolution(e.Graphics.DpiX, e.Graphics.DpiY);
                e.Graphics.DrawImage(PlayerChoice, 0, 0);
                var top = 540;
                var left = 300;
                var buttonsName = new[] {"MouseBot", "NoBot", "CatBot"};
                var buttonsText = new[] {"Mouse Bot", "Two Players", "Cat Bot"};
                for (var i = 0; i < 3; i++)
                {
                    var button = new Button
                    {
                        Left = left,
                        Top = top,
                        Height = 40,
                        Width = 150,
                        Name = buttonsName[i],
                        Text = buttonsText[i]
                    };
                    button.Click += PlayerChoiceButtonOnClick;

                    Controls.Add(button);
                    left += button.Width + 20;
                }
            }

            if (CurrentGameState == GameState.CatWin)
            {
                GameLogics.AutoWay.Clear();
                ClientSize = new Size(catWin.Width, catWin.Height);
                Location = new Point((Screen.PrimaryScreen.WorkingArea.Width - Width) / 2,
                    (Screen.PrimaryScreen.WorkingArea.Height - Height) / 2);
                catWin.SetResolution(e.Graphics.DpiX, e.Graphics.DpiY);
                e.Graphics.DrawImage(catWin, 0, 0);
                var restart = new Button();
                GameLogics.CreateRestartButton(restart, Controls);
            }

            if (CurrentGameState == GameState.MouseWin)
            {
                GameLogics.AutoWay.Clear();
                ClientSize = new Size(mouseWin.Width, mouseWin.Height);
                Location = new Point((Screen.PrimaryScreen.WorkingArea.Width - Width) / 2,
                    (Screen.PrimaryScreen.WorkingArea.Height - Height) / 2);
                mouseWin.SetResolution(e.Graphics.DpiX, e.Graphics.DpiY);
                e.Graphics.DrawImage(mouseWin, 0, 0);
                var restart = new Button();
                GameLogics.CreateRestartButton(restart, Controls);
            }

            if (CurrentGameState == GameState.Draw)
            {
                ClientSize = new Size(Draw.Width, Draw.Height);
                Location = new Point((Screen.PrimaryScreen.WorkingArea.Width - Width) / 2,
                    (Screen.PrimaryScreen.WorkingArea.Height - Height) / 2);
                Draw.SetResolution(e.Graphics.DpiX, e.Graphics.DpiY);
                e.Graphics.DrawImage(Draw, 0, 0);
                var restart = new Button();
                GameLogics.CreateRestartButton(restart, Controls);
            }
        }
    }
}