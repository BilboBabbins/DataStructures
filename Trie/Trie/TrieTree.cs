using System;
using System.Collections.Generic;
using System.Text;

namespace Trie
{
   
    class TrieNode
    {
        public List<TrieNode> Children;
        public char Value;

        public TrieNode(char val)
        {
            Value = val;
        }

    }

    class TrieTree
    {
        public TrieNode Head;

        public bool Contains(TrieNode current, char letter)
        {
            for (int a = 0; a < current.Children.Count; a++)
            {
                if (current.Children[a].Value == letter)
                {
                    return true;
                }
            }
            return false;
        }
        public void Add(string word)
        { 
           
        }
            

    }
}
