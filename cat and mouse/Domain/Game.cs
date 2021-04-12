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
        public Player Cat;
        public Player Mouse;
        public Tuple<Player> CurrentPlayers;// = new Tuple<Player>();
    }


    





}