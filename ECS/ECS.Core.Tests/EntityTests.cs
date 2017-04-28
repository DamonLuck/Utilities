using FluentAssertions;
using Xunit;

namespace ECS.Core.Tests
{
    public class EntityTests
    {
        [Fact]
        public void Entity_CanBe_Created()
        {
            Entity sut = Entity.Create();
        }

        [Fact]
        public void Entity_HasA_UniqueId()
        {
            Entity e1 = Entity.Create();
            Entity e2 = Entity.Create();

            e1.Id.Should().NotBe(e2.Id);
        }

        [Fact]
        public void EntityAfterRelease_HasA_UniqueId()
        {
            Entity e1 = Entity.Create();
            long initialId = e1.Id;
            e1.Release();
            Entity e2 = Entity.Create();
            initialId.Should().NotBe(e2.Id);
        }

        [Fact]
        public void EntityReleasedEvent_Raised_WhenEntityReleased()
        {
            bool wasEventRaised = false;
            Entity sut = Entity.Create();
            sut.EntityReleased += (_,__)=> wasEventRaised = true;

            sut.Release();

            wasEventRaised.Should().BeTrue();
        }
    }
}
