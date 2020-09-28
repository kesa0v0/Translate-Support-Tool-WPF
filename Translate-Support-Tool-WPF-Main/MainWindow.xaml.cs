using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using Microsoft.Win32;

namespace Translate_Support_Tool_WPF_Main
{
    public partial class MainWindow
    {
        private FileManager _fileManager;
        public MainWindow()
        {
            InitializeComponent();

            _fileManager = new FileManager();

            var file =
                @"C:\_Storage\Programming\MyProject\WPF\Translate-Support-Tool-WPF\Translate-Support-Tool-WPF-Main\sample\test.yml";
            string[] fileContents = File.ReadAllLines(file);
            FileManager.YamlList items = _fileManager.Yaml(fileContents);
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
            FileManager.YamlList items = _fileManager.Yaml(_fileManager.Open());
            TextList.ItemsSource = items;
        }

        private void MenuItem_Save(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}

public class TranslateItem
{
    public string Comment { get; set; }
    public string Context { get; set; }
    public string Number { get; set; } 
    public string Origin { get; set; }
}


class FileManager
{
    public string[] Open()
    {
        OpenFileDialog openFileDialog = new OpenFileDialog();

        if (openFileDialog.ShowDialog() == true)
        {
            var fileContent = File.ReadAllLines(openFileDialog.FileName);
            return fileContent;
        }

        return new string[]{};
    }

    public YamlList Yaml(string[] rawData)
    {
        var result = new YamlList();

        result.Add(new TranslateItem { Context = "context1", Origin = "test1"});

        // TODO: Some kind of get yml text
        
        var lineRegex = @"\b(?:\s*|\t*)(\S+)\s*:([0-9]*)\s*""(.*)""";
        var commentRegex = @"(#.*)";
        
        foreach (var line in rawData)
        {
            var lineMatches = new Regex(lineRegex).Match(line);
            var commentMatches = new Regex(commentRegex).Match(line);

            var lineMatchesGroups = lineMatches.Groups;
            var comment = commentMatches.Groups;
            
            
            result.Add(new TranslateItem
            {
                Comment = comment[0].Value,
                Context = lineMatchesGroups[1].Value,
                Number = lineMatchesGroups[2].Value,
                Origin = lineMatchesGroups[3].Value,
            });
        }

        return result;
    }

    public class YamlList : List<TranslateItem>
    {
    }
}