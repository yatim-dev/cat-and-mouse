using System;
using System.Drawing;
using System.Windows.Forms;

namespace cat_and_mouse.Domain
{
    public class Mouse : Character
    {
        
        //победа если съела сыр
        //проигрыш если кошка съела мышь

        public Mouse(int x, int y) : base(x, y)
        {
            
        }

        public void StateCheck(Character mousePlayer)
        {
            if (mousePlayer.Position.X == Map.CheesePosition.X
                && mousePlayer.Position.Y == Map.CheesePosition.Y)
            {
                throw new Exception("Эмм.... дальше пилить надо");
                //TypeOfGameForm.ActiveForm.Mes
            }
        }
    }
}