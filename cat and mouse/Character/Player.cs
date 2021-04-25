using System.Drawing;

namespace cat_and_mouse.Domain
{
    public class Player : Mouse
    {
        public Player(int x, int y, Image character) : base(x, y, character)
        {
        }
    }
}