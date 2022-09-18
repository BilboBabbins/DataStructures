using System;

namespace HashMap
{
    class Program
    {
        static void Main(string[] args)
        {

            hashMap<int, string> dogs = new hashMap<int, string>();
            dogs.Add(12, "poodle");
            dogs.Add(2, "german shep");
           
            
            
            //dogs.ReHash();

            BloomFlter<string> test = new BloomFlter<string>(dogs.Count);
            int HashFunc1(string item)
            {
                return item.GetHashCode() % dogs.Count;
            }
            test.LoadHashFunc(HashFunc1);


            test.Insert(dogs[4]);

            bool contains = test.ProbContains(dogs[4]);

            Console.WriteLine("Hello World!");
        }
    }
}
