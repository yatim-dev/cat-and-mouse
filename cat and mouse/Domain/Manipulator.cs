  
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
            new Task(() => Manipulator.CatMove(catPlayer, cat, mousePlayer, e), TaskCreationOptions.LongRunning).Start();
            new Task(() => Manipulator.MouseMove(catPlayer, mousePlayer, mouse, e), TaskCreationOptions.LongRunning).Start();
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

        public static void CatMove(Cat catPlayer, Image cat, Mouse mousePlayer, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    catPlayer.deltaY = -1;
                    //catPlayer.Position.Y += catPlayer.deltaY;
                    //new Task(() => catPlayer.Move(catPlayer), TaskCreationOptions.LongRunning).Start();
                    //new Task(() => catPlayer.StateCheck(catPlayer, mousePlayer), TaskCreationOptions.LongRunning).Start();
                    break;
                case Keys.S:
                    catPlayer.deltaY = 1;
                    //catPlayer.Position.Y += catPlayer.deltaY;
                    //new Task(() => catPlayer.Move(catPlayer), TaskCreationOptions.LongRunning).Start();
                    //new Task(() => catPlayer.StateCheck(catPlayer, mousePlayer), TaskCreationOptions.LongRunning).Start();
                    break;
                case Keys.A:
                    catPlayer.deltaX = -1;
                    //catPlayer.Position.X += catPlayer.deltaX;

                    //new Task(() => catPlayer.Move(catPlayer), TaskCreationOptions.LongRunning).Start();
                    //new Task(() => catPlayer.StateCheck(catPlayer, mousePlayer), TaskCreationOptions.LongRunning).Start();
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
                    //catPlayer.Position.X += catPlayer.deltaX;
                    // new Task(() => catPlayer.Move(catPlayer), TaskCreationOptions.LongRunning).Start();
                    // new Task(() => catPlayer.StateCheck(catPlayer, mousePlayer), TaskCreationOptions.LongRunning).Start();
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

        public static void MouseMove(Cat catPlayer, Mouse mousePlayer, Image mouse, KeyEventArgs e)
            {
                switch (e.KeyCode)
                {
                    case Keys.Up:
                        mousePlayer.deltaY = -1;
                        //mousePlayer.Position.Y += mousePlayer.deltaY;
                        // new Task(() => mousePlayer.Move(mousePlayer), TaskCreationOptions.LongRunning).Start();
                        // new Task(() => mousePlayer.StateCheck(mousePlayer), TaskCreationOptions.LongRunning).Start();
                        // new Task(() => catPlayer.StateCheck(catPlayer, mousePlayer), TaskCreationOptions.LongRunning).Start();
                        break;
                    case Keys.Down:
                        mousePlayer.deltaY = 1;
                        //mousePlayer.Position.Y += mousePlayer.deltaY;
                        // new Task(() => mousePlayer.Move(mousePlayer), TaskCreationOptions.LongRunning).Start();
                        // new Task(() => mousePlayer.StateCheck(mousePlayer), TaskCreationOptions.LongRunning).Start();
                        // new Task(() => catPlayer.StateCheck(catPlayer, mousePlayer), TaskCreationOptions.LongRunning).Start();
                        break;
                    case Keys.Left:
                        mousePlayer.deltaX = -1;
                        //mousePlayer.Position.X += mousePlayer.deltaX;
                        // new Task(() => mousePlayer.Move(mousePlayer), TaskCreationOptions.LongRunning).Start();
                        // new Task(() => mousePlayer.StateCheck(mousePlayer), TaskCreationOptions.LongRunning).Start();
                        // new Task(() => catPlayer.StateCheck(catPlayer, mousePlayer), TaskCreationOptions.LongRunning).Start();
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
                       // mousePlayer.Position.X += mousePlayer.deltaX;
                        //mousePlayer.Move(mousePlayer);
                        // new Task(() => mousePlayer.Move(mousePlayer), TaskCreationOptions.LongRunning).Start();
                        // new Task(() => mousePlayer.StateCheck(mousePlayer), TaskCreationOptions.LongRunning).Start();
                        // new Task(() => catPlayer.StateCheck(catPlayer, mousePlayer), TaskCreationOptions.LongRunning).Start();
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

