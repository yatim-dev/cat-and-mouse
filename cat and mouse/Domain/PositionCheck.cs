namespace cat_and_mouse.Domain
{
    public static class PositionCheck
    {
        public static void StateCheck(Character catPlayer, Character mousePlayer)
        {
            if (catPlayer.Position.X == mousePlayer.Position.X
                && catPlayer.Position.Y == mousePlayer.Position.Y
                && mousePlayer.Position.X == Map.CheesePosition.X
                && mousePlayer.Position.Y == Map.CheesePosition.Y)
                TypeOfGameForm.CurrentGameState = GameState.Draw;
        }
        
        public static void StateCatWinCheck(Character catPlayer, Character mousePlayer)
        {
            if (catPlayer.Position.X == mousePlayer.Position.X && catPlayer.Position.Y == mousePlayer.Position.Y)
                TypeOfGameForm.CurrentGameState = GameState.CatWin;
        }
        
        public static void StateMouseWinCheck(Character character)
        {
            if (character.Position.X == Map.CheesePosition.X && character.Position.Y == Map.CheesePosition.Y)
                TypeOfGameForm.CurrentGameState = GameState.MouseWin;
        }
    }
}