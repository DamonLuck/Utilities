namespace DL.ObjectPool.Tests
{
    public class TestObjectWithoutReleaseCount : PooledObject<TestObjectWithoutReleaseCount>
    {
        public int Foo { get; private set; }
        public string Bar { get; private set; }

        public static int ConstructorCalls = 0;

        public TestObjectWithoutReleaseCount()
        {
            ConstructorCalls++;
        }

        public static TestObjectWithoutReleaseCount Create(int foo, string bar)
        {
            var result = _objectPool.Create();
            result.Foo = foo;
            result.Bar = bar;
            return result;
        }
    }
}
