using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace cat_and_mouse.Domain
{
    public class BfsTask
    {
        private static IEnumerable<Point> CellChecker(Point point)
        {
            for (var dy = -1; dy <= 1; dy++)
            for (var dx = -1; dx <= 1; dx++)
                if (dx != 0 && dy != 0)
                    continue;
                else
                    yield return new Point {X = point.X + dx, Y = point.Y + dy};
        }

        private static bool DungeonCheck(Point point)
        {
            return point.X < 0 || point.X >= Map.MapWidth || point.Y < 0 || point.Y >= Map.MapHeight ||
                   Map.MapArray[point.X, point.Y] != MapCell.Empty;
        }

        public static IEnumerable<SinglyLinkedList<Point>> FindPaths(Point start, Point[] chests)
        {
            var visitedCell = new HashSet<Point> {start};
            var queue = new Queue<SinglyLinkedList<Point>>();
            queue.Enqueue(new SinglyLinkedList<Point>(start));

            while (queue.Count != 0)
            {
                var point = queue.Dequeue();
                if (DungeonCheck(point.Value)) continue;
                foreach (var newPoint in CellChecker(point.Value))
                {
                    if (!visitedCell.Contains(newPoint)) queue.Enqueue(new SinglyLinkedList<Point>(newPoint, point));
                    visitedCell.Add(newPoint);
                }

                if (chests.Contains(point.Value)) yield return point;
            }
        }
    }
}