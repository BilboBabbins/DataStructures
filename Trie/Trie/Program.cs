using System;

namespace Trie
{
    class Program
    {
        static void Main(string[] args)
        {
            TrieTree test = new TrieTree();

            test.Add("hello");
            test.Add("hey");
            test.Add("he");
            test.Remove("hello");
            TrieNode searched = test.Search("he");

        }
    }
}
