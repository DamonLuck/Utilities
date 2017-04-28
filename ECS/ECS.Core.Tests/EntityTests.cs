using FluentAssertions;
using Xunit;

namespace DL.ECS.Core.Tests
{
    public class EntityTests
    {
        [Fact]
        public void Entity_CanBe_Created()
        {
            Context sut = CreateSut();
            Entity entity = sut.Create();

            entity.Should().NotBeNull();
        }

        [Fact]
        public void Entity_HasA_UniqueId()
        {
            Context sut = CreateSut();
            Entity entity1 = sut.Create();
            Entity entity2 = sut.Create();

            entity1.Id.Should().NotBe(entity2.Id);
        }

        [Fact]
        public void EntityAfterRelease_HasA_UniqueId()
        {
            Context sut = CreateSut();
            Entity entity = sut.Create();
            long initialId = entity.Id;
            entity.Release();
            Entity e2 = sut.Create();

            initialId.Should().NotBe(e2.Id);
        }

        [Fact]
        public void EntityReleasedEvent_Raised_WhenEntityReleased()
        {
            Context sut = CreateSut();
            bool wasEventRaised = false;
            Entity entity = sut.Create();
            entity.EntityReleased += (_,__)=> wasEventRaised = true;

            entity.Release();

            wasEventRaised.Should().BeTrue();
        }

        private Context CreateSut() => new Context();
    }
}
