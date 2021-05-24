using System.Drawing;
using System.Windows.Forms.VisualStyles;
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

        public static void DrawMap(Graphics e)
        {
            var wall = new Bitmap(TypeOfGameForm.MainPath + @"\Pictures\wall.png");
            var empty = new Bitmap(TypeOfGameForm.MainPath + @"\Pictures\empty.png");
            for (var i = 0; i < MapWidth; i++)
            for (var j = 0; j < MapHeight; j++)
            {
                if (MapArray[i, j] == MapCell.Wall)
                {
                    wall.SetResolution(e.DpiX, e.DpiY);
                    e.DrawImage(wall, i * TypeOfGameForm.ElementSize, j * TypeOfGameForm.ElementSize);
                }

                if (MapArray[i, j] == MapCell.Empty)
                {
                    empty.SetResolution(e.DpiX, e.DpiY);
                    e.DrawImage(empty, i * TypeOfGameForm.ElementSize, j * TypeOfGameForm.ElementSize);
                }
            }
        }

        public static void ChangeMap(Character character)
        {
            for (var dy = -1; dy <= 1; dy++)
            for (var dx = -1; dx <= 1; dx++)
            {
                if(character.Position.X + dx < 0 || character.Position.X + dx >= MapWidth
                    || character.Position.Y + dy < 0 || character.Position.Y + dy >= MapHeight)
                    continue;
                MapArray[character.Position.X + dx, character.Position.Y + dy] = MapCell.Empty;
            }
            if (TypeOfGameForm.CurrentPlayerState == PlayerState.MouseBot)
            {
                GameLogics.AutoWay.Clear();
                GameLogics.AutoMove(character);
            }
        }
    }
}