using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;

namespace cat_and_mouse.Domain
{
    public enum GameState //состяния игры
    {
        PlayerChoose,
        MapChoose,
        Game,
        Pause,
        EndGame
    }

    public class Game
    {
        private Stopwatch watch = new Stopwatch();
        public readonly List<Level> Levels;
        public long TotalTime;
        public GameState curentState { get; private set; } = GameState.PlayerChoose;
        private int curentLevelID;
        public Player Cat;
        public Player Mouse;

        public Level CurentLevel
        {
            get { return Levels[curentLevelID]; }
        }

        public Game(List<string> levelNames)
        {
            Levels = new List<Level>();
            //foreach (var name in levelNames)
               // Levels.Add(new Level(name));
        }

        public void PlayerChoose()
        {
            if (curentState == GameState.PlayerChoose)
            {
                curentState = GameState.MapChoose;
            }
            else
                throw new Exception("Попытка выбрать карту вне выбора игроков");
        }

        public void Menu()
        {
            if (curentState == GameState.PlayerChoose || curentState == GameState.MapChoose)
            {
                curentLevelID = 0;
                curentState = GameState.Game;
                watch.Start();
            }
            else
                throw new Exception("Попытка начать игру вне меню");
        }

        public void Pause()
        {
            switch (curentState)
            {
                case GameState.Game:
                    curentState = GameState.Pause;
                    watch.Stop();
                    break;
                case GameState.Pause:
                    curentState = GameState.Game;
                    watch.Start();
                    break;
                default:
                    throw new Exception("попытка зайти в паузу вне игры");
            }
        }

        public void EndGame()
        {
            if (curentLevelID == 0 && watch.ElapsedMilliseconds > 30000 ||
                curentLevelID == 1 && watch.ElapsedMilliseconds > 60000 ||
                curentLevelID == 2 && watch.ElapsedMilliseconds > 90000 || !Cat.Alive || !Mouse.Alive)
            {
                watch.Stop();
                TotalTime = watch.ElapsedMilliseconds;
                curentState = GameState.EndGame;
            }
            else
                throw new Exception("Попытка начать игру вне меню");
        }

        public void Restart()
        {
            curentState = GameState.PlayerChoose;
        }
    }
}