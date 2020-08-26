using System;
using System.Collections.Generic;

namespace Translate_Support_Tool_WPF_Main
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            var fileManager = new FileManager();
            
            var rawFile = fileManager.ReadFile(@"C:\_Storage\Programming\MyProject\WPF\Translate-Support-Tool-WPF\Translate-Support-Tool-WPF-Main\testData\test.yml");
            // var file = fileManager.Yaml(rawFile);

            // Context.Content = file[0];
            // Origin.Content = file[1];

            Context.Content = rawFile[1].Split(':')[0];
            Context.Content = rawFile[1].Split(':')[1].Substring(2, -1);
        }
    }

    class FileManager
    {
        public string[] ReadFile(string location)
        {
             return System.IO.File.ReadAllLines(location);
        }

        public Dictionary<int, string[]> Yaml(string[] rawData)
        {
            var dictionary = new Dictionary<int, string[]>();
            
            // processing
            // DataStructure = {0, ("origin", "dest")}
            
            return dictionary;
        }
    }
}