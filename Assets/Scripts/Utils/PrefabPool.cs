using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    /**
     * A pool for MonoBehaviour prefabs.
     */
    public class PrefabPool<T> where T : MonoBehaviour
    {
        private readonly Queue<T> _inactive = new();
        private readonly List<T> _active = new();
        private readonly T _prefab;
        private readonly Transform _parent;
        private readonly int _maxSize;

        /**
         * Initializes a pool of prefabs of given type with predefined parameters of size.
         */
        public PrefabPool(T prefab, Transform parent = null, int initialSize = 10, int maxSize = 50)
        {
            _prefab = prefab;
            _parent = parent;
            _maxSize = maxSize;

            for (int i = 0; i < initialSize; i++)
                _inactive.Enqueue(Create());
        }

        /**
         * Creates a GameObject in the pool of prefab type.
         */
        private T Create()
        {
            var obj = Object.Instantiate(_prefab, _parent);
            obj.gameObject.SetActive(false);
            return obj;
        }

        /**
         * Returns the first object in pool queue or reuses the earliest activated object.  
         */
        public T Get()
        {
            T obj;
            if (_inactive.Count > 0)
            {
                obj = _inactive.Dequeue();
            }
            else if (_active.Count + _inactive.Count < _maxSize)
            {
                obj = Create();
            }
            else
            {
                obj = _active[0];
                _active.RemoveAt(0);
            }

            _active.Add(obj);
            obj.gameObject.SetActive(true);
            return obj;
        }

        /**
         * Returns an object in the pool.
         */
        public void Return(T obj)
        {
            if (obj == null) return;
            obj.gameObject.SetActive(false);
            _active.Remove(obj);
            _inactive.Enqueue(obj);
        }

        /**
         * Returns all objects back to pool.
         */
        public void ReturnAll()
        {
            foreach (var obj in _active)
            {
                if (obj != null)
                {
                    obj.gameObject.SetActive(false);
                    _inactive.Enqueue(obj);
                }
            }
            _active.Clear();
        }
    }
} 