using System;

namespace AVLTree
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] nums = { 25, 15, 6 };
            Tree<int> testTree = new Tree<int>();
            for (int a = 0; a <nums.Length; a++)
            {
                testTree.Add(nums[a]);
            }
           // testTree.Delete(2);

        }
    }
}
