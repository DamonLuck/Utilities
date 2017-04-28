using DL.ECS.Core.Exceptions;
using FluentAssertions;
using System;
using Xunit;

namespace DL.ECS.Core.Tests
{
    public class EntityTests
    {
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

            entity1.Id.Should().NotBe(entity2.Id);
        }

        [Fact]
        public void EntityAfterRelease_HasA_UniqueId()
        {
            Context sut = CreateSut();
            IEntity entity = sut.Create();
            long initialId = entity.Id;
            sut.Destroy(entity);
            IEntity e2 = sut.Create();

            initialId.Should().NotBe(e2.Id);
        }

        [Fact]
        public void Entities_DestroyedTwice_ThrowException()
        {
            Context sut = CreateSut();
            IEntity entity = sut.Create();
            long initialId = entity.Id;
            sut.Destroy(entity);
            Assert.Throws(typeof(EntityException), () => sut.Destroy(entity));
        }

        private Context CreateSut() => new Context();
    }
}
