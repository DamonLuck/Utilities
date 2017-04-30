using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            return x.Id == y.Id;
        }

        public static bool operator !=(ComponentId x, ComponentId y)
        {
            return !(x == y);
        }
    }

    public class ComponentManager
    {
        private Dictionary<ComponentId, HashSet<EntityId>> _componentEntityRelations =
            new Dictionary<ComponentId, HashSet<EntityId>>();
        private readonly Context _context;
        private readonly int _totalComponentCount;

        internal ComponentManager(Context context, int totalComponentCount)
        {
            _context = context;
            _totalComponentCount = totalComponentCount;
        }

        public void AddComponent(ComponentId componentId, EntityId entityId)
        {
            if (!_componentEntityRelations.ContainsKey(componentId))
                _componentEntityRelations.Add(componentId, new HashSet<EntityId>());

            _componentEntityRelations[componentId].Add(entityId);
        }

        public void RemoveComponent(ComponentId componentId, EntityId entityId)
        {
            if (_componentEntityRelations.ContainsKey(componentId))
            {
                _componentEntityRelations[componentId].Remove(entityId);
            }
        }

        internal IEnumerable<IEntity> GetEntities(ComponentId componentId)
        {
            List<IEntity> entities = new List<IEntity>();
            _componentEntityRelations[componentId].ToList().ForEach(
                entityId => entities.Add(_context.GetEntity(entityId)));

            return entities;
        }

        internal void DestroyComponentRelations(IEntity entity)
        {
            for(int i = 0; i< _totalComponentCount; i++)
            {
                if (entity.GetComponent(new ComponentId(i)) != null)
                {
                    _componentEntityRelations[new ComponentId(i)].Remove(entity.EntityId);
                }
            }
        }
    }
}
