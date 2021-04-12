using System.Collections.Generic;

namespace cat_and_mouse.Domain
{
    public class Cell
    {
        public List<Cell> CellStatus { get; set; }
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
}