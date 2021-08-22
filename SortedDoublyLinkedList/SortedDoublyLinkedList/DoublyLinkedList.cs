using System;
using System.Collections.Generic;
using System.Text;

namespace SortedDoublyLinkedList
{
    class Node<T>
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


    }
    class DoublyLinkedList<T> where T : IComparable
    {
        public Node<T> Head;
        public DoublyLinkedList()
        {
            Head = new Node<T>(default);
        }
        private void ConnectNodes(Node<T> prev, Node<T> current, Node<T> next)
        {
            current.Next = next;
            current.Prev = prev;
            prev.Next = current;


            if (prev == current)
            {
                return;
            }
            next.Prev = current;
            
        }
        public void InsertNode(T value)
        {
            Node<T> current = Head;
            Node<T> prev = Head;
            Node<T> added = new Node<T>(value);

            //important to skip lsit 
            while (current.Next.Value.CompareTo(value) < 0)
            {
                prev = current;
                if (current.Next == null)
                {
                    break;
                }
                current = current.Next;               
            }

            ConnectNodes(prev, added, current);
        }
        public void Remove(T value)
        {
            Node<T> removed = Head;
            while(!removed.Value.Equals(value))
            {
                removed = removed.Next;
            }
            RemoveConnection(removed);
        }
        private void RemoveConnection(Node<T> removed)
        {
            removed.Next.Prev = removed.Prev;
            removed.Prev.Next = removed.Next;
        }
        public Node<T> Search(T value)
        {
            Node<T> searching = Head;
            while (!searching.Value.Equals(value))
            {
                searching = searching.Next;
            }
            return searching;
        }
    }
}
