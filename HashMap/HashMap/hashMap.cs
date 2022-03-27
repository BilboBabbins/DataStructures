using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace HashMap
{
    class hashMap<TKey, TValue> : IDictionary<TKey, TValue>
    {
        private int count;
        public LinkedList<KeyValuePair<TKey, TValue>>[] buckets;
        public int Count => count;

        IEqualityComparer<TKey> comparer;

        public TValue this[TKey key]
        {
            get
            {
                int index = key.GetHashCode() % buckets.Length;
                if (ContainsKey(key))
                {
                    LinkedListNode<KeyValuePair<TKey, TValue>> curr = buckets[index].First;
                    while (curr != null)
                    {
                        if (curr.Value.Key.Equals(key))
                        {
                            return curr.Value.Value;
                        }
                        curr = curr.Next;
                    }
                }
                throw new Exception("key pair not there");
            }

            set
            {
                int index = key.GetHashCode() % buckets.Length;
                Remove(key);
                buckets[index].AddLast(new KeyValuePair<TKey, TValue>(key, value));
            }
            
        }

        public ICollection<TKey> Keys => throw new NotImplementedException();

        public ICollection<TValue> Values => throw new NotImplementedException();



        public bool IsReadOnly => throw new NotImplementedException();
        public hashMap(int count = 10, IEqualityComparer<TKey> comparer = null)
        {
            this.comparer = comparer;
            buckets = new LinkedList<KeyValuePair<TKey, TValue>>[count];
        }
        public void Add(TKey key, TValue value)
        {
            int index = key.GetHashCode() % buckets.Length;
            if (buckets[index] == null)
            {
                LinkedList<KeyValuePair<TKey, TValue>> newList = new LinkedList<KeyValuePair<TKey, TValue>>();

                newList.AddLast(new KeyValuePair<TKey, TValue>(key, value));
                buckets[index] = newList;

            }
            else
            {
                LinkedListNode<KeyValuePair<TKey, TValue>> current = buckets[index].First;
                //TKey currKey = buckets[index].First.Value.Key;
                while (current != null)
                {
                    if (current.Value.Key.Equals(key))
                    {
                        throw new Exception("duplicate");
                    }
                    current = current.Next;
                }
                buckets[index].AddLast(new KeyValuePair<TKey, TValue>(key, value));

            }
            count++;
            if (count >= buckets.Length)
            {
                ReHash();
            }
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            Add(item);
        }

        public void Clear()
        {
            for (int a = 0; a < buckets.Length; a++)
            {
                buckets[a] = null;
            }
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            int index = item.Key.GetHashCode() % buckets.Length;
            if (buckets[index] == null)
            {
                return false;
            }
            else
            {
                LinkedListNode<KeyValuePair<TKey, TValue>> current = buckets[index].First;
                while (current != null)
                {
                    if (current.Value.Equals(item))
                    {
                        return true;
                    }
                    current = current.Next;
                }
                return false;
            }
        }

        public bool ContainsKey(TKey key)
        {
            int index = key.GetHashCode() % buckets.Length;
            if (buckets[index] == null)
            {
                return false;
            }
            else
            {
                LinkedListNode<KeyValuePair<TKey, TValue>> current = buckets[index].First;
                while (current != null)
                {
                    if (current.Value.Key.Equals(key))
                    {
                        return true;
                    }
                    current = current.Next;
                }
                return false;
            }
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public void ReHash()
        {
            LinkedList<KeyValuePair<TKey, TValue>>[] newBuckets = new LinkedList<KeyValuePair<TKey, TValue>>[buckets.Length * 2];
            for (int a = 0; a < buckets.Length; a++)
            {
                if (buckets[a] != null)
                {
                    LinkedListNode<KeyValuePair<TKey, TValue>> curr = buckets[a].First;
                    while (curr != null)
                    {
                        int index = curr.Value.Key.GetHashCode() % newBuckets.Length;
                        KeyValuePair<TKey, TValue> newNode = new KeyValuePair<TKey, TValue>(curr.Value.Key, curr.Value.Value);
                        if (newBuckets[index] == null)
                        {
                            LinkedList<KeyValuePair<TKey, TValue>> newList = new LinkedList<KeyValuePair<TKey, TValue>>();
                            newList.AddLast(newNode);
                            newBuckets[index] = newList;
                        }
                        else
                        {
                            newBuckets[index].AddLast(newNode);
                        }
                        curr = curr.Next;
                    }
                }
            }
            buckets = newBuckets;

        }

        public bool Remove(TKey key)
        {
            
            if (ContainsKey(key))
            {
                int index = key.GetHashCode() % buckets.Length;
                LinkedListNode<KeyValuePair<TKey, TValue>> current = buckets[index].First;
                while (current != null)
                {
                    if (current.Value.Key.Equals(key))
                    {
                        buckets[index].Remove(current);
                        return true;
                    }
                    current = current.Next;

                }
            }
            return false;
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            throw new NotImplementedException();
        }

        public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
