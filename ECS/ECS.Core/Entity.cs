using DL.ObjectPool;

namespace DL.ECS.Core
{
    public interface IEntity
    {
        long Id { get; }
        int TotalComponents { get; }

        IComponent GetComponent(int index);
    }

    internal class Entity : PooledObject<Entity>, IEntity
    {
        private static long _currentId;
        public long Id { get; private set; }
        public int TotalComponents { get; private set; }
        private IComponent[] _components;
        
        internal static Entity Create(int totalComponents)
        {
            Entity entity = _objectPool.Create();
            entity.Id = _currentId++;
            entity.TotalComponents = totalComponents;
            entity._components = new IComponent[totalComponents];
            return entity;
        }

        public IComponent GetComponent(int index) => _components[index];
    }
}
