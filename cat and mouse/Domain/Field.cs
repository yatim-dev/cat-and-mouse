using System;
using System.Collections.Generic;
using System.Drawing;

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
        
        /*
        public class Map
        {
            public readonly Cell[,] Dungeon;
            public readonly Point InitialPosition;
            public readonly Point Exit;
            public readonly Point[] Chests;

            private Map(Cell[,] dungeon, Point initialPosition, Point exit, Point[] chests)
            {
                Dungeon = dungeon;
                InitialPosition = initialPosition;
                Exit = exit;
                Chests = chests;
            }
		
            public static Map FromText(string text)
            {
                var lines = text.Split(new[] { "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
                return FromLines(lines);
            }

            public static Map FromLines(string[] lines)
            {
                var dungeon = new Cell[lines[0].Length, lines.Length];
                var initialPosition = Point.Empty;
                var exit = Point.Empty;
                var chests = new List<Point>();
                for (var y = 0; y < lines.Length; y++)
                {
                    for (var x = 0; x < lines[0].Length; x++)
                    {
                        switch (lines[y][x])
                        {
                            case '#':
                                dungeon[x, y] = Cell.Wall;
                                break;
                            case 'P':
                                dungeon[x, y] = Cell.Empty;
                                initialPosition = new Point(x, y);
                                break;
                            case 'C':
                                dungeon[x, y] = Cell.Empty;
                                chests.Add(new Point(x, y));
                                break;
                            case 'E':
                                dungeon[x, y] = Cell.Empty;
                                exit = new Point(x, y);
                                break;
                            default:
                                dungeon[x, y] = Cell.Empty;
                                break;
                        }
                    }
                }
                return new Map(dungeon, initialPosition, exit, chests.ToArray());
            }

            public bool InBounds(Point point)
            {
                var bounds = new Rectangle(0, 0, Dungeon.GetLength(0), Dungeon.GetLength(1));
                return bounds.Contains(point);
            }
        }
    }*/
    }
}