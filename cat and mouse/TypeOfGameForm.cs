using System;

using System.Drawing;
using System.IO;
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

        public Cat CatPlayer;
        public Mouse MousePlayer;

        public static GameState currentGameState = GameState.Game;

        public static Bitmap Pause = new(TypeOfGameForm.MainPath + @"\Pictures\pause.png");
        private Bitmap Menu = new(MainPath + @"\Pictures\menu.png");
        
        public readonly Image Cat = Image.FromFile(MainPath + @"\Pictures\cat.png");
        public readonly Image Mouse = Image.FromFile(MainPath + @"\Pictures\mouse.png");
        private readonly Image cheese = Image.FromFile(MainPath + @"\Pictures\cheese.png");
        private Size clientSize; 
        public TypeOfGameForm()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            DoubleBuffered = true;
            timer.Interval = 20;
            timer.Tick += Update;
            MaximizeBox = false;
            LoadLevels();
            ClientSize = new Size(Map.MapWidth * ElementSize, Map.MapHeight * ElementSize);
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
            var text = new StreamReader(MainPath + @"\Levels\Level1.txt").ReadToEnd();
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
                
            }

            if (currentGameState == GameState.PlayerChoose)
            {
                
            }
        }
    }
}