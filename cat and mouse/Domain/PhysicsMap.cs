namespace cat_and_mouse.Domain
{
    public class PhysicsMap
    {
        public static void IsCollide(Character character)
        {
            if (character.Position.X + character.DeltaX < 0 ||
                character.Position.X + character.DeltaX >= Map.MapWidth ||
                character.Position.Y + character.DeltaY < 0 ||
                character.Position.Y + character.DeltaY >= Map.MapHeight ||
                Map.MapArray[character.Position.X + character.DeltaX, character.Position.Y + character.DeltaY] ==
                MapCell.Wall)
            {
                character.DeltaX = 0;
                character.DeltaY = 0;
            }
        }
    }
}