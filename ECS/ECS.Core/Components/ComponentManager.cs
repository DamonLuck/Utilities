using System;
using System.Collections.Generic;
using System.Linq;

namespace DL.ECS.Core.Components
{
    public class ComponentManager
    {
        private Dictionary<ComponentId, HashSet<EntityId>> _componentEntityRelations =
            new Dictionary<ComponentId, HashSet<EntityId>>();
        private readonly Context _context;
        private readonly int _totalComponentCount;
        private readonly IDictionary<Type, ComponentId> _componentIdLookup;
        internal ComponentManager(Context context, IList<Type> componentIdLookup)
        {
            _context = context;
            _totalComponentCount = componentIdLookup.Count;
            _componentIdLookup = GenerateComponentIdLookup(componentIdLookup);
        }

        internal ComponentId GetId<TComponent>() where TComponent : IComponent
        {
            return _componentIdLookup[typeof(TComponent)];
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

        internal IEnumerable<IEntity> GetEntities<TComponent>()
            where TComponent : IComponent
        {
            ComponentId componentId = GetId<TComponent>();
            List <IEntity> entities = new List<IEntity>();
            _componentEntityRelations[componentId].ToList().ForEach(
                entityId => entities.Add(_context.GetEntity(entityId)));

            return entities;
        }

        internal void DestroyComponentRelations(IEntity entity)
        {
            for(int i = 0; i< _totalComponentCount; i++)
            {
                ComponentId componentId = new ComponentId(i);
                if(_componentEntityRelations.ContainsKey(componentId))
                    _componentEntityRelations[componentId].Remove(entity.EntityId);
            }
        }

        private IDictionary<Type, ComponentId> GenerateComponentIdLookup(IList<Type> types)
        {
            IDictionary<Type, ComponentId> componentLookup =
                new Dictionary<Type, ComponentId>();

            int currentId = 0;

            foreach (var componentType in types)
            {
                componentLookup.Add(componentType, new ComponentId(currentId));
                currentId++;
            }

            return componentLookup;
        }
    }
}
