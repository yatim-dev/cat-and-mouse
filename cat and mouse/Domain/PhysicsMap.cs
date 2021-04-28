using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace cat_and_mouse.Domain
{
    public class PhysicsMap
    {
        public static (int deltaX, int deltaY) IsCollide(Character character, (int deltaX, int deltaY) deltaPosition)
        {
            if (Map.MapArray[character.Position.X + deltaPosition.deltaX, character.Position.Y + deltaPosition.deltaY] == MapCell.Wall)
            {
                deltaPosition.deltaX = 0;
                deltaPosition.deltaY = 0;
            }
            

            return deltaPosition;
        }
    }
}