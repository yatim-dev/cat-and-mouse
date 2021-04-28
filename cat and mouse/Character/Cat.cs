using System;
using System.Drawing;

namespace cat_and_mouse.Domain
{
    public class Cat : Character
    {
        //победа если съел мышку
        //проигрыш время истекло или мышь победила 
        //управление отдельно
        public Cat(int x, int y) : base(x, y)
        {
        }

        public void StateCheck(Character CatPlayer, Character MousePlayer)
        {
            if (CatPlayer.Position.X == MousePlayer.Position.X
                && CatPlayer.Position.Y == MousePlayer.Position.Y)
            {
                throw new Exception("Бе-бе-бе");
            }
        }
    }
}