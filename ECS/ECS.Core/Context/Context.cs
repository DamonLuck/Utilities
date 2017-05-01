using DL.ECS.Core.Components;
using DL.ECS.Core.Exceptions;
using System;
using System.Collections.Generic;

namespace DL.ECS.Core
{
    public class Context
    {
        private RelationManager _relationManager;
        private ComponentManager _componentManager;
        private Dictionary<EntityId, Entity> _entities = new Dictionary<EntityId, Entity>();
        private readonly int _totalComponents;

        public Context(IDictionary<Type, ComponentId> componentLookup)
        {
            _totalComponents = componentLookup.Count;
            _relationManager = new RelationManager(this);
            _componentManager = new ComponentManager(this, componentLookup);
        }

        public IEntity Create()
        {
            Entity newEntity = Entity.Create(_totalComponents, _componentManager);
            _entities.Add(newEntity.EntityId, newEntity);
            return newEntity;
        }

        public ISet CreateSet() => Set.Create(_relationManager);

        public IEnumerable<IRelation> GetRelationsBy(Func<IRelation, bool> predicate)
            => _relationManager.GetRelationsBy(predicate);

        public IEnumerable<IEntity> GetEntitiesByComponent(ComponentId componentId)
            => _componentManager.GetEntities(componentId);
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
            _relationManager.DestroyEntityRelations(entity);
            _componentManager.DestroyComponentRelations(entity);
            _entities.Remove(entity.EntityId);
            entityToDestroy.Release();
        }
    }
}