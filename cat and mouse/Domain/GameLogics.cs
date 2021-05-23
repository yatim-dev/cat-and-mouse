using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace cat_and_mouse.Domain
{
    public class GameLogics
    {
        private static IEnumerable<List<Point>> pathsToCheese;
        public static readonly List<Point> AutoWay = new();

        public static void CreateRestartButton(Button restart, Control.ControlCollection controls)
        {
            restart.Left = 0;
            restart.Top = 0;
            restart.Width = 150;
            restart.Height = 40;
            restart.Text = "Restart";
            controls.Add(restart);
            restart.Click += delegate
            {
                TypeOfGameForm.CurrentGameState = GameState.PlayerChoose;
                controls.Clear();
            };
        }

        public static void AutoMove(Character character)
        {
            var lastCharacterPosition = new Point(character.Position.X, character.Position.Y);
            var controlPoints = new List<Point>();
            if (TypeOfGameForm.CurrentPlayerState == PlayerState.MouseBot)
                controlPoints.Add(Map.CheesePosition);
            else
                controlPoints.Add(TypeOfGameForm.MousePlayer.Position);

            var controlPointsArray = new Point[controlPoints.Count];
            for (var i = 0; i < controlPointsArray.Length; i++)
            {
                controlPointsArray[i] = controlPoints.First();
                controlPoints.RemoveAt(0);
            }

            if (Map.MapArray[lastCharacterPosition.X, lastCharacterPosition.Y] == MapCell.Empty)
            {
                pathsToCheese = BfsTask.FindPaths(lastCharacterPosition, controlPointsArray)
                    .Select(x => x.ToList())
                    .ToList();
                foreach (var pathsToChest in pathsToCheese) pathsToChest.Reverse();
            }

            var cheesePaths = pathsToCheese.ToArray();
            foreach (var t in cheesePaths)
                for (var j = 0; j < cheesePaths[0].Count; j++)
                    AutoWay.Add(t[j]);
        }
    }
}