using System;
using System.Collections.Generic;
using System.Linq;

namespace DL.ECS.Core
{
    internal class RelationManager
    {
        private Dictionary<EntityId, HashSet<RelationId>> _entityRelations =
            new Dictionary<EntityId, HashSet<RelationId>>();

        private Dictionary<RelationId, HashSet<EntityId>> _relationEntities =
            new Dictionary<RelationId, HashSet<EntityId>>();

        private Dictionary<RelationId, IRelation> _relations =
            new Dictionary<RelationId, IRelation>();
        private readonly Context _context;

        internal RelationManager(Context context)
        {
            _context = context;
        }

        internal void RegisterRelation(IRelation relation)
        {
            _relations.Add(relation.RelationId, relation);
            _relationEntities.Add(relation.RelationId, new HashSet<EntityId>());
        }

        internal void AddEntity(RelationId relationId, EntityId entityId)
        {
            if (!_entityRelations.ContainsKey(entityId))
                _entityRelations.Add(entityId, new HashSet<RelationId>());

            _entityRelations[entityId].Add(relationId);
            _relationEntities[relationId].Add(entityId);
        }

        internal void RemoveEntity(RelationId relationId, EntityId entityId)
        {
            if (_entityRelations.ContainsKey(entityId))
            {
                _entityRelations[entityId].Remove(relationId);
                _relationEntities[relationId].Remove(entityId);
            }
        }

        internal IEnumerable<IRelation> GetRelationsBy(Func<IRelation, bool> predicate)
        {
            return _relations.Values.Where(x => predicate(x));
        }

        internal IRelation GetRelationById(RelationId relationid)
            => _relations[relationid];

        internal IEnumerable<IEntity> GetEntities(RelationId relationid)
        {
            List<IEntity> entities = new List<IEntity>();
            _relationEntities[relationid].ToList().ForEach(
                entityId => entities.Add(_context.GetEntity(entityId)));

            return entities;
        }

        internal void DestroyEntityRelations(IEntity entity)
        {
            if (_entityRelations.ContainsKey(entity.EntityId))
            {
                IEnumerable<RelationId> relationsToClean =
                    _entityRelations[entity.EntityId]
                        .Select(x => x)
                        .Distinct();
                relationsToClean.ToList().ForEach(x => _relations[x].RemoveEntity(entity.EntityId));
                _entityRelations.Remove(entity.EntityId);
            }
        }
    }
}