﻿using DL.ObjectPool;
using DL.ECS.Core.Exceptions;
using System;
using System.Text;

namespace DL.ECS.Core
{
    public class EntityId
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
        IEntity AddComponent(IComponent component, int index);
        IEntity RemoveComponent(int index);
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

        public IEntity AddComponent(IComponent component, int index)
        {
            if (_components[index] != null)
                throw new EntityAlreadyHasComponentException(this, component, index);

            _components[index] = component;
            return this;
        }

        public IEntity RemoveComponent(int index)
        {
            if (_components[index] == null)
                throw new EntityDoesNotHaveComponentException(this, index);

            _components[index] = null;
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
