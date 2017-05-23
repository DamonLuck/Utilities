namespace DL.ObjectPool.Tests.TestObjects
{
    public class TestObjectWithReleaseCount : PooledObject<TestObjectWithReleaseCount>
    {
        public int Foo { get; private set; }
        public string Bar { get; private set; }

        public static int ConstructorCalls;

        public TestObjectWithReleaseCount()
        {
            ConstructorCalls++;
        }

        public static TestObjectWithReleaseCount Create(int foo, string bar)
        {
            var result = _objectPool.Create();
            result.Foo = foo;
            result.Bar = bar;
            return result;
        }
    }
}
