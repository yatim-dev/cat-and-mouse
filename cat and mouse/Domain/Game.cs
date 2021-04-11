using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace cat_and_mouse.Domain
{
    public enum GameStage //состяния игры//
    {
        PlayerChoose,
        MapChoose,
        Game,
        EndGame
    }
    public class Game
    {
        public Player CatHuman;
        public Player MouseHuman;
        public Player CatAI;
        public Player MouseAI;
        public Tuple<Player> CurrentPlayers;// = new Tuple<Player>();
    }


    





}