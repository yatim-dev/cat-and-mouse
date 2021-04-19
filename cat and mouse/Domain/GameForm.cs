using System.IO;
using System.Windows.Forms;
using System;
using System.Drawing;

namespace cat_and_mouse.Domain
{
    public class GameForm : Form
    {
        private const int ElementSize = 32;
        static readonly string DomainBaseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        static readonly string MainPath = DomainBaseDirectory.Remove(DomainBaseDirectory.Length - 25, 25);
        //public const string MainPath = @"C:\Users\warsd\RiderProjects\cat-and-mouse\cat and mouse\";
        
        public GameForm()
        {
            LoadLevels();
            MaximizeBox = false;
            ClientSize = new Size(Map.MapWidth * ElementSize, Map.MapHeight * ElementSize);
        }
        
        private static void LoadLevels()
        {
            var text = new StreamReader(MainPath + @"Levels\Level1.txt").ReadToEnd();
            var lines = text.Split(new[] {"\r", "\n"}, StringSplitOptions.RemoveEmptyEntries);
            Map.FromLines(lines);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            // e.Graphics.FillRectangle(                                               
            //     Brushes.Black, 0, 0, ElementSize * Game.MapWidth,
            //     ElementSize * Game.MapHeight);
            var wall = Image.FromFile(MainPath + @"Pictures\wall.png");
            var empty = Image.FromFile(MainPath + @"Pictures\empty.png");
            var cat = Image.FromFile(MainPath + @"Pictures\cat.png");
            for (var i = 0; i < Map.MapWidth; i++)
            for (var j = 0; j < Map.MapHeight; j++)
            {
                //(Map.MapArray[i, j] == MapCell.Wall)
                //? e.Graphics.DrawImage(wall, i * ElementSize, j * ElementSize)
                //: e.Graphics.DrawImage(empty, i * ElementSize, j * ElementSize);
                if (Map.MapArray[i, j] == MapCell.Wall)
                    e.Graphics.DrawImage(wall, i * ElementSize, j * ElementSize);
                if(Map.MapArray[i,j] == MapCell.Empty)
                    e.Graphics.DrawImage(empty, i * ElementSize, j * ElementSize);
            }
            //e.Graphics.DrawImage(cat, 32, 32);

        }
    }
}