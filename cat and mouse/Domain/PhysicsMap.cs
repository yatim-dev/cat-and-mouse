using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace cat_and_mouse.Domain
{
    public class PhysicsMap
    {
        public static void IsCollide(Character character)
        {
            
            if (character.Position.X + character.deltaX < 0 
                || character.Position.X + character.deltaX >= Map.MapWidth 
                || character.Position.Y + character.deltaY < 0
                || character.Position.Y + character.deltaY >= Map.MapHeight
                || Map.MapArray[character.Position.X + character.deltaX, character.Position.Y + character.deltaY] == MapCell.Wall)
            {
                character.deltaX = 0;
                character.deltaY = 0;
            }
        }
    }
}