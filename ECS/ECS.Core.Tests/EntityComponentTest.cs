using System;
using DL.ECS.Core.Components;
using DL.ECS.Core.Exceptions;
using FluentAssertions;
using NSubstitute;
using Xunit;
using System.Collections.Generic;
using DL.ECS.Core.Tests.TestComponents;

namespace DL.ECS.Core.Tests
{
    public class EntityComponentTest
    {
        public EntityComponentTest()
        {
            _componentDictionary.Add(typeof(PlayerComponent), new ComponentId(0));
            _componentDictionary.Add(typeof(TeamComponent), new ComponentId(1));
        }

        [Fact]
        public void AddComponent_ForGet_ReturnsComponent()
        {
            IComponent component = new TeamComponent();
            ComponentId index = new ComponentId(_componentDictionary.Count - 1);
            IComponentBuilder builder = new TestComponentBuilder(index, component);
            IEntity entity = CreateSut();
            entity.AddComponent<TeamComponent>(builder);

            entity.GetComponent<TeamComponent>().Should().Be(component);
        }

        [Fact]
        public void RemoveComponent_ForGet_ReturnsNull()
        {
            IComponent component = new TeamComponent();
            ComponentId index = new ComponentId(_componentDictionary.Count - 1);
            IComponentBuilder builder = new TestComponentBuilder(index, component);

            IEntity entity = CreateSut();
            entity.AddComponent<TeamComponent>(builder);
            entity.RemoveComponent(index);

            entity.GetComponent<TeamComponent>().Should().Be(null);
        }

        [Fact]
        public void AddComponent_Twice_ThrowsException()
        {
            IComponent component = new PlayerComponent();
            ComponentId index = new ComponentId(_componentDictionary.Count - 1);
            IComponentBuilder builder = new TestComponentBuilder(index, component);

            IEntity entity = CreateSut();
            entity.AddComponent<PlayerComponent>(builder);

            Assert.Throws(typeof(EntityAlreadyHasComponentException),
                () => entity.AddComponent<PlayerComponent>(builder));
        }

        [Fact]
        public void RemoveComponent_Twice_ThrowsException()
        {
            IComponent component = new TeamComponent();
            ComponentId index = new ComponentId(_componentDictionary.Count - 1);
            IComponentBuilder builder = new TestComponentBuilder(index, component);

            IEntity entity = CreateSut();
            entity.AddComponent<TeamComponent>(builder);
            entity.RemoveComponent(index);

            Assert.Throws(typeof(EntityDoesNotHaveComponentException),
                () => entity.RemoveComponent(index));
        }

        private Context _context => new Context(_componentDictionary);
        private IEntity CreateSut() => _context.Create();

        private IDictionary<Type, ComponentId> _componentDictionary
            = new Dictionary<Type, ComponentId>();
    }
}
