namespace cat_and_mouse.Domain
{
    public struct Point
    {
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; }
        public int Y { get; }

        public static Point Empty => new Point(0,0);


        public override string ToString() => $"({X}, {Y})";
    }
}