using System;
using System.Drawing;
using System.Windows.Forms;

namespace cat_and_mouse.Domain
{
    public class Mouse : Character
    {
        public Mouse(int x, int y) : base(x, y)
        {
        }

        public static void StateCheck(Character mousePlayer)
        {
            if (mousePlayer.Position.X == Map.CheesePosition.X && mousePlayer.Position.Y == Map.CheesePosition.Y)
                TypeOfGameForm.currentGameState = GameState.MouseWin;
        }
    }
}