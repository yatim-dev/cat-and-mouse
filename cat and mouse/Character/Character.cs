using System;
using System.Drawing;

namespace cat_and_mouse.Domain
{
    public class Character
    {
        public Point Position;
        public int deltaX;
        public int deltaY;
        public Character(int x, int y)
        {
            Position.X = x;
            Position.Y = y;
        }
    }
}