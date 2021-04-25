using System.Drawing;

namespace cat_and_mouse.Domain
{
    public class Character
    {
        public Point position;
        public bool Alive = true;
        public int Size;
        
        public Character(int x, int y)
        {
            position.X = x;
            position.Y = y;
            Size = 40;
        }

        public void Move(int deltaX, int deltaY)
        {
            //PhysicsMap.IsCollide(TypeOfGameForm.)
            position.X += deltaX;
            position.Y += deltaY;
        }
    }
}