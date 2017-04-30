using System.Collections.Generic;

namespace DL.ECS.Core
{
    public interface IRelation
    {
        RelationId RelationId { get; }
        EntityId PrimaryEntityId { get; }
        IRelation AddEntity(IEntity entity);
        IRelation AddEntities(IEnumerable<IEntity> entities);
        IRelation RemoveEntity(IEntity entity);
        IRelation RemoveEntities(IEnumerable<IEntity> entities);
        IEnumerable<IEntity> GetEntities();
    }
}