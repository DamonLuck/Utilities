namespace DL.ObjectPool
{
    /// <summary>
    /// Base class for a pooled object
    /// 
    /// Requires a static factory method implementing for Create
    /// </summary>
    /// <example>
    /// public class TestObject : PooledObject<TestObject>
    /// {
    ///     public int Foo { get; private set; }
    ///     public string Bar { get; private set; }
    /// 
    ///     public static TestObject Create(int foo, string bar)
    ///     {
    ///         var result = _objectPool.Create();
    ///         result.Foo = foo;
    ///         result.Bar = bar;
    ///         return result;
    ///     }
    /// }
    /// </example>
    /// <typeparam name="TObjectType">The pooled object class</typeparam>
    public abstract class PooledObject<TObjectType> where TObjectType : class, new()
    {
        /// <summary>
        /// Releases this instance back to the object pool.
        /// Note: This does not destroy the object. Any references are still 
        /// valid but the object itself is invalid.
        /// Once released it's made available to future calls to create
        /// </summary>
        public virtual void Release() => _objectPool.Release(this as TObjectType);

        protected static ObjectPool<TObjectType> _objectPool =
            new ObjectPool<TObjectType>(() => new TObjectType());
    }
}