using DL.ECS.Core.Exceptions;
using System.Collections.Generic;

namespace DL.ECS.Core
{
    public class Context
    {
        private RelationManager _relationManager;
        private Dictionary<EntityId, Entity> _entities = new Dictionary<EntityId, Entity>();
        private readonly int _totalComponents;

        public Context(int totalComponents)
        {
            _totalComponents = totalComponents;
            _relationManager = new RelationManager(this);
        }

        public IEntity Create()
        {
            Entity newEntity = Entity.Create(_totalComponents);
            _entities.Add(newEntity.EntityId, newEntity);
            return newEntity;
        }

        public IRelation CreateSet()
        {
            IRelation set = Set.Create(_relationManager);
            _relationManager.RegisterRelation(set);
            return set;
        }

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
            _entities.Remove(entity.EntityId);
            entityToDestroy.Release();
        }
    }
}