using System.Drawing;
using System.Windows.Forms;

namespace cat_and_mouse.Domain
{
    public static class Manipulator
    {
        private static bool isFirstLeft = true;
        private static bool isFirstRight = true;
        private static bool realFirst = true;
        private static bool isFirstEsc = true;
        
        public static void OnClick(Cat CatPlayer, Image Cat, Mouse MousePlayer, Image Mouse, KeyEventArgs e, ref Size ClientSize, int ElementSize)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    CatPlayer.Move(0, -1, CatPlayer);
                    CatPlayer.StateCheck(CatPlayer, MousePlayer);
                    break;
                case Keys.S:
                    CatPlayer.Move(0, 1, CatPlayer);
                    CatPlayer.StateCheck(CatPlayer, MousePlayer);
                    break;
                case Keys.A:
                    CatPlayer.Move(-1, 0, CatPlayer);
                    CatPlayer.StateCheck(CatPlayer, MousePlayer);
                    isFirstRight = true;
                    realFirst = false;
                    if (isFirstLeft && !realFirst)
                    {
                        Cat.RotateFlip(RotateFlipType.Rotate180FlipY);
                        isFirstLeft = false;
                    }
                    break;
                case Keys.D:
                    CatPlayer.Move(1, 0, CatPlayer);
                    CatPlayer.StateCheck(CatPlayer, MousePlayer);
                    isFirstLeft = true;
                    if (isFirstRight && !realFirst)
                    {
                        Cat.RotateFlip(RotateFlipType.Rotate180FlipY);
                        isFirstRight = false;
                        realFirst = false;
                    }
                    break;
                case Keys.Up:
                    MousePlayer.Move(0, -1, MousePlayer);
                    MousePlayer.StateCheck(MousePlayer);
                    CatPlayer.StateCheck(CatPlayer, MousePlayer);
                    break;
                case Keys.Down:
                    MousePlayer.Move(0, 1, MousePlayer);
                    MousePlayer.StateCheck(MousePlayer);
                    CatPlayer.StateCheck(CatPlayer, MousePlayer);
                    break;
                case Keys.Left:
                    MousePlayer.Move(-1, 0, MousePlayer);
                    MousePlayer.StateCheck(MousePlayer);
                    CatPlayer.StateCheck(CatPlayer, MousePlayer);
                    isFirstRight = true;
                    realFirst = false;
                    if (isFirstLeft && !realFirst)
                    {
                        Mouse.RotateFlip(RotateFlipType.Rotate180FlipY);
                        isFirstLeft = false;
                    }
                    realFirst = false;
                    break;
                case Keys.Right:
                    MousePlayer.Move(1, 0, MousePlayer);
                    MousePlayer.StateCheck(MousePlayer);
                    CatPlayer.StateCheck(CatPlayer, MousePlayer);
                    isFirstLeft = true;
                    if (isFirstRight && !realFirst)
                    {
                        Mouse.RotateFlip(RotateFlipType.Rotate180FlipY);
                        isFirstRight = false;
                        realFirst = false;
                    }
                    break;
                case Keys.Escape:
                    if (isFirstEsc)
                    {
                        TypeOfGameForm.currentGameState = GameState.Pause;
                        isFirstEsc = false;
                    }
                    else
                    {
                        TypeOfGameForm.currentGameState = GameState.Game;
                        ClientSize = new Size(Map.MapWidth * ElementSize, Map.MapHeight * ElementSize);
                        isFirstEsc = true;
                    }
                    break;
            }    
        }
    }
}