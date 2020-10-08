using System;
using System.Collections.Generic;

namespace Translate_Support_Tool_WPF_Main
{
    public class WordDictionaryManager
    {
        public WordDictionary WordDictionary = new WordDictionary();
        
        
    }

    public class WordDictionary : Dictionary<string, WordItem>
    {
    }

    public class WordItem
    {
        // name is english
        public int count { get; set; }
        public string korean { get; set; }
    }
}