using System.Drawing;

namespace cat_and_mouse.Domain
{
    public class Character
    {
        public Point position;
        public Image character;
        public bool Alive = true;//жив
        public int Size;
        
        public Character(int x, int y, Image character)
        {
            position.X = x;
            position.Y = y;
            this.character = character;
            Size = 40;
        }

        public void Move(int deltaX, int deltaY)
        {
            position.X += deltaX;
            position.Y += deltaY;
        }
    }
}