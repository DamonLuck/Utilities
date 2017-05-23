using DL.ECS.Core.Exceptions;
using DL.ECS.Core.Tests.TestComponents;
using FluentAssertions;
using System;
using System.Collections.Generic;
using DL.ECS.Core.Entity;
using Xunit;

namespace DL.ECS.Core.Tests
{
    public class EntityCreationTests
    {
        public EntityCreationTests()
        {
            _componentLookup.Add(typeof(PlayerComponent));
            _componentLookup.Add(typeof(TeamComponent));
        }

        [Fact]
        public void Entity_CanBe_Created()
        {
            Context.Context sut = CreateSut();
            IEntity entity = sut.Create();

            entity.Should().NotBeNull();
        }

        [Fact]
        public void Entity_HasA_UniqueId()
        {
            Context.Context sut = CreateSut();
            IEntity entity1 = sut.Create();
            IEntity entity2 = sut.Create();

            entity1.EntityId.Should().NotBe(entity2.EntityId);
        }

        [Fact]
        public void EntityAfterRelease_HasA_UniqueId()
        {
            Context.Context sut = CreateSut();
            IEntity entity = sut.Create();
            EntityId initialId = entity.EntityId;
            sut.Destroy(entity);
            IEntity e2 = sut.Create();

            initialId.Should().NotBe(e2.EntityId);
        }

        [Fact]
        public void Entities_DestroyedTwice_ThrowException()
        {
            Context.Context sut = CreateSut();
            IEntity entity = sut.Create();
            sut.Destroy(entity);
            Assert.Throws(typeof(EntityException), () => sut.Destroy(entity));
        }

        [Fact]
        public void Entities_AreCreated_WithTotalComponentCount()
        {
            Context.Context sut = CreateSut();
            IEntity entity = sut.Create();

            entity.TotalComponents.Should().Be(_componentLookup.Count);
        }

        [Fact]

        public void Entities_AreCreated_WithComponentsInitialisedAsNull()
        {
            Context.Context sut = CreateSut();
            IEntity entity = sut.Create();

            entity.TotalComponents.Should().Be(_componentLookup.Count);

            entity.GetComponent<PlayerComponent>().Should().BeNull();
            entity.GetComponent<TeamComponent>().Should().BeNull();
        }

        private Context.Context CreateSut() => new Context.Context(_componentLookup);

        private IList<Type> _componentLookup
            = new List<Type>();
    }
}
