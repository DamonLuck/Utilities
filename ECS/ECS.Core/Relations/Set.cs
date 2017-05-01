using DL.ObjectPool;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DL.ECS.Core
{
    public static class SetFunctions
    {
        public static Func<IRelation, bool> LookupByPrimaryEntity(EntityId primaryEntityId)
            => (x) => x is Set && ((Set)x).PrimaryEntityId == primaryEntityId;
    }

    internal class Set : PooledObject<Set>, ISet, IRelation
    {
        private static long _currentId;
        public RelationId RelationId { get; private set; }
        public static EntityId DefaultPrimaryEntityId = new EntityId(-1);
        public EntityId PrimaryEntityId { get; private set; }

        private static IEnumerable<long> Empty = new List<long>();
        private RelationManager _relationManager = null;

        internal static Set Create(RelationManager relationManager)
        {
            Set set = _objectPool.Create();
            set._relationManager = relationManager;
            set.RelationId = new RelationId(_currentId++);
            set._relationManager.RegisterRelation(set);
            set.PrimaryEntityId = DefaultPrimaryEntityId;
            return set;
        }

        public ISet AddPrimaryEntity(IEntity entity)
        {
            PrimaryEntityId = entity.EntityId;
            AddEntity(entity);
            return this;
        }

        public IRelation AddEntity(IEntity entity)
        {
            _relationManager.AddEntity(RelationId, entity.EntityId);
            return this;
        }

        public IRelation AddEntities(IEnumerable<IEntity> entities)
        {
            entities.ToList().ForEach(x => AddEntity(x));
            return this;
        }

        public IEnumerable<IEntity> GetEntities() => _relationManager.GetEntities(RelationId);

        public IRelation RemoveEntity(EntityId entityId)
        {
            _relationManager.RemoveEntity(RelationId, entityId);
            RemovePrimaryEntityId(entityId);
            return this;
        }

        private void RemovePrimaryEntityId(EntityId entityId)
        {
            if (PrimaryEntityId.Id == entityId.Id)
            {
                PrimaryEntityId = DefaultPrimaryEntityId;
            }
        }

        public IRelation RemoveEntities(IEnumerable<EntityId> entityIds)
        {
            entityIds.ToList().ForEach(entityId => RemoveEntity(entityId));
            return this;
        }
    }
}