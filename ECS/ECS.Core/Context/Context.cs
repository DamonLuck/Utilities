using DL.ECS.Core.Components;
using DL.ECS.Core.Exceptions;
using System;
using System.Linq;
using System.Collections.Generic;

namespace DL.ECS.Core
{
    public class Context
    {
        private ComponentManager _componentManager;
        private Dictionary<EntityId, Entity> _entities = new Dictionary<EntityId, Entity>();
        private readonly int _totalComponents;

        public Context(IList<Type> componentLookup)
        {
            _totalComponents = componentLookup.Count;
            _componentManager = new ComponentManager(this, componentLookup);
        }

        public IEntity Create()
        {
            Entity newEntity = Entity.Create(_totalComponents, _componentManager);
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

        public IEnumerable<IEntity> GetAllEntities() => _entities.Values;

        internal IEntity GetEntity(EntityId index)
        {
            return _entities[index];
        }

        public void Destroy(IEntity entity)
        {
            if (!_entities.ContainsKey(entity.EntityId))
                throw new EntityException($"Cannot destroy. Entity with "+
                    $"id {entity.EntityId} no longer exists", entity);

            Entity entityToDestroy = _entities[entity.EntityId];
            _componentManager.DestroyComponentRelations(entity);
            _entities.Remove(entity.EntityId);
            entityToDestroy.Release();
        }
    }
}