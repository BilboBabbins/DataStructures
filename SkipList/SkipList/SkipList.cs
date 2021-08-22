using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SkipList
{

    /*class Node<T>
    {
        public Node<T> Next;
        public Node<T> Prev;
        public T Value;
        public Node(T value)
        {
            Value = value;
            Next = null;
            Prev = null;
        }
    }*/


    class SkipListNode<T>
    {
        public bool VoidNode;
        public SkipListNode<T> Next;
        public SkipListNode<T> Prev;
        public SkipListNode<T> Down;
        public int Height;
        public T Value;
      

        public SkipListNode(T value, int height)
        {
            Value = value;
            VoidNode = false;
            Height = height;
  
        }
    }

    class SkipList<T> : ICollection<T> 
        where T : IComparable<T>
    {
        Random Ran;
        public SkipListNode<T> Head;
        public int Count { get; set;}


        public bool IsReadOnly => throw new NotImplementedException();

        public SkipList(Random ran)
        {
            Head = new SkipListNode<T>(default(T), 0);
            Head.VoidNode = true;
            Ran = ran;
        }

       
        public int CalcHeight()
        {
            int totalHeight = 0;
            
            while (Ran.Next(0, 2) == 1 && totalHeight < Head.Height + 1)
            {
                totalHeight++;
            }

            return totalHeight;
        }
        // each down node is a different node idk what this is 
        // each collum is like a linked list
        // each row is also a linked list??
        // make each node when initliazling
        // array of nodes
        //how do i initalize each node
        // make for loop of collum ? temp.down = 
        //smth like that^^^
        private void ConnectNodes(SkipListNode<T> prev, SkipListNode<T> current, SkipListNode<T> next, int height)
        {

            if (prev == next)
            {
                prev.Next = current;
                current.Prev = prev;
                current.Height = height;
            }
            else
            {
                current.Next = next;
                current.Prev = prev;
                prev.Next = current;
                next.Prev = current;
                current.Height = height;
            }



           
        }
        private void RemoveConnection(SkipListNode<T> removed)
        {
            removed.Next.Prev = removed.Prev;
            removed.Prev.Next = removed.Next;
        }
        public void Add(T value)
        {
            SkipListNode<T> Inserting = new SkipListNode<T>(value, CalcHeight());

          
          
            if (Inserting.Height > Head.Height) 
            {
                var newHead = new SkipListNode<T>(default(T), Inserting.Height);
                newHead.VoidNode = true;
                newHead.Down = Head;
                Head = newHead;
             
            }
          

            //current.Next != null && current.Next.Value.CompareTo(value) < 0


            //go down when next value is greater than or null
            //go next when value is less than

            //insert when you cant go down any further

            SkipListNode<T> prevInsert = null;
            SkipListNode<T> temp = Head;
            while (temp.Height != Inserting.Height)
            {
                temp = temp.Down;
            }
            while (Inserting.Height != -1)
            {
             

                while (temp.Next != null && temp.Next.Value.CompareTo(value) < 0)
                {
                    temp = temp.Next;
                }

                Inserting.Prev = temp;
                if (temp.Next != null)
                {
                    Inserting.Next = temp.Next;
                }
                temp.Next = Inserting;
                //over here ^^ messes up, on second 

                if (prevInsert != null)
                {
                   prevInsert.Down = Inserting;
                }

                prevInsert = Inserting;
                Inserting = new SkipListNode<T>(value, prevInsert.Height - 1);
                temp = temp.Down;
            }

            //down is duplicated because youre doing inserting = inserting, you have to differentiate 
            //prevInsert and Insertin
            Count++;
         }

     
        public SkipListNode<T> Search(T value)
        {
            SkipListNode<T> temp = Head;
            SkipListNode<T> prevTemp = Head;
            while (!temp.Next.Value.Equals(value))
            {
                while (temp.Next != null && temp.Next.Value.CompareTo(value) < 0)
                {
                    temp = temp.Next;
                }
                if (prevTemp.Down != null)
                {
                    temp = prevTemp.Down;
                    prevTemp = temp;
                }
               
            }
            return temp.Next;
        }
        public bool Contains(T value)
        {
            SkipListNode<T> contain = Search(value);
            if (contain.Value.Equals(value))
            {
                return true;
            }
            return false;
        }
        public bool Remove(T value)
        {
             SkipListNode<T> deleted = Search(value);
             while (deleted != null)
             {
                   deleted.Prev.Next = deleted.Next;
                   deleted.Next.Prev = deleted.Prev;
                   deleted = deleted.Down;
             }
             if (Contains(value))
             {
                Count--;
                return true;
             }
            return false;
        }

        public void Clear()
        {
            Head = null;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}

//option 1:
//added node always 0 height
//flip coin is more of a bool
//if true, duplicate bottom level then add node


//option 2:
//Node has a height as we add it
//Go down to that node's height, treat each level as a doublylinked list

//use tail/ dont use tail






//copy level down, then insert
// if true flip, go first level, duplicate down, connect previous
//add HEIGHT june 7th, july, 6:30 - 10


