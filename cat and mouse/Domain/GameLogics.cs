using System.Windows.Forms;

namespace cat_and_mouse.Domain
{
    public class GameLogics
    {
        public static void CreateRestartButton(Button restart, Control.ControlCollection Controls)
        {
            restart.Left = 0;
            restart.Top = 0;
            restart.Text = "Restart";
            Controls.Add(restart);
            restart.Click += delegate
            {
                TypeOfGameForm.currentGameState = GameState.PlayerChoose;
                Controls.Clear();
            };
        } 
    }
}