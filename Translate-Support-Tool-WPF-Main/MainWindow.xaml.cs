using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

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

            List<TranslateItem> items = new List<TranslateItem>();
            items.Add(new TranslateItem() { Context = "context1", Origin = "test1"});
            items.Add(new TranslateItem() { Context = "context1", Origin = "test2"});
            items.Add(new TranslateItem() { Origin = "test3"});
            TextList.ItemsSource = items;
        }

        private void TextList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TextList.SelectedItem != null)
            {
                Context.Content = (TextList.SelectedItem as TranslateItem)?.Context;
                Origin.Content = (TextList.SelectedItem as TranslateItem)?.Origin;
            }
        }
        private void Confirm_OnClick(object sender, RoutedEventArgs e)
        {
            
        }
    }
}

public class TranslateItem
{
    // is this string target to translate?
    public Boolean IsTarget { get; set; }
    public string Context { get; set; }
    public string Origin { get; set; }
}



class FileManager
{
    public string[] ReadFile(string location)
    {
         return System.IO.File.ReadAllLines(location);
    }

    public List<YamlList> Yaml(string[] rawData)
    {
        var result = new List<YamlList>();
        
        // TODO: Some kind of get yml text
        // maybe can use regex
        
        return result;
    }
    
    public class YamlList : List<TranslateItem>
    {
        public string WhatLanguage { get; set; }
    
    
    }
}