using System;

namespace SkipList
{
    class Program
    {
        static void Main(string[] args)
        {
            Random ran = new Random();
            SkipList<int> Test = new SkipList<int>(ran);
            int[] Numbers = new int[] { 10, 30, 50, 100, 80};
            for (int a = 0; a < 5; a++)
            {
                Test.Add(Numbers[a]);
            }
            Test.Remove(30);

        }
       
    }
}
