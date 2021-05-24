using System.Drawing;

namespace cat_and_mouse.Domain
{
    public class Character
    {
        public Point Position;
        public int DeltaX;
        public int DeltaY;

        public Character(int x, int y)
        {
            Position.X = x;
            Position.Y = y;
        }
        
        public static void StateCheck(Character catPlayer, Character mousePlayer)
        {
            if (catPlayer.Position.X == mousePlayer.Position.X
                && catPlayer.Position.Y == mousePlayer.Position.Y
                && mousePlayer.Position.X == Map.CheesePosition.X
                && mousePlayer.Position.Y == Map.CheesePosition.Y)
                TypeOfGameForm.CurrentGameState = GameState.Draw;
        }
    }
}