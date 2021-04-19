using Point = System.Drawing.Point;

namespace cat_and_mouse.Domain
{
    public static class Map 
    {
        public static MapCell[,] MapArray;
        public static Point CatPosition;
        public static Point MousePosition;
        public static Point CheesePosition;
        public static int MapWidth;
        public static int MapHeight;

        public static void FromLines(string[] lines)
        {
            MapWidth = lines[0].Length;
            MapHeight = lines.Length;
            MapArray = new MapCell[MapWidth, MapHeight];
            for (var y = 0; y < lines.Length; y++)
            {
                for (var x = 0; x < lines[0].Length; x++)
                {
                    switch (lines[y][x])
                    {
                        case '#':
                            MapArray[x, y] = MapCell.Wall;
                            break;
                        case 'M':
                            MapArray[x, y] = MapCell.Empty;
                            MousePosition = new Point(x, y);
                            break;
                        case 'C':
                            MapArray[x, y] = MapCell.Empty;
                            CatPosition = new Point(x, y);
                            break;
                        case 'c':
                            MapArray[x, y] = MapCell.Empty;
                            CheesePosition = new Point(x, y);
                            break;
                        default:
                            MapArray[x, y] = MapCell.Empty;
                            break;
                    }
                }
            }
        }
    }
}