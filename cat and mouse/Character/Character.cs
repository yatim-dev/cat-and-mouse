using System;
using System.Drawing;

namespace cat_and_mouse.Domain
{
    public class Character
    {
        public Point Position;
        public bool Alive = true;
        
        public Character(int x, int y)
        {
            Position.X = x;
            Position.Y = y;
        }

        public void Move(int deltaX, int deltaY, Character character)
        {
            var deltaPosition = (deltaX, deltaY);
            deltaPosition = PhysicsMap.IsCollide(character, deltaPosition);
            Position.X += deltaPosition.deltaX;
            Position.Y += deltaPosition.deltaY;
        }
    }
}