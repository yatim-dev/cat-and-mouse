using System.IO;
using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using Microsoft.VisualBasic.Logging;

namespace cat_and_mouse.Domain
{
    public class GameForm : Form
    {
        public const int ElementSize = 40;//???

        public static readonly string MainPath = new DirectoryInfo
            (Directory.GetCurrentDirectory()).Parent?.Parent?.Parent?.ToString();

        public Character player;
        private Timer timer;
        
        public readonly Image Cat = Image.FromFile(GameForm.MainPath + @"\Pictures\cat.png");
        public readonly Image Mouse = Image.FromFile(GameForm.MainPath + @"\Pictures\mouse.png");
        private readonly Image cheese = Image.FromFile(GameForm.MainPath + @"\Pictures\cheese.png");

        public GameForm()
        {
            InitializeComponent();
            LoadLevels();
            MaximizeBox = false;
            ClientSize = new Size((Map.MapWidth) * ElementSize, (Map.MapHeight) * ElementSize);
            Initialize();
        }

        private static void LoadLevels()
        {
            var text = new StreamReader(MainPath + @"\Levels\Level1.txt").ReadToEnd();
            var lines = text.Split(new[] {"\r", "\n"}, StringSplitOptions.RemoveEmptyEntries);
            Map.FromLines(lines);
        }

        public void Initialize()
        {
            player = new Character(1,1, Cat);
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            Map.DrawMap(g);
            //Invalidate(true);
           // Refresh();
            //Update();
           e.Graphics.DrawImage(Cat, player.position.X * ElementSize, player.position.Y * ElementSize);
           /* e.Graphics.DrawImage(mouse,Map.MousePosition.X * ElementSize,Map.MousePosition.Y * ElementSize);
            e.Graphics.DrawImage(cheese,Map.CheesePosition.X * ElementSize,Map.CheesePosition.Y * ElementSize);
            */

        }
    }
}