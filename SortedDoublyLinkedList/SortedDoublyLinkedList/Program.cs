using System;

namespace SortedDoublyLinkedList
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            DoublyLinkedList<int> test = new DoublyLinkedList<int>();
            int[] testNum = { 5, 9, 2, 3 };
            for (int a = 0; a < testNum.Length; a++)
            {
                test.InsertNode(testNum[a]);
            }
            test.InsertNode(20);
        }
    }
}
