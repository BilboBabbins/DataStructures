using System;
using System.Collections.Generic;
using System.Text;

namespace Trie
{
   
    class TrieNode
    {
        public Dictionary<char, TrieNode> Children { get; set; }
        public char Value;
        public bool endWord;

        public TrieNode(char val, bool endwordd)
        {
            Children = new Dictionary<char, TrieNode>();
            Value = val;
            endWord = endwordd;
        }

    }

    class TrieTree
    {
        public TrieNode Head;
        public TrieTree()
        {
            Head = new TrieNode('^', false);
        }
        public bool Contains(char letter, TrieNode current)
        {
            if (current.Children != null && current.Children.ContainsKey(letter))
            {
                return true;
            }
            return false;
        }
        public void Add(string word)
        { 
           TrieNode temp = Head;
           for(int a = 0; a < word.Length; a++)
           {
                if (!Contains(word[a], temp))
                {
                    TrieNode added = new TrieNode(word[a], a == word.Length - 1);
                    temp.Children.Add(word[a], added);
                }
                temp = temp.Children[word[a]];

                if (a == word.Length - 1 && temp.endWord == false)
                {
                    temp.endWord = true;
                }
                   
           }
        }
        public bool Remove(string word)
        {
            TrieNode temp = Head;
            for (int a = 0; a < word.Length; a++)
            {
                if (Contains(word[a], temp))
                {
                    temp = temp.Children[word[a]];
                }
                else
                {
                    return false;
                }
            }
            temp.endWord = false;
            return true;
        }

        public TrieNode Search(string word)
        {
            TrieNode temp = Head;
            for (int a = 0; a < word.Length; a++)
            {
                if (Contains(word[a], temp))
                {
                    temp = temp.Children[word[a]];
                }
            }
            return temp;
        }
    }
}
