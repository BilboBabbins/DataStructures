using System;
using System.Collections.Generic;
using System.Text;

namespace WeightedDirectGraphs
{
    class Priority<T> 
    {
        public IComparer<T> comparer;
        public T[] values = new T[0];
        public Priority(IComparer<T> comp)
        {
            this.comparer = comp;
        }

        void heapifyUp(int index)
        {
            int parentInd = (index - 1) / 2;

            while (comparer.Compare(values[index], values[parentInd]) == -1)
            {
                T bucket = values[index];
                values[index] = values[parentInd];
                values[parentInd] = bucket;
                index = parentInd;
                index = (index - 1) / 2;
            }

        }

        void heapifyDown(int index)
        {

            //index  > children
            int lChildInd = (index + 1) + index;
            int rChildInd = (index + 2) + index;

            //values[index] > values[rchildind] && values[ind]
            // > values[lchild] && not leaf 
            while ((rChildInd < values.Length || lChildInd < values.Length) &&
                (comparer.Compare(values[index], values[rChildInd - 1]) == 1
              || comparer.Compare(values[index], values[lChildInd - 1]) == 1))
            {
                if (comparer.Compare(values[lChildInd], values[rChildInd]) == -1)
                {
                    T bucket = values[index];
                    values[index] = values[lChildInd];
                    values[(index + 1) + index] = bucket;
                    index = (index + 1) + index;
                }


                if (comparer.Compare(values[rChildInd], values[lChildInd]) == -1)
                {
                    T bucket = values[index];
                    values[index] = values[rChildInd];
                    values[(index + 2) + index] = bucket;
                    index = (index + 2) + index;
                }
            }
        }
        public void insert(T value)
        {
            T[] temp = new T[values.Length + 1];
            for (int a = 0; a < values.Length; a++)
            {
                temp[a] = values[a];
            }
            temp[values.Length] = value;
            values = temp;
            heapifyUp(values.Length - 1);

        }

        public T pop()
        {
            T[] temp = new T[values.Length - 1];
            T returning = values[0];
            values[0] = values[values.Length - 1];
            for (int a = 0; a < temp.Length; a++)
            {
                temp[a] = values[a];
            }
            values = temp;
            heapifyDown(0);
            return returning;
        }

        public bool contains(T containee)
        {
            for(int a = 0; a < values.Length - 1; a++)
            {
                if (values[a].Equals(containee))
                {
                    return true;
                }
            }
            return false;
        }
        // add heapify/delete
  

    }
}
