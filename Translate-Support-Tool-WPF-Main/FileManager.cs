using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using Microsoft.Win32;

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
        
        var lineRegex = @"\b(?:\s*|\t*)(\S+)\s*:([0-9]*)\s*""(.*)""";
        var commentRegex = @"(#.*)";
        
        foreach (var line in rawData)
        {
            var lineMatches = new Regex(lineRegex).Match(line);
            var commentMatches = new Regex(commentRegex).Match(line);
            
            var lineMatchesGroups = lineMatches.Groups;
            var comment = commentMatches.Groups;

            var tempVisible = Visibility.Visible;
            if (lineMatches.Success == false)
            {
                tempVisible = Visibility.Collapsed;
            }

            result.Add(new TranslateItem
            {
                IsVisible = tempVisible,
                Comment = comment[0].Value,
                Context = lineMatchesGroups[1].Value.Replace("\\n", "\\\\n"),
                Number = lineMatchesGroups[2].Value,
                Origin = lineMatchesGroups[3].Value.Replace("\\n", "\\\\n")
            });
        }

        return result;
    }

    public class YamlList : List<TranslateItem>
    {
    }
}