using System;

namespace Translate_Support_Tool_WPF_Main
{
    [Serializable]
    public class TranslateItem
    {
        public bool IsDone { get; set; }
        public bool IsTarget { get; set; }
        public string Comment { get; set; }
        public string Context { get; set; }
        public string Number { get; set; }
        public string Origin { get; set; }
        public string Dest { get; set; }
    }
}