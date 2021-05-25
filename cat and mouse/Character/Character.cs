using System.Drawing;

namespace cat_and_mouse.Domain
{
    public class Character
    {
        public Point Position;
        public int DeltaX;
        public int DeltaY;

        public Character(int x, int y)
        {
            Position.X = x;
            Position.Y = y;
        }
    }
}