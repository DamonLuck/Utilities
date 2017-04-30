using System;
using System.Collections.Generic;

namespace DL.ECS.Core
{
    public class RelationId
    {
        public RelationId(long id)
        {
            Id = id;
        }
        public long Id { get; }

        public override bool Equals(Object obj)
        {
            return obj is RelationId && this == (RelationId)obj;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static bool operator ==(RelationId x, RelationId y)
        {
            return x.Id == y.Id;
        }

        public static bool operator !=(RelationId x, RelationId y)
        {
            return !(x == y);
        }
    }
}