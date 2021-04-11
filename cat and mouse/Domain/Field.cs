using System;

namespace cat_and_mouse.Domain
{
    public class Field : Cell
    {
        public Field(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public event Action Updated;

        public int Width { get; }
        public int Height { get; }
    }
}