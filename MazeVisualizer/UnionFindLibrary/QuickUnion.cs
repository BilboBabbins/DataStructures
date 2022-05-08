using System;
using System.Collections.Generic;
using System.Linq;

namespace UnionFindLibrary
{
    public class QuickUnion<T>
    {
        public int[] parents;
        public Dictionary<T, int> map;

        public QuickUnion(IEnumerable<T> items)//linq
        {
            map = new Dictionary<T, int>();
            
            int count = 0;
            parents = new int[items.Count()];

            foreach (T item in items)
            {
                map.Add(item, count);
                parents[count] = count;
                count++;
            }
        }

        public int Find(T p)
        {
            int pInd = map[p];
            int parentOfInd = parents[map[p]];
            while (parentOfInd != pInd)
            {
                pInd = parentOfInd;
                parentOfInd = parents[parentOfInd];
            }
            return parentOfInd;
        }

        public bool Union(T p, T q)
        {
            if (!AreConnected(p, q))
            {
                parents[Find(p)] = Find(q);
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
