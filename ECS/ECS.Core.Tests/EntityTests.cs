using DL.ECS.Core.Exceptions;
using FluentAssertions;
using Xunit;

namespace DL.ECS.Core.Tests
{
    public class EntityCreationTests
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

            entity.TotalComponents.Should().Be(_totalComponents);
        }

        [Fact]

        public void Entities_AreCreated_WithComponentsInitialisedAsNull()
        {
            Context sut = CreateSut();
            IEntity entity = sut.Create();

            entity.TotalComponents.Should().Be(_totalComponents);

            for (int index = 0; index < _totalComponents; index++)
            {
                entity.GetComponent(index).Should().BeNull();
            }
        }

        private const int _totalComponents = 5;
        private Context CreateSut() => new Context(_totalComponents);
    }
}
