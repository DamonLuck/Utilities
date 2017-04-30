using DL.ObjectPool;
using DL.ECS.Core.Exceptions;
using System;
using System.Text;
using DL.ECS.Core.Components;

namespace DL.ECS.Core
{
    public class EntityId
    {
        public EntityId(long id)
        {
            Id = id;
        }
        public long Id { get; }

        public override bool Equals(Object obj)
        {
            return obj is EntityId && this == (EntityId)obj;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static bool operator ==(EntityId x, EntityId y)
        {
            return x.Id == y.Id;
        }

        public static bool operator !=(EntityId x, EntityId y)
        {
            return !(x == y);
        }
    }
}
