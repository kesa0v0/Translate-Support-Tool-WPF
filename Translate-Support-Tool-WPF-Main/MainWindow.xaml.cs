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

            List<TranslateItem> items = new List<TranslateItem>();
            items.Add(new TranslateItem() { Context = "context1", Origin = "test1"});
            items.Add(new TranslateItem() { Context = "context1", Origin = "test2"});
            items.Add(new TranslateItem() { Origin = "test3"});
            TextList.ItemsSource = items;
        }

        private void TextList_SelectionChanged(object sender, RoutedEventArgs e) {
            if (TextList.SelectedItem != null)
            {
                Context.Content = (TextList.SelectedItem as TranslateItem)?.Context;
                Origin.Content = (TextList.SelectedItem as TranslateItem)?.Origin;
            }
        }
        private void Confirm_OnClick(object sender, RoutedEventArgs e)
        {
            
        }

        private void MenuItem_New(object sender, RoutedEventArgs e)
        {
            
        }

        private void MenuItem_Open(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void MenuItem_Save(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
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