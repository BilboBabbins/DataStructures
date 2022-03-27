using System;
using System.Collections.Generic;
using System.Text;

namespace AVLTree
{

    class Node<T> where T : IComparable
    {
        public T Value;
        public int childCount
        {
            get
            {
                int count = 0;
                if (Rchild != null)
                {
                    count++;
                }
                if (Lchild != null)
                {
                    count++;
                }
                return count;
            }
        }
        public int height
        {
            get
            {
                int leftHeight = 0;
                if(Lchild != null)
                    {
                    leftHeight = Lchild.height;
                }

                int rightHeight = 0;
                if(Rchild != null)
                    {
                    rightHeight = Rchild.height;
                }

                return Math.Max(rightHeight, leftHeight) + 1;
            }
        }
        public int Balance
        {
            get
            {

                int RchildHeight = 0;
                if(Rchild != null)
                {
                    RchildHeight = Rchild.height;
                }
                int LchildHeight = 0;
                if(Lchild != null)
                {
                    LchildHeight = Lchild.height;
                }

                return RchildHeight - LchildHeight;
            }
        }
        public Node<T> Rchild;
        public Node<T> Lchild;

        public Node(T value)
        {
           Value = value;
        }
    }
    class Tree<T> where T :  IComparable
    {
        Node<T> Root;
        public Node<T> Min(Node<T> current)
        {
            Node<T> temp = current;
            while (temp.Lchild != null)
            {
                temp = temp.Lchild;
            }
            return temp;
        }
        public Node<T> Max(Node<T> current)
        {
            Node<T> temp = current;
            while (temp.Rchild != null)
            {
                temp = temp.Rchild;
            }
            return temp;
        }
        private Node<T> RightRotate(Node<T> rotated)
        {
          Node<T> NewMiddle = rotated.Lchild;
            //rotated.Lchild = null;
          rotated.Lchild = NewMiddle.Rchild;
          NewMiddle.Rchild = rotated;
          return NewMiddle;

        }
        private Node<T> LeftRotate(Node<T> rotated)
        {
            Node<T> NewMiddle = rotated.Rchild;
            //rotated.Rchild = null;
            rotated.Rchild = NewMiddle.Lchild;
            NewMiddle.Lchild = rotated;
            return NewMiddle;
        }

        private Node<T> Balance(Node<T> current)
        {
            if (current.Balance > 1)
            {
                if(current.Rchild.Balance < 0)
                {
                   current.Rchild = RightRotate(current.Rchild);
                }
                current = LeftRotate(current);
            }
            else if (current.Balance < -1)
            {
                if (current.Lchild.Balance > 0)
                {
                    current.Lchild = LeftRotate(current.Lchild);
                }
                current = RightRotate(current);
            }
            return current;
        }


        private Node<T> Add(Node<T> current, T value)
        {
           
            if(current == null)
            {
                return new Node<T>(value);
            }

            if (value.CompareTo(current.Value) > 0)
            { 
                current.Rchild = Add(current.Rchild, value);
            }
            else if (value.CompareTo(current.Value) < 0)
            { 
                current.Lchild = Add(current.Lchild, value);
            }

            return Balance(current);
        }
        public void Add(T value)
        {
            Root = Add(Root, value);
        }

        private Node<T> Delete(Node<T> current, T value)
        {
            if(current == null)
            {
                return null;
            }
            
            if(value.CompareTo(current.Value) < 0)
            {
                current.Rchild = Delete(current.Rchild, value);
            }
            else if(value.CompareTo(current.Value) > 0)
            {
                current.Lchild = Delete(current.Lchild, value);
            }
            else
            {
                if (current.childCount == 2)
                {
                    T repValue = Max(current.Lchild).Value;
                    current.Value = repValue;
                    current.Lchild = Delete(current.Lchild, repValue);
                }
                else if (current.childCount == 1)
                {
                    if (current.Lchild != null)
                    {
                        return current.Lchild;
                    }
                    else
                    {
                        return current.Rchild;
                    }
                }
                else
                {
                    return null;
                }
            }
            return Balance(current);
            //test remove  
        }
        public void Delete(T value)
        {
            Root = Delete(Root, value);
        }
    }
}
