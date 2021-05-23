namespace cat_and_mouse.Domain
{
    public class Cat : Character
    {
        public Cat(int x, int y) : base(x, y)
        {
        }

        public static void StateCheck(Character CatPlayer, Character MousePlayer)
        {
            if (CatPlayer.Position.X == MousePlayer.Position.X && CatPlayer.Position.Y == MousePlayer.Position.Y)
            {
                TypeOfGameForm.CurrentGameState = GameState.CatWin;
            }
        }
    }
}