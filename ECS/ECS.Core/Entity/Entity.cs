using DL.ObjectPool;
using DL.ECS.Core.Exceptions;
using System;
using System.Text;
using DL.ECS.Core.Components;
using DL.Infrastructure;

namespace DL.ECS.Core
{
    public interface IEntity
    {
        EntityId EntityId { get; }
        int TotalComponents { get; }

        IComponent GetComponent(ComponentId componentId);
        IEntity AddComponent(IComponent component, ComponentId componentId);
        IEntity RemoveComponent(ComponentId componentId);
    }

    internal class Entity : PooledObject<Entity>, IEntity
    {
        private static long _currentId;
        public EntityId EntityId { get; private set; }
        public int TotalComponents { get; private set; }
        private IComponent[] _components;
        private ComponentManager _componentManager;
        internal static Entity Create(int totalComponents, ComponentManager componentManager)
        {
            Entity entity = _objectPool.Create();
            entity.EntityId = new EntityId(_currentId++);
            entity.TotalComponents = totalComponents;
            entity._components = new IComponent[totalComponents];
            entity._componentManager = componentManager;

            AmbientLogger.SystemNotification.EntityCreated(entity);
            return entity;
        }

        public IComponent GetComponent(ComponentId componentId) => _components[componentId.Id];

        public IEntity AddComponent(IComponent component, ComponentId componentId)
        {
            if (_components[componentId.Id] != null)
                throw new EntityAlreadyHasComponentException(this, component, componentId.Id);

            _components[componentId.Id] = component;
            _componentManager.AddComponent(componentId, this.EntityId);
            return this;
        }

        public IEntity RemoveComponent(ComponentId componentId)
        {
            if (_components[componentId.Id] == null)
                throw new EntityDoesNotHaveComponentException(this, componentId.Id);

            _components[componentId.Id] = null;
            return this;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"Entity {EntityId.Id}\t");
            foreach(var component in _components)
            {
                if(component != null)
                    sb.Append($"Component {component.ToString()}\t");
            }

            return sb.ToString();
        }
    }
}
