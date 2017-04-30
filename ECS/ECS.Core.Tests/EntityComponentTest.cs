using DL.ECS.Core.Components;
using DL.ECS.Core.Exceptions;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace DL.ECS.Core.Tests
{
    public class EntityComponentTest
    {
        private Context _context = new Context(_totalComponents);

        [Fact]
        public void AddComponent_ForGet_ReturnsComponent()
        {
            IComponent component = Substitute.For<IComponent>();
            ComponentId index = new ComponentId(_totalComponents - 1);

            IEntity entity = CreateSut();
            entity.AddComponent(component, index);

            entity.GetComponent(index).Should().Be(component);
        }

        [Fact]
        public void RemoveComponent_ForGet_ReturnsNull()
        {
            IComponent component = Substitute.For<IComponent>();
            ComponentId index = new ComponentId(_totalComponents - 1);

            IEntity entity = CreateSut();
            entity.AddComponent(component, index);
            entity.RemoveComponent(index);

            entity.GetComponent(index).Should().Be(null);
        }

        [Fact]
        public void AddComponent_Twice_ThrowsException()
        {
            IComponent component = Substitute.For<IComponent>();
            ComponentId index = new ComponentId(_totalComponents - 1);

            IEntity entity = CreateSut();
            entity.AddComponent(component, index);

            Assert.Throws(typeof(EntityAlreadyHasComponentException),
                () => entity.AddComponent(component, index));
        }

        [Fact]
        public void RemoveComponent_Twice_ThrowsException()
        {
            IComponent component = Substitute.For<IComponent>();
            ComponentId index = new ComponentId(_totalComponents - 1);

            IEntity entity = CreateSut();
            entity.AddComponent(component, index);
            entity.RemoveComponent(index);

            Assert.Throws(typeof(EntityDoesNotHaveComponentException),
                () => entity.RemoveComponent(index));
        }

        private const int _totalComponents = 5;
        private IEntity CreateSut() => _context.Create();
    }
}
