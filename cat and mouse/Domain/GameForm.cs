using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.PropertyGridInternal;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Net.Mime;

namespace cat_and_mouse.Domain
{
    public class GameForm : Form
    {
        private readonly Dictionary<string, Bitmap> bitmaps = new();//
        public const int ElementSize = 32;
        public const string Path = @"C:\Users\warsd\RiderProjects\cat-and-mouse\cat and mouse\";
        public GameForm()
        {
            // var imagesDirectory = new DirectoryInfo("Images");
            // foreach (var e in imagesDirectory.GetFiles("*.png"))
            //     bitmaps[e.Name] = (Bitmap) Image.FromFile(e.FullName);
            var levels = LoadLevels();
            //PrintMap(levels);
        }

        private static IEnumerable<Map> LoadLevels()
        {
            const string pathForLevel1 = @"C:\Users\warsd\RiderProjects\cat-and-mouse\cat and mouse\Levels\Level1.txt";
            var text = new StreamReader(pathForLevel1);
            
            yield return Map.FromText(text.ReadToEnd());
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var wallPath = Path + @"Pictures\wall.png";
            var wall = Image.FromFile(wallPath);
            var empty = Image.FromFile(Path + @"Pictures\empty.png");
            e.Graphics.FillRectangle(                                               //задний фон
                Brushes.Black, 0, 0, ElementSize * Game.MapWidth,
                ElementSize * Game.MapHeight);
            e.Graphics.DrawImage(wall, 0,0);//вывод картинок
        }
        private void PrintMap(IEnumerable<Map> levels)
        {
            throw new NotImplementedException();
        }
    }
}