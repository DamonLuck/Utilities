using DL.ObjectPool;
using DL.ECS.Core.Exceptions;
using System;

namespace DL.ECS.Core
{
    public struct EntityId
    {
        public EntityId(long id)
        {
            Id = id;
        }
        public long Id { get; }

        public override bool Equals(Object obj)
        {
            return obj is EntityId && this == (EntityId)obj;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static bool operator ==(EntityId x, EntityId y)
        {
            return x.Id == y.Id;
        }

        public static bool operator !=(EntityId x, EntityId y)
        {
            return !(x == y);
        }
    }

    public interface IEntity
    {
        EntityId EntityId { get; }
        int TotalComponents { get; }

        IComponent GetComponent(int index);
        void AddComponent(IComponent component, int index);
        void RemoveComponent(int index);
    }

    internal class Entity : PooledObject<Entity>, IEntity
    {
        private static long _currentId;
        public EntityId EntityId { get; private set; }
        public int TotalComponents { get; private set; }
        private IComponent[] _components;
        
        internal static Entity Create(int totalComponents)
        {
            Entity entity = _objectPool.Create();
            entity.EntityId = new EntityId(_currentId++);
            entity.TotalComponents = totalComponents;
            entity._components = new IComponent[totalComponents];
            return entity;
        }

        public IComponent GetComponent(int index) => _components[index];

        public void AddComponent(IComponent component, int index)
        {
            if (_components[index] != null)
                throw new EntityAlreadyHasComponentException(this, component, index);

            _components[index] = component;
        }

        public void RemoveComponent(int index)
        {
            if (_components[index] == null)
                throw new EntityDoesNotHaveComponentException(this, index);

            _components[index] = null;
        }
    }
}
