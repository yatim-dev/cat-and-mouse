using System.Drawing;

namespace cat_and_mouse.Domain
{
    public class Character
    {
        public int DeltaX;
        public int DeltaY;
        public Point Position;

        protected Character(int x, int y)
        {
            Position.X = x;
            Position.Y = y;
        }
    }
}