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
        
        public static void OnClick(Cat catPlayer, Image cat, Mouse mousePlayer, Image mouse, KeyEventArgs e, ref Size clientSize, int elementSize)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    catPlayer.Move(0, -1, catPlayer);
                    catPlayer.StateCheck(catPlayer, mousePlayer);
                    break;
                case Keys.S:
                    catPlayer.Move(0, 1, catPlayer);
                    catPlayer.StateCheck(catPlayer, mousePlayer);
                    break;
                case Keys.A:
                    catPlayer.Move(-1, 0, catPlayer);
                    catPlayer.StateCheck(catPlayer, mousePlayer);
                    isFirstRight = true;
                    realFirst = false;
                    if (isFirstLeft && !realFirst)
                    {
                        cat.RotateFlip(RotateFlipType.Rotate180FlipY);
                        isFirstLeft = false;
                    }
                    break;
                case Keys.D:
                    catPlayer.Move(1, 0, catPlayer);
                    catPlayer.StateCheck(catPlayer, mousePlayer);
                    isFirstLeft = true;
                    if (isFirstRight && !realFirst)
                    {
                        cat.RotateFlip(RotateFlipType.Rotate180FlipY);
                        isFirstRight = false;
                        realFirst = false;
                    }
                    break;
                case Keys.Up:
                    mousePlayer.Move(0, -1, mousePlayer);
                    mousePlayer.StateCheck(mousePlayer);
                    catPlayer.StateCheck(catPlayer, mousePlayer);
                    break;
                case Keys.Down:
                    mousePlayer.Move(0, 1, mousePlayer);
                    mousePlayer.StateCheck(mousePlayer);
                    catPlayer.StateCheck(catPlayer, mousePlayer);
                    break;
                case Keys.Left:
                    mousePlayer.Move(-1, 0, mousePlayer);
                    mousePlayer.StateCheck(mousePlayer);
                    catPlayer.StateCheck(catPlayer, mousePlayer);
                    isFirstRight = true;
                    realFirst = false;
                    if (isFirstLeft && !realFirst)
                    {
                        mouse.RotateFlip(RotateFlipType.Rotate180FlipY);
                        isFirstLeft = false;
                    }
                    realFirst = false;
                    break;
                case Keys.Right:
                    mousePlayer.Move(1, 0, mousePlayer);
                    mousePlayer.StateCheck(mousePlayer);
                    catPlayer.StateCheck(catPlayer, mousePlayer);
                    isFirstLeft = true;
                    if (isFirstRight && !realFirst)
                    {
                        mouse.RotateFlip(RotateFlipType.Rotate180FlipY);
                        isFirstRight = false;
                        realFirst = false;
                    }
                    break;
                case Keys.Escape:
                    if (TypeOfGameForm.currentGameState == GameState.Game || TypeOfGameForm.currentGameState == GameState.Pause)
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
    }
}