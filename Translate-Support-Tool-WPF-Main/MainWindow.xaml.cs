using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Windows;
using System.Windows.Controls;

namespace Translate_Support_Tool_WPF_Main
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            var fileManager = new FileManager();
            
            var rawFile = fileManager.ReadFile(@"C:\_Storage\Programming\MyProject\WPF\Translate-Support-Tool-WPF\Translate-Support-Tool-WPF-Main\testData\test.yml");
            var file = fileManager.Yaml(rawFile);

            foreach (var t in rawFile)
            {
                ListViewData.GetInstance().Add(new ListViewData() { Context = t });
            }
            
            TextList.ItemsSource = ListViewData.GetInstance();
            
            TextList.SelectionChanged
        }
    }

    class ListViewData
    {
        public string Context { get; set; }

        private static List<ListViewData> instance;

        public static List<ListViewData> GetInstance()
        {
            if (instance == null)
                instance = new List<ListViewData>();

            return instance;
        }
}

    class FileManager
    {
        public string[] ReadFile(string location)
        {
             return System.IO.File.ReadAllLines(location);
        }

        public string Yaml(string[] rawData)
        {
            return "";
        }
    }
}