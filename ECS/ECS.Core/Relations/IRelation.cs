using System.Collections.Generic;

namespace DL.ECS.Core
{
    public interface IRelation
    {
        RelationId RelationId { get; }
        IRelation AddEntity(IEntity entity);
        IRelation AddEntities(IEnumerable<IEntity> entities);
        IRelation RemoveEntity(EntityId entityId);
        IRelation RemoveEntities(IEnumerable<EntityId> entityIds);
        IEnumerable<IEntity> GetEntities();
    }
}