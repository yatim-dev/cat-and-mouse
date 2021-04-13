using System;
using System.Collections.Generic;
using System.Drawing;
using cat_and_mouse.Domain;
using Point = System.Drawing.Point;

namespace cat_and_mouse.Domain
{
    public class Map 
    {
        public readonly MapCell[,] MapArray;
        public readonly Point InitialPosition;
        public readonly Point Cheese;

        private Map(MapCell[,] mapArray, Point initialPosition, Point cheese)
        {
            MapArray = mapArray;
            InitialPosition = initialPosition;
            Cheese = cheese;
        }
		
        public static Map FromText(string text)
        {
            var lines = text.Split(new[] { "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            return FromLines(lines);
        }

        public static Map FromLines(string[] lines)
        {
            var mapCells = new MapCell[lines[0].Length, lines.Length];
            var initialPosition = Point.Empty;
            var cheese = Point.Empty;
            for (var y = 0; y < lines.Length; y++)
            {
                for (var x = 0; x < lines[0].Length; x++)
                {
                    switch (lines[y][x])
                    {
                        case '#':
                            mapCells[x, y] = MapCell.Wall;
                            break;
                        case 'P':
                            mapCells[x, y] = MapCell.Empty;
                            initialPosition = new Point(x, y);
                            break;
                        case 'C':
                            mapCells[x, y] = MapCell.Empty;
                            cheese = new Point(x, y);
                            break;
                        default:
                            mapCells[x, y] = MapCell.Empty;
                            break;
                    }
                }
            }
            return new Map(mapCells, initialPosition, cheese);
        }
    }
}