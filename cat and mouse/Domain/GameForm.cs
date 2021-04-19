using System.IO;
using System.Windows.Forms;
using System;
using System.Diagnostics;
using System.Drawing;
using Microsoft.VisualBasic.Logging;

namespace cat_and_mouse.Domain
{
    public class GameForm : Form
    {
        private const int ElementSize = 40;//???
        private static readonly string MainPath = new DirectoryInfo
            (Directory.GetCurrentDirectory()).Parent?.Parent?.Parent?.ToString();

        public GameForm()
        {
            LoadLevels();
            MaximizeBox = false;
            ClientSize = new Size((Map.MapWidth) * ElementSize, (Map.MapHeight) * ElementSize);
        }

        private static void LoadLevels()
        {
            var text = new StreamReader(MainPath + @"\Levels\Level1.txt").ReadToEnd();
            var lines = text.Split(new[] {"\r", "\n"}, StringSplitOptions.RemoveEmptyEntries);
            Map.FromLines(lines);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var wall = Image.FromFile(MainPath + @"\Pictures\wall.png");
            var empty = Image.FromFile(MainPath + @"\Pictures\empty.png");
            var cat = Image.FromFile(MainPath + @"\Pictures\cat.png");
            var mouse = Image.FromFile(MainPath + @"\Pictures\mouse.png");
            var cheese = Image.FromFile(MainPath + @"\Pictures\cheese.png");
            for (var i = 0; i < Map.MapWidth; i++)
            for (var j = 0; j < Map.MapHeight; j++)
            {
                if (Map.MapArray[i, j] == MapCell.Wall)
                    e.Graphics.DrawImage(wall, (i) * ElementSize, (j) * ElementSize);
                if (Map.MapArray[i, j] == MapCell.Empty)
                  e.Graphics.DrawImage(empty, (i) * ElementSize, (j) * ElementSize);
            }

            e.Graphics.DrawImage(cat,Map.CatPosition.X * ElementSize,Map.CatPosition.Y * ElementSize);
            e.Graphics.DrawImage(mouse,Map.MousePosition.X * ElementSize,Map.MousePosition.Y * ElementSize);
            e.Graphics.DrawImage(cheese,Map.CheesePosition.X * ElementSize,Map.CheesePosition.Y * ElementSize);

        }
    }
}