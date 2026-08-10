using GameEngine.Entities;

namespace GameEngine.Map
{
    public class Spawn
    {
        public int X { get; init; }
        
        public int Y { get; init; }

        EntityType EntityType { get; init; }

        public Spawn(int x, int y, EntityType entityType)
        {
            X = x;
            Y = y;
            EntityType = entityType;
        }
    }
}
