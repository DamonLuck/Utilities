using DL.ECS.Core.Exceptions;
using System.Collections.Generic;

namespace DL.ECS.Core
{
    public class Context
    {
        private Dictionary<long, Entity> _entities = new Dictionary<long, Entity>();
        private readonly int _totalComponents;

        public Context(int totalComponents)
        {
            _totalComponents = totalComponents;
        }

        public IEntity Create()
        {
            Entity newEntity = Entity.Create(_totalComponents);
            _entities.Add(newEntity.Id, newEntity);
            return newEntity;
        }

        public void Destroy(IEntity entity)
        {
            if (!_entities.ContainsKey(entity.Id))
                throw new EntityException($"Cannot destroy. Entity with "+
                    $"id {entity.Id} no longer exists", entity);

            Entity entityToDestroy = _entities[entity.Id];
            _entities.Remove(entity.Id);
            entityToDestroy.Release();
        }
    }
}
