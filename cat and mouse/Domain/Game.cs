﻿using System.Collections.Generic;
using System.Drawing;

namespace cat_and_mouse.Domain
{
    public class Game
    {
        public Player CatHuman;
        public Player MouseHuman;
        public Player CatAI;
        public Player MouseAI;
    }

    public class Field : Cell
    {
        public int Width { get; set; }
        public int Height { get; set; }
        //размер
        //тИМА! пососи - губой тряси
        
    }
    
    public class Cell
    {
        public List<Cell> CellStatusCat { get; set; }
        /*
         * public List<Cell> CellStatusCat { get; set; }
         *    while(true){
         *        if(CellStatusCat.Length > 5)
         *         {
         *            CellStatusCat.Remove();
         *            CellStatusCat.Add(CatPosition);
         *         }
         *         CellStatusCat.Add(CatPosition);
         * }
         *
         * 
         */
        //сыр/очки в клетке
        //?? где рисовать забор
        //состояние клетки (кто и когда ходил/можно ли ходить) реализовать очередь
    }

    public class Player
    {
        
    }

    public class Character
    {
        //появление мышки и кошки
        //управление
        //если сходили на клетку, то некоторое время вернуться нельзя
    }
    
    public class Mouse : Character
    {
        public Point MousePosition;
        
        //победа если съела сыр
        //проигрыш если кошка съела мышь
    }

    public class Cat : Character
    {
        public Point CatPosition;
        
        //победа если съел мышку
        //проигрыш время истекло или мышь победила 
    }
    
    
}