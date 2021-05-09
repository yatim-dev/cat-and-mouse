using System.Windows.Forms;

namespace cat_and_mouse.Domain
{
    public class GameLogics
    {
        public static void CreateRestartButton(Button restart, Control.ControlCollection Controls)
        {
            restart.Left = 800;
            restart.Top = 350;
            restart.Text = "Restart";
            Controls.Add(restart);
            restart.Click += delegate
            {
                TypeOfGameForm.currentGameState = GameState.MapChoose;
                Controls.Clear();
            };
        } 
    }
}