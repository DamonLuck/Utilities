using DL.ObjectPool;

namespace DL.ECS.Core
{
    public interface IEntity
    {
        long Id { get; }
    }

    internal class Entity : PooledObject<Entity>, IEntity
    {
        private static long _currentId = 0;
        public long Id { get; private set; }

        internal static Entity Create()
        {
            Entity entity = _objectPool.Create();
            entity.Id = _currentId++;
            return entity;
        }
    }
}
