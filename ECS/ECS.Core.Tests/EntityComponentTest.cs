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
            TeamComponent component = new TeamComponent();
            IEntity entity = CreateSut();
            entity.AddComponent<TeamComponent>(component);

            entity.GetComponent<TeamComponent>().Should().Be(component);
        }

        [Fact]
        public void RemoveComponent_ForGet_ReturnsNull()
        {
            TeamComponent component = new TeamComponent();
            IEntity entity = CreateSut();
            entity.AddComponent<TeamComponent>(component);
            entity.RemoveComponent<TeamComponent>();

            entity.GetComponent<TeamComponent>().Should().Be(null);
        }

        [Fact]
        public void AddComponent_Twice_ThrowsException()
        {
            PlayerComponent component = new PlayerComponent();
            IEntity entity = CreateSut();
            entity.AddComponent<PlayerComponent>(component);

            Assert.Throws(typeof(EntityAlreadyHasComponentException),
                () => entity.AddComponent<PlayerComponent>(component));
        }

        [Fact]
        public void RemoveComponent_Twice_ThrowsException()
        {
            TeamComponent component = new TeamComponent();
            IEntity entity = CreateSut();
            entity.AddComponent<TeamComponent>(component);
            entity.RemoveComponent<TeamComponent>();

            Assert.Throws(typeof(EntityDoesNotHaveComponentException),
                () => entity.RemoveComponent<TeamComponent>());
        }

        private Context _context => new Context(_componentDictionary);
        private IEntity CreateSut() => _context.Create();

        private IDictionary<Type, ComponentId> _componentDictionary
            = new Dictionary<Type, ComponentId>();
    }
}
