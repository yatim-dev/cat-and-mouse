using System.Drawing;

namespace cat_and_mouse.Domain
{
    public class Level
    {
        public  MapCell[,] MapArray;
        public  Point CatPosition;
        public  Point MousePosition;
        public  Point CheesePosition;
    }
}