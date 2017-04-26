﻿namespace DL.ObjectPool.Tests
{
    public class TestObject : PooledObject<TestObject>, System.IEquatable<TestObject>
    {
        public int Foo { get; private set; }
        public string Bar { get; private set; }

        public static TestObject Create(int foo, string bar)
        {
            var result = _objectPool.Create();
            result.Foo = foo;
            result.Bar = bar;
            return result;
        }

        public bool Equals(TestObject other) => Foo == other.Foo && Bar == other.Bar;
    }
}
