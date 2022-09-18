using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HashMap
{
    internal class BloomFlter<T>
    {
        List<Func<T, int>> hashSet;
        public bool[] bools;
        public BloomFlter(int cap)
        {
            hashSet = new List<Func<T, int>>();
            bool[] bools = new bool[cap];
            for (int a = 0; a < cap; a++)
            {
                bools[a] = false;
            }
        }

        public void LoadHashFunc(Func<T, int> hashFunc)
        {

            hashSet.Add(hashFunc);
        }

        public void Insert(T item)
        {

            for (int a = 0; a < hashSet.Count(); a++)
            {
                Func<T, int> hashFunc = hashSet[a];
                bools[hashFunc(item)] = true;
            }
        }

        public bool ProbContains(T item)
        {
            for (int a = 0; a < hashSet.Count(); a++)
            {
                Func<T, int> hashFunc = hashSet[a];
                if (!bools[hashFunc(item)])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
