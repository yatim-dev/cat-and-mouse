using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cat_and_mouse.Domain
{
    public static class Manipulator
    {
        private static bool isFirstLeft = true;
        private static bool isFirstRight = true;
        private static bool realFirstCat = true;
        private static bool isFirstA = true;
        private static bool isFirstD = true;
        private static bool realFirstMouse = true;
        private static bool isFirstEsc = true;
        private static bool isFirstShift = true;
        public static readonly SoundPlayer explosionSound = new(TypeOfGameForm.MainPath + @"\Music\explosion.wav");

        public static void OnClick(KeyEventArgs e, ref Size clientSize, int elementSize)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    if (TypeOfGameForm.CurrentGameState == GameState.Game ||
                        TypeOfGameForm.CurrentGameState == GameState.Pause)
                    {
                        if (isFirstEsc)
                        {
                            TypeOfGameForm.CurrentGameState = GameState.Pause;
                            isFirstEsc = false;
                        }
                        else
                        {
                            TypeOfGameForm.CurrentGameState = GameState.Game;
                            clientSize = new Size(Map.MapWidth * elementSize, Map.MapHeight * elementSize);
                            isFirstEsc = true;
                        }
                    }

                    break;
                case Keys.ShiftKey:
                    if (isFirstShift)
                    {
                        isFirstShift = false;
                        // explosionSound.LoadAsync();
                        // explosionSound.Play();
                        //explosionSound.
                        Map.ChangeMap(TypeOfGameForm.MousePlayer);
                    }

                    break;
            }
        }

        public static void CatMove(Cat catPlayer, Image cat, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    catPlayer.DeltaY = -1;
                    break;
                case Keys.S:
                    catPlayer.DeltaY = 1;
                    break;
                case Keys.A:
                    catPlayer.DeltaX = -1;
                    isFirstD = true;
                    realFirstCat = false;
                    if (isFirstA && !realFirstCat)
                    {
                        cat.RotateFlip(RotateFlipType.Rotate180FlipY);
                        isFirstA = false;
                    }

                    break;
                case Keys.D:
                    catPlayer.DeltaX = 1;
                    isFirstA = true;
                    if (isFirstD && !realFirstCat)
                    {
                        cat.RotateFlip(RotateFlipType.Rotate180FlipY);
                        isFirstD = false;
                        realFirstCat = false;
                    }

                    break;
            }
        }

        public static void MouseMove(Mouse mousePlayer, Image mouse, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    mousePlayer.DeltaY = -1;
                    break;
                case Keys.Down:
                    mousePlayer.DeltaY = 1;
                    break;
                case Keys.Left:
                    mousePlayer.DeltaX = -1;
                    isFirstRight = true;
                    realFirstMouse = false;
                    if (isFirstLeft && !realFirstMouse)
                    {
                        mouse.RotateFlip(RotateFlipType.Rotate180FlipY);
                        isFirstLeft = false;
                        realFirstMouse = false;
                    }

                    break;
                case Keys.Right:
                    mousePlayer.DeltaX = 1;
                    isFirstLeft = true;
                    if (isFirstRight && !realFirstMouse)
                    {
                        mouse.RotateFlip(RotateFlipType.Rotate180FlipY);
                        isFirstRight = false;
                        realFirstMouse = false;
                    }

                    break;
            }
        }

        public static void KeyUp(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    TypeOfGameForm.CatPlayer.DeltaY = 0;
                    break;
                case Keys.S:
                    TypeOfGameForm.CatPlayer.DeltaY = 0;
                    break;
                case Keys.A:
                    TypeOfGameForm.CatPlayer.DeltaX = 0;
                    break;
                case Keys.D:
                    TypeOfGameForm.CatPlayer.DeltaX = 0;
                    break;
                case Keys.Up:
                    TypeOfGameForm.MousePlayer.DeltaY = 0;
                    break;
                case Keys.Down:
                    TypeOfGameForm.MousePlayer.DeltaY = 0;
                    break;
                case Keys.Left:
                    TypeOfGameForm.MousePlayer.DeltaX = 0;
                    break;
                case Keys.Right:
                    TypeOfGameForm.MousePlayer.DeltaX = 0;
                    break;
            }
        }

        public static void ChangePosition()
        {
            if (TypeOfGameForm.CurrentPlayerState == PlayerState.MouseBot)
            {
                TypeOfGameForm.MousePlayer.Position.X = GameLogics.AutoWay.First().X;
                TypeOfGameForm.MousePlayer.Position.Y = GameLogics.AutoWay.First().Y;
                TypeOfGameForm.CatPlayer.Position.X += TypeOfGameForm.CatPlayer.DeltaX;
                TypeOfGameForm.CatPlayer.Position.Y += TypeOfGameForm.CatPlayer.DeltaY;
            }

            if (TypeOfGameForm.CurrentPlayerState == PlayerState.CatBot)
            {
                TypeOfGameForm.CatPlayer.Position.X = GameLogics.AutoWay.First().X;
                TypeOfGameForm.CatPlayer.Position.Y = GameLogics.AutoWay.First().Y;
                TypeOfGameForm.MousePlayer.Position.X += TypeOfGameForm.MousePlayer.DeltaX;
                TypeOfGameForm.MousePlayer.Position.Y += TypeOfGameForm.MousePlayer.DeltaY;
            }

            if (TypeOfGameForm.CurrentPlayerState == PlayerState.NoBot)
            {
                TypeOfGameForm.MousePlayer.Position.X += TypeOfGameForm.MousePlayer.DeltaX;
                TypeOfGameForm.MousePlayer.Position.Y += TypeOfGameForm.MousePlayer.DeltaY;
                TypeOfGameForm.CatPlayer.Position.X += TypeOfGameForm.CatPlayer.DeltaX;
                TypeOfGameForm.CatPlayer.Position.Y += TypeOfGameForm.CatPlayer.DeltaY;
            }
        }
    }
}