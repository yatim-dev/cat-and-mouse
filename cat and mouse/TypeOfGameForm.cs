﻿using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using cat_and_mouse.Domain;

namespace cat_and_mouse
{
    public sealed partial class TypeOfGameForm : Form
    {
        private Timer timer = new();
        public const int ElementSize = 40; //???

        public static readonly string MainPath =
            new DirectoryInfo(Directory.GetCurrentDirectory()).Parent?.Parent?.Parent?.ToString();

        public Cat CatPlayer;
        public Mouse MousePlayer;

        public static GameState currentGameState = GameState.MapChoose;

        public static Bitmap Pause = new(MainPath + @"\Pictures\pause.png");
        private Bitmap Menu = new(MainPath + @"\Pictures\menu.png");

        public readonly Image Cat = Image.FromFile(MainPath + @"\Pictures\cat.png");
        public readonly Image Mouse = Image.FromFile(MainPath + @"\Pictures\mouse.png");
        private readonly Image cheese = Image.FromFile(MainPath + @"\Pictures\cheese.png");
        private Size clientSize;
        private static string levelNumber;

        public TypeOfGameForm()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            DoubleBuffered = true;
            timer.Interval = 20;
            timer.Tick += Update;
            MaximizeBox = false;
            //MinimumSize = ClientSize;
            Initialize();
            KeyDown += OnPress;
        }

        public void OnPress(object sender, KeyEventArgs e)
        {
            Manipulator.OnClick(CatPlayer, Cat, MousePlayer, Mouse, e, ref clientSize, ElementSize);
            ClientSize = clientSize;
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
            clientSize = ClientSize;
            currentGameState = GameState.Game;
            ClientSize = clientSize;
            Controls.Clear();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (currentGameState == GameState.Game)
            {
                Map.DrawMap(e.Graphics);
                e.Graphics.DrawImage(cheese, Map.CheesePosition.X * ElementSize, Map.CheesePosition.Y * ElementSize);
                e.Graphics.DrawImage(Cat, CatPlayer.Position.X * ElementSize, CatPlayer.Position.Y * ElementSize);
                e.Graphics.DrawImage(Mouse, MousePlayer.Position.X * ElementSize, MousePlayer.Position.Y * ElementSize);
            }

            if (currentGameState == GameState.Pause)
            {
                ClientSize = new Size(Pause.Width, Pause.Height);
                e.Graphics.DrawImage(Pause, 0, 0);
            }

            if (currentGameState == GameState.MapChoose)
            {
                ClientSize = new Size(Menu.Width, Menu.Height);
                e.Graphics.DrawImage(Menu, 0, 0);
                var top = 550;
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
                ClientSize = new Size(Menu.Width, Menu.Height);
                e.Graphics.DrawImage(Menu, 0, 0);
            }
        }
    }
}