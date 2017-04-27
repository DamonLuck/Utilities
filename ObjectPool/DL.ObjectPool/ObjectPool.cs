using System;
using System.Collections.Concurrent;

namespace DL.ObjectPool
{
    /// <summary>
    /// A thread safe object pool
    /// </summary>
    /// <typeparam name="TObjectType">The pooled object class</typeparam>
    public class ObjectPool<TObjectType> where TObjectType : class
    {
        private readonly Func<TObjectType> _createEmpty;
        private readonly ConcurrentBag<TObjectType> _pooledObjects;

        internal ObjectPool(Func<TObjectType> createEmpty)
        {
            _pooledObjects = new ConcurrentBag<TObjectType>();
            _createEmpty = createEmpty ?? throw new ArgumentNullException(nameof(createEmpty));
        }

        public TObjectType Create()
        {
            TObjectType newObject;
            _pooledObjects.TryTake(out newObject);

            if(newObject == null)
                newObject = _createEmpty();

            return newObject;
        }

        internal void Release(TObjectType oldObject)
        {
            _pooledObjects.Add(oldObject);
        }
    }
}