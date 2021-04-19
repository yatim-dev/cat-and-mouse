namespace cat_and_mouse.Domain
{
    public class Character
    {
        public Vector Position;
        public bool Alive = true;//жив
        public double Speed { get; protected set; }//скорость
        
        //направление
        //
    }
}