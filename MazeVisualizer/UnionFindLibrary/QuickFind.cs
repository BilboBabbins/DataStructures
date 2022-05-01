using System;
using System.Collections.Generic;
using System.Text;

namespace UnionFindLibrary
{
    public class QuickFind<T>
    {
        private int[] sets;
        private Dictionary<T, int> map;

        public QuickFind(IEnumerable<T> items)//linq
        {
            map = new Dictionary<T, int>();
            int count = 0;
            foreach (T item in items)
            {
                map.Add(item, count);
                count++;
            }
            sets = new int[count];
            for (int a = 0; a < count; a++)
            {
                sets[a] = a;
            }
        }

        public int Find(T p) => sets[map[p]];
        public bool Union(T p, T q)
        {
            if (!AreConnected(p, q))
            {
                int pSet = Find(p);
                int qSet = Find(q);

                for (int a = 0; a < sets.Length; a++)
                {
                    if (sets[a] == pSet)
                    {
                        sets[a] = qSet;
                    }
                }
                return true;
            }
            return false;

        }

        public bool AreConnected(T p, T q)
        {
            if (Find(p) == Find(q))
            {
                return true;
            }
            return false;
        }
    }
}

