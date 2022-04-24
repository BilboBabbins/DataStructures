using System;
using System.Collections.Generic;

namespace QuickFindUnion
{
    class Program
    {
        static void Main(string[] args)
        {
            LinkedList<int> dogList = new LinkedList<int>();
            dogList.AddLast(5);
            dogList.AddLast(7);
            dogList.AddLast(1);
            dogList.AddLast(4);
            dogList.AddLast(3);
            /*QuickFind<string> dogs = new QuickFind<string>(dogList);
            dogs.Union("ger shep", "golden ret");
            dogs.Union("golden ret", "shiba");
            bool connected = dogs.AreConnected("golden ret", "shiba");*/
            QuickFind<int> dogs = new QuickFind<int>(dogList);
            dogs.Union(7, 5);
            dogs.Union(4, 1);
            dogs.Union(7, 4);
            bool connected = dogs.AreConnected(5, 7);

            Console.WriteLine("Hello World!");
        }
    }
}
