using System;
using FluentAssertions;
using Xunit;
using System.Collections.Generic;
using DL.ECS.Core.Entity;
using DL.ECS.Core.Exceptions.Entity;
using DL.ECS.Core.Tests.TestComponents;

namespace DL.ECS.Core.Tests
{
    public class EntityComponentTest
    {
        public EntityComponentTest()
        {
            _componentLookup.Add(typeof(PlayerComponent));
            _componentLookup.Add(typeof(TeamComponent));
        }

        [Fact]
        public void AddComponent_ForGet_ReturnsComponent()
        {
            TeamComponent component = new TeamComponent();
            IEntity entity = CreateSut();
            entity.AddComponent(component);

            entity.GetComponent<TeamComponent>().Should().Be(component);
        }

        [Fact]
        public void RemoveComponent_ForGet_ReturnsNull()
        {
            TeamComponent component = new TeamComponent();
            IEntity entity = CreateSut();
            entity.AddComponent(component);
            entity.RemoveComponent<TeamComponent>();

            entity.GetComponent<TeamComponent>().Should().Be(null);
        }

        [Fact]
        public void AddComponent_Twice_ThrowsException()
        {
            PlayerComponent component = new PlayerComponent();
            IEntity entity = CreateSut();
            entity.AddComponent(component);

            Assert.Throws(typeof(EntityAlreadyHasComponentException),
                () => entity.AddComponent(component));
        }

        [Fact]
        public void RemoveComponent_Twice_ThrowsException()
        {
            TeamComponent component = new TeamComponent();
            IEntity entity = CreateSut();
            entity.AddComponent(component);
            entity.RemoveComponent<TeamComponent>();

            Assert.Throws(typeof(EntityDoesNotHaveComponentException),
                () => entity.RemoveComponent<TeamComponent>());
        }

        private Context.Context Context => new Context.Context(_componentLookup);
        private IEntity CreateSut() => Context.Create();

        private IList<Type> _componentLookup
            = new List<Type>();
    }
}
