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
            dogs.Add(112, "german shep");
            dogs.Add(42, "german shep");
            dogs.Add(62, "german shep");
            dogs.Add(423, "german shep");
            dogs.Add(7654, "german shep");
            dogs.Add(13212, "german shep");
            dogs.Add(23, "german shep");
            dogs.Add(223, "german shep");
            dogs.Add(21, "german shep");
            dogs[2] = "golden retriever";
            //dogs.ReHash();

            Console.WriteLine("Hello World!");
        }
    }
}
