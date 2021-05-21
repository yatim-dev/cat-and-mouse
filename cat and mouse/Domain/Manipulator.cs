using System.Drawing;
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

        public static void OnClick(KeyEventArgs e,
            ref Size clientSize, int elementSize)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    if (TypeOfGameForm.currentGameState == GameState.Game ||
                        TypeOfGameForm.currentGameState == GameState.Pause)
                    {
                        if (isFirstEsc)
                        {
                            TypeOfGameForm.currentGameState = GameState.Pause;
                            isFirstEsc = false;
                        }
                        else
                        {
                            TypeOfGameForm.currentGameState = GameState.Game;
                            clientSize = new Size(Map.MapWidth * elementSize, Map.MapHeight * elementSize);
                            isFirstEsc = true;
                        }
                    }

                    break;
            }
        }

        public static void CatMove(Cat catPlayer, Image cat, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    catPlayer.deltaY = -1;
                    break;
                case Keys.S:
                    catPlayer.deltaY = 1;
                    break;
                case Keys.A:
                    catPlayer.deltaX = -1;
                    isFirstD = true;
                    realFirstCat = false;
                    if (isFirstA && !realFirstCat)
                    {
                        cat.RotateFlip(RotateFlipType.Rotate180FlipY);
                        isFirstA = false;
                    }

                    break;
                case Keys.D:
                    catPlayer.deltaX = 1;
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
                    mousePlayer.deltaY = -1;
                    break;
                case Keys.Down:
                    mousePlayer.deltaY = 1;
                    break;
                case Keys.Left:
                    mousePlayer.deltaX = -1;
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
                    mousePlayer.deltaX = 1;
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
                    TypeOfGameForm.CatPlayer.deltaY = 0;
                    break;
                case Keys.S:
                    TypeOfGameForm.CatPlayer.deltaY = 0;
                    break;
                case Keys.A:
                    TypeOfGameForm.CatPlayer.deltaX = 0;
                    break;
                case Keys.D:
                    TypeOfGameForm.CatPlayer.deltaX = 0;
                    break;
                case Keys.Up:
                    TypeOfGameForm.MousePlayer.deltaY = 0;
                    break;
                case Keys.Down:
                    TypeOfGameForm.MousePlayer.deltaY = 0;
                    break;
                case Keys.Left:
                    TypeOfGameForm.MousePlayer.deltaX = 0;
                    break;
                case Keys.Right:
                    TypeOfGameForm.MousePlayer.deltaX = 0;
                    break;
            }
        }

        public static void ChangePosition()
        {
            TypeOfGameForm.CatPlayer.Position.X += TypeOfGameForm.CatPlayer.deltaX;
            TypeOfGameForm.CatPlayer.Position.Y += TypeOfGameForm.CatPlayer.deltaY;
            TypeOfGameForm.MousePlayer.Position.X += TypeOfGameForm.MousePlayer.deltaX;
            TypeOfGameForm.MousePlayer.Position.Y += TypeOfGameForm.MousePlayer.deltaY;
        }
    }
}