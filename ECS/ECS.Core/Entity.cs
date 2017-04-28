using DL.ObjectPool;
using System;

namespace ECS.Core
{
    public class Entity : PooledObject<Entity>
    {
        public event EventHandler<long> EntityReleased;
        private static long _currentId = 0;
        public long Id { get; private set; }

        internal static Entity Create()
        {
            Entity entity = _objectPool.Create();
            entity.Id = _currentId++;
            return entity;
        }

        public override void Release()
        {
            base.Release();
            EntityReleased?.Invoke(this, this.Id);
        }
    }
}
