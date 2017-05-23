using FluentAssertions;
using System.Threading.Tasks;
using DL.ObjectPool.Tests.TestObjects;
using Xunit;

namespace DL.ObjectPool.Tests
{
    public class ObjectPoolTests
    {
        [Fact]
        public void Create_TwoSeperateObjects_AreNotEqual()
        {
            TestObject object1 = TestObject.Create(1, "Test1");
            TestObject object2 = TestObject.Create(2, "Test2");
            object1.Should().NotBe(object2);
            object1.Should().NotBeSameAs(object2);
        }

        [Fact]
        public void CreateReleaseThenCreate_Returns_TheSameObject()
        {
            TestObject object1 = TestObject.Create(1, "Test1");
            object1.Release();
            TestObject object2 = TestObject.Create(2, "Test2");
            object1.Should().Be(object2);
            object1.Should().BeSameAs(object2);
            object1.Foo.Should().Be(2);
            object1.Bar.Should().Be("Test2");
        }
        
        [Fact]
        public void ForParallelThreads_ReleaseItems_AreReusedSafely()
        {
            double ONE_PERCENT = 0.01;
            int TEN_THOUSAND_ITEMS = 10000;
            Parallel.For(0, TEN_THOUSAND_ITEMS, (i, loopState) =>
            {
                TestObjectWithoutReleaseCount.Create(1,"A");
                TestObjectWithReleaseCount.Create(1, "A").Release();
            });

            double percentageCreationDifference =
                TestObjectWithReleaseCount.ConstructorCalls
                / TestObjectWithoutReleaseCount.ConstructorCalls
                * 100;

            TestObjectWithReleaseCount.ConstructorCalls.Should()
                .BeLessThan(TestObjectWithoutReleaseCount.ConstructorCalls);

            percentageCreationDifference.Should().BeLessThan(ONE_PERCENT);
        }
    }
}
