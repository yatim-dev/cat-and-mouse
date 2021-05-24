namespace cat_and_mouse.Domain
{
    public class Mouse : Character
    {
        public Mouse(int x, int y) : base(x, y)
        {
        }

        public static void StateCheck(Character character)
        {
            if (character.Position.X == Map.CheesePosition.X && character.Position.Y == Map.CheesePosition.Y)
                TypeOfGameForm.CurrentGameState = GameState.MouseWin;
        }
    }
}