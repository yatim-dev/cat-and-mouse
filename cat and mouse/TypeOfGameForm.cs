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
using cat_and_mouse.Domain;

namespace cat_and_mouse
{
    public sealed partial class TypeOfGameForm : Form
    {
        private Timer timer = new();
        public const int ElementSize = 40;//???
        public static readonly string MainPath = new DirectoryInfo
            (Directory.GetCurrentDirectory()).Parent?.Parent?.Parent?.ToString();

        public Character CatPlayer;
        public Character MousePlayer;
        private bool isFirstA = true;
        private bool isFirstD = true;
        private bool realFirst = true;
        public readonly Image Cat = Image.FromFile(MainPath + @"\Pictures\cat.png");
        public readonly Image Mouse = Image.FromFile(MainPath + @"\Pictures\mouse.png");
        private readonly Image cheese = Image.FromFile(MainPath + @"\Pictures\cheese.png");
        public TypeOfGameForm()
        {
            InitializeComponent();
            DoubleBuffered = true;
            timer.Interval = 20;
            timer.Tick += Update;
            LoadLevels();
            MaximizeBox = false;
            ClientSize = new Size((Map.MapWidth) * ElementSize, (Map.MapHeight) * ElementSize);
            Initialize();
            KeyDown += OnPress;
        }
        
        public void OnPress(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    CatPlayer.Move(0, -1);
                    break;
                case Keys.S:
                    CatPlayer.Move(0, 1);
                    break;
                case Keys.A:
                    CatPlayer.Move(-1, 0);
                    isFirstD = true;
                    realFirst = false;
                    if (isFirstA && !realFirst)
                    {
                        Cat.RotateFlip(RotateFlipType.Rotate180FlipY);
                        isFirstA = false;
                    }
                    realFirst = false;
                    break;
                case Keys.D:
                    CatPlayer.Move(1, 0);
                    isFirstA = true;
                    if (isFirstD && !realFirst)
                    {
                        Cat.RotateFlip(RotateFlipType.Rotate180FlipY);
                        isFirstD = false;
                        realFirst = false;
                    }
                    break;
            }
            switch (e.KeyCode)
            {
                 case Keys.Up:
                    MousePlayer.Move(0, -1);
                    break;
                case Keys.Down:
                    MousePlayer.Move(0, 1);
                    break;
                case Keys.Left:
                    MousePlayer.Move(-1, 0);
                    break;
                case Keys.Right:
                    MousePlayer.Move(1, 0);
                    break;
            }
        }
        private static void LoadLevels()
        {
            var text = new StreamReader(MainPath + @"\Levels\Level1.txt").ReadToEnd();
            var lines = text.Split(new[] {"\r", "\n"}, StringSplitOptions.RemoveEmptyEntries);
            Map.FromLines(lines);
        }

        public void Initialize()
        {
            CatPlayer = new Character(1,1);
            MousePlayer = new Character(2, 1);
            timer.Start();
        }

        public void Update(object sender, EventArgs e)
        {
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            Map.DrawMap(g);
            e.Graphics.DrawImage(Cat, CatPlayer.position.X * ElementSize, CatPlayer.position.Y * ElementSize);
            //e.Graphics.DrawImage(Mouse,MousePlayer.position.X * ElementSize,MousePlayer.position.Y * ElementSize);
            //e.Graphics.DrawImage(cheese,Map.CheesePosition.X * ElementSize,Map.CheesePosition.Y * ElementSize);

        }
    }
}