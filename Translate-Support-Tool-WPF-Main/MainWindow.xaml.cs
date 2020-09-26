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
    // is this string target to translate?
    public Boolean IsTarget { get; set; }
    
    public int Indent { get; set; }
    public string Context { get; set; }
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
        // maybe can use regex
        
        // 한줄한줄 변환해도 될듯
        
        var lineRegex = @"\b(\s*|\t*)(\S+)\s*:0\s*""(.*)""(?!\n)";
        var commentRegex = @"#.*(?!\n)";
        
        foreach (var line in rawData)
        {
            var lineMatches = new Regex(lineRegex).Matches(line);
            var commentMatches = new Regex(commentRegex).Matches(line);
            
        }


        // 
        // foreach (var lineMatch in lineMatches)
        // {
        //     result.Add(new TranslateItem
        //     {
        //         IsTarget = true,
        //         Context = lineMatch.ToString(),
        //         Origin = lineMatch.ToString()
        //     });
        // }
        //
        // 
        // foreach (var commentMatch in commentMatches)
        // {
        //     result.Add(new TranslateItem
        //     {
        //         IsTarget = false,
        //         Context = commentMatch.ToString(),
        //         Origin = commentMatch.ToString()
        //     });
        // }

        return result;
    }

    public class YamlList : List<TranslateItem>
    {
    }
}