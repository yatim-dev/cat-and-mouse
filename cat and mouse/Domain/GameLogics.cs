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
            restart.Left = 10;
            restart.Top = 10;
            restart.Width = 150;
            restart.Height = 80;
            restart.Text = "Restart";
            restart.Font = new Font("Arial", 13);
            controls.Add(restart);
            restart.Click += delegate
            {
                TypeOfGameForm.CurrentGameState = GameState.PlayerChoose;
                controls.Clear();
            };
        }
        
        public static void CreateStartButton(Button start, Control.ControlCollection controls)
        {
            start.Left = 1250;
            start.Top = 950;
            start.Width = 500;
            start.Height = 100;
            start.Text = "Start the game";
            start.Font = new Font("Arial", 20);
            controls.Add(start);
            start.Click += delegate
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