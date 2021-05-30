namespace cat_and_mouse.Domain
{
    public enum GameState //состяния игры
    {
        Start,
        PlayerChoose,
        MapChoose,
        Game,
        Pause,
        CatWin,
        MouseWin,
        Draw
    }
}