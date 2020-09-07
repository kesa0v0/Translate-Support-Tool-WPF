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

            List<TextListItem> items = new List<TextListItem>();
            items.Add(new TextListItem() { Context = "context1", Origin = "test1"});
            items.Add(new TextListItem() { Context = "context1", Origin = "test2"});
            items.Add(new TextListItem() { Origin = "test3"});
            TextList.ItemsSource = items;
        }

        private void TextList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TextList.SelectedItem != null)
            {
                Context.Content = (TextList.SelectedItem as TextListItem)?.Context;
                Origin.Content = (TextList.SelectedItem as TextListItem)?.Origin;
            }
        }
    }

    public class TextListItem
    {
        public string Context { get; set; }
        public string Origin { get; set; }
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