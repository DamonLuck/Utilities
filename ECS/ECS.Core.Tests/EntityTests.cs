using DL.ECS.Core.Components;
using DL.ECS.Core.Exceptions;
using DL.ECS.Core.Tests.TestComponents;
using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace DL.ECS.Core.Tests
{
    public class EntityCreationTests
    {
        public EntityCreationTests()
        {
            _componentDictionary.Add(typeof(PlayerComponent), new ComponentId(0));
            _componentDictionary.Add(typeof(TeamComponent), new ComponentId(1));
        }

        [Fact]
        public void Entity_CanBe_Created()
        {
            Context sut = CreateSut();
            IEntity entity = sut.Create();

            entity.Should().NotBeNull();
        }

        [Fact]
        public void Entity_HasA_UniqueId()
        {
            Context sut = CreateSut();
            IEntity entity1 = sut.Create();
            IEntity entity2 = sut.Create();

            entity1.EntityId.Should().NotBe(entity2.EntityId);
        }

        [Fact]
        public void EntityAfterRelease_HasA_UniqueId()
        {
            Context sut = CreateSut();
            IEntity entity = sut.Create();
            EntityId initialId = entity.EntityId;
            sut.Destroy(entity);
            IEntity e2 = sut.Create();

            initialId.Should().NotBe(e2.EntityId);
        }

        [Fact]
        public void Entities_DestroyedTwice_ThrowException()
        {
            Context sut = CreateSut();
            IEntity entity = sut.Create();
            EntityId initialId = entity.EntityId;
            sut.Destroy(entity);
            Assert.Throws(typeof(EntityException), () => sut.Destroy(entity));
        }

        [Fact]
        public void Entities_AreCreated_WithTotalComponentCount()
        {
            Context sut = CreateSut();
            IEntity entity = sut.Create();

            entity.TotalComponents.Should().Be(_componentDictionary.Count);
        }

        [Fact]

        public void Entities_AreCreated_WithComponentsInitialisedAsNull()
        {
            Context sut = CreateSut();
            IEntity entity = sut.Create();

            entity.TotalComponents.Should().Be(_componentDictionary.Count);

            entity.GetComponent<PlayerComponent>().Should().BeNull();
            entity.GetComponent<TeamComponent>().Should().BeNull();
        }

        private Context CreateSut() => new Context(_componentDictionary);

        private IDictionary<Type, ComponentId> _componentDictionary
            = new Dictionary<Type, ComponentId>();
    }
}
