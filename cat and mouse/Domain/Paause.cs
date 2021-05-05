using System;
using System.Drawing;
using System.Windows.Forms;

namespace cat_and_mouse.Domain
{
    public static class PauseForm
    {
        public static void DrawPause(int Width, int Height, PaintEventArgs e)
        {
            var PauseForm = new Form();
            PauseForm.Width = Width;
            PauseForm.Height = Height;
            PauseForm.StartPosition = FormStartPosition.CenterScreen;
           //тут ошибка PauseForm.DrawToBitmap(TypeOfGameForm.Pause, Rectangle.FromLTRB(0, Width, 0, Height)); /*0, 0,Width, Height*/
            e.Graphics.DrawImage(TypeOfGameForm.Pause, 0,0);
            PauseForm.Show();
        }
    }
}