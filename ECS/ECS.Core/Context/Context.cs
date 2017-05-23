using System;
using System.Collections.Generic;
using System.Linq;
using DL.ECS.Core.Components;
using DL.ECS.Core.Entity;
using DL.ECS.Core.Exceptions;

namespace DL.ECS.Core.Context
{
    public class Context
    {
        private ComponentManager _componentManager;
        private Dictionary<EntityId, Entity.Entity> _entities = new Dictionary<EntityId, Entity.Entity>();
        private readonly int _totalComponents;

        public Context(IList<Type> componentLookup)
        {
            _totalComponents = componentLookup.Count;
            _componentManager = new ComponentManager(this, componentLookup);
        }

        public IEntity Create()
        {
            Entity.Entity newEntity = Entity.Entity.Create(_totalComponents, _componentManager);
            _entities.Add(newEntity.EntityId, newEntity);
            return newEntity;
        }

        public IEnumerable<IEntity> GetEntitiesByComponent<TComponent>()
            where TComponent : IComponent
            => _componentManager.GetEntities<TComponent>();

        public IEnumerable<IEntity> GetEntitiesByComponent<TComponent>(Func<TComponent, bool> predicate)
            where TComponent : IComponent
            => _componentManager
                    .GetEntities<TComponent>()
                    .Where(x => predicate(x.GetComponent< TComponent>()));

        public IEnumerable<TComponent> GetInstancesOfComponent<TComponent>(Func<TComponent, bool> predicate)
            where TComponent : IComponent
            => _componentManager
                    .GetEntities<TComponent>()
                    .Where(x => predicate(x.GetComponent<TComponent>()))
                    .Select(x => x.GetComponent<TComponent>());

        public IEnumerable<IEntity> GetAllEntities() => _entities.Values;
        public IEntity GetEntityById(EntityId entityId) => _entities[entityId];

        internal IEntity GetEntity(EntityId index)
        {
            return _entities[index];
        }

        public void Destroy(IEntity entity)
        {
            if (!_entities.ContainsKey(entity.EntityId))
                throw new EntityException($"Cannot destroy. Entity with "+
                    $"id {entity.EntityId} no longer exists", entity);

            Entity.Entity entityToDestroy = _entities[entity.EntityId];
            _componentManager.DestroyComponentRelations(entity);
            _entities.Remove(entity.EntityId);
            entityToDestroy.Release();
        }
    }
}