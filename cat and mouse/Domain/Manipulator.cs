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

        public static void OnClick(Cat catPlayer, Image cat, Mouse mousePlayer, Image mouse, KeyEventArgs e,
            ref Size clientSize, int elementSize)
        {
            new Task(() => CatMove(catPlayer, cat, mousePlayer, e), TaskCreationOptions.LongRunning).Start();
            new Task(() => MouseMove(catPlayer, mousePlayer, mouse, e), TaskCreationOptions.LongRunning).Start();

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

        static void CatMove(Cat catPlayer, Image cat, Mouse mousePlayer, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    catPlayer.deltaY = -1;
                    catPlayer.Move(catPlayer);
                    catPlayer.StateCheck(catPlayer, mousePlayer);
                    break;
                case Keys.S:
                    catPlayer.deltaY = 1;
                    catPlayer.Move(catPlayer);
                    catPlayer.StateCheck(catPlayer, mousePlayer);
                    break;
                case Keys.A:
                    catPlayer.deltaX = -1;
                    catPlayer.Move(catPlayer);
                    catPlayer.StateCheck(catPlayer, mousePlayer);
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
                    catPlayer.Move(catPlayer);
                    catPlayer.StateCheck(catPlayer, mousePlayer);
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

        static void MouseMove(Cat catPlayer, Mouse mousePlayer, Image mouse, KeyEventArgs e)
            {
                switch (e.KeyCode)
                {
                    case Keys.Up:
                        mousePlayer.deltaY = -1;
                        mousePlayer.Move(mousePlayer);
                        mousePlayer.StateCheck(mousePlayer);
                        catPlayer.StateCheck(catPlayer, mousePlayer);
                        break;
                    case Keys.Down:
                        mousePlayer.deltaY = 1;
                        mousePlayer.Move(mousePlayer);
                        mousePlayer.StateCheck(mousePlayer);
                        catPlayer.StateCheck(catPlayer, mousePlayer);
                        break;
                    case Keys.Left:
                        mousePlayer.deltaX = -1;
                        mousePlayer.Move(mousePlayer);
                        mousePlayer.StateCheck(mousePlayer);
                        catPlayer.StateCheck(catPlayer, mousePlayer);
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
                        mousePlayer.Move(mousePlayer);
                        mousePlayer.StateCheck(mousePlayer);
                        catPlayer.StateCheck(catPlayer, mousePlayer);
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
        }
    }


