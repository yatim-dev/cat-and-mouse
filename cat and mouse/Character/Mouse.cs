using System.Drawing;

namespace cat_and_mouse.Domain
{
    public class Mouse : Character
    {
        
        //победа если съела сыр
        //проигрыш если кошка съела мышь

        public Mouse(int x, int y, Image character) : base(x, y, character)
        {
        }
    }
}