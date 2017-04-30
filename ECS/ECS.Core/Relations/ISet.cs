namespace DL.ECS.Core
{
    public interface ISet : IRelation
    {
        ISet AddPrimaryEntity(IEntity entity);
    }
}