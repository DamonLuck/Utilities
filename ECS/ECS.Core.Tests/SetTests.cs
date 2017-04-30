using FluentAssertions;
using Xunit;

namespace DL.ECS.Core.Tests
{
    public class SetTests
    {
        [Fact]
        public void Entity_CanBe_AddedToASet()
        {
            Context sut = CreateSut();
            IEntity entity1 = sut.Create();
            IRelation set = sut.CreateSet()
                .AddEntity(entity1);

            set.GetEntities().Should().BeEquivalentTo(new[] { entity1 });
        }

        [Fact]
        public void MultipleEntities_CanBe_AddedToASet()
        {
            Context sut = CreateSut();
            IEntity entity1 = sut.Create();
            IEntity entity2 = sut.Create();
            IRelation set = sut.CreateSet()
                .AddEntity(entity1)
                .AddEntity(entity2);

            set.GetEntities().Should().BeEquivalentTo(new[] { entity1, entity2 });
        }

        [Fact]
        public void GroupsOfEntities_CanBe_AddedToASet()
        {
            Context sut = CreateSut();
            IEntity entity1 = sut.Create();
            IEntity entity2 = sut.Create();
            IRelation set = sut.CreateSet()
                .AddEntities(new[] { entity1, entity2 });

            set.GetEntities().Should().BeEquivalentTo(new[] { entity1, entity2 });
        }

        [Fact]
        public void Entities_CanBeRemoved_FromASet()
        {
            Context sut = CreateSut();
            IEntity entity1 = sut.Create();
            IEntity entity2 = sut.Create();

            IRelation set = sut.CreateSet()
                .AddEntities(new[] { entity1, entity2 })
                .RemoveEntity(entity1.EntityId);

            set.GetEntities().Should().BeEquivalentTo(new[] { entity2 });
        }

        [Fact]
        public void DestroyedEntities_AreRemoved_FromASet()
        {
            Context sut = CreateSut();
            IEntity entity1 = sut.Create();
            IEntity entity2 = sut.Create();
            IRelation set = sut.CreateSet()
                .AddEntities(new[] { entity1, entity2 });

            sut.Destroy(entity1);
                
            set.GetEntities().Should().BeEquivalentTo(new[] { entity2 });
        }

        [Fact]
        public void PrimaryEntity_CountsAsOneEntity_InASet()
        {
            Context sut = CreateSut();
            IEntity entity1 = sut.Create();
            IEntity entity2 = sut.Create();
            ISet set = sut.CreateSet();
            set.AddPrimaryEntity(entity1)
                .AddEntity(entity2);

            set.GetEntities().Should().BeEquivalentTo(new[] { entity1, entity2 });
            set.PrimaryEntityId.Should().Be(entity1.EntityId);
        }

        [Fact]
        public void PrimaryEntity_CanBeReplaced_InASet()
        {
            Context sut = CreateSut();
            IEntity entity1 = sut.Create();
            IEntity entity2 = sut.Create();
            ISet set = sut.CreateSet();
            set.AddPrimaryEntity(entity1)
                .AddPrimaryEntity(entity2);

            set.GetEntities().Should().BeEquivalentTo(new[] { entity1, entity2 });
            set.PrimaryEntityId.Should().Be(entity2.EntityId);
        }

        [Fact]
        public void RemovedPrimaryEntity_ReturnsDefaultId_ForPrimaryEntityId()
        {
            Context sut = CreateSut();
            IEntity entity1 = sut.Create();
            ISet set = sut.CreateSet();
            set.AddPrimaryEntity(entity1);

            sut.Destroy(entity1);

            set.PrimaryEntityId.Should().Be(Set.DefaultPrimaryEntityId);
        }

        private const int _totalComponents = 5;
        private Context CreateSut() => new Context(_totalComponents);
    }
}
