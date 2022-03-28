using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchiverHuffmanMethod
{
    class PriorityQueue <T>
    {
        private int _size;
        public int Size => _size;

        private SortedDictionary<int, Queue<T>> _storage = new SortedDictionary<int, Queue<T>>();

        public PriorityQueue()
        {

        }

        public void Enqueue(int prioriry, T item)
        {
            if(_storage.ContainsKey(prioriry) == false)
                _storage.Add(prioriry, new Queue<T>());

            _storage[prioriry].Enqueue(item);
            _size++;
        }

        public T Dequeue()
        {
            if (_size == 0)
                throw new SystemException("Queue is empty");

            _size--;

            foreach(Queue<T> queue in _storage.Values)
            {
                if (queue.Count > 0)
                    return queue.Dequeue();
            }

            throw new SystemException("Queue Error"); 
        }
    }
}
