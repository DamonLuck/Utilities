using System;

namespace DL.ECS.Core.Components
{
    public class ComponentId
    {
        public ComponentId(long id)
        {
            Id = id;
        }
        public long Id { get; }

        public override bool Equals(Object obj)
        {
            return obj is ComponentId && this == (ComponentId)obj;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static bool operator ==(ComponentId x, ComponentId y)
        {
            return y != null && (x != null && x.Id == y.Id);
        }

        public static bool operator !=(ComponentId x, ComponentId y)
        {
            return !(x == y);
        }
    }
}
