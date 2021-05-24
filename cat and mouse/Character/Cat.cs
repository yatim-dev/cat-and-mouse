namespace cat_and_mouse.Domain
{
    public class Cat : Character
    {
        public Cat(int x, int y) : base(x, y)
        {
        }

        public static void StateCheck(Character catPlayer, Character mousePlayer)
        {
            if (catPlayer.Position.X == mousePlayer.Position.X && catPlayer.Position.Y == mousePlayer.Position.Y)
                TypeOfGameForm.CurrentGameState = GameState.CatWin;
        }
    }
}