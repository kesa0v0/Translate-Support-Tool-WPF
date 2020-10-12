using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.Win32;

namespace Translate_Support_Tool_WPF_Main
{
    [Serializable]
    public class FileManager
    {
        public string CurrentFile;
        public string CurrentWorkingFile = "";
        public YamlList CurrentYamlList = new YamlList();

        public YamlList New()
        {
            var openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == true)
            {
                CurrentFile = openFileDialog.FileName;
                // if txt
                if (CurrentFile.EndsWith(".txt")) throw new NotImplementedException();

                // if yaml
                if (CurrentFile.EndsWith(".yml") || CurrentFile.EndsWith(".yaml"))
                {
                    var fileContent = File.ReadAllLines(CurrentFile);
                    return Yaml(fileContent);
                }
            }

            return new YamlList();
        }

        public YamlList Yaml(string[] rawData)
        {
            CurrentYamlList = new YamlList();

            var lineRegex = @"\b(?:\s*|\t*)(\S+)\s*:([0-9]*)\s*""(.*)""";
            var commentRegex = @"(#.*)";

            foreach (var line in rawData)
            {
                var lineMatches = new Regex(lineRegex).Match(line);
                var commentMatches = new Regex(commentRegex).Match(line);

                var lineMatchesGroups = lineMatches.Groups;
                var comment = commentMatches.Groups;

                var tempTarget = lineMatches.Success;


                CurrentYamlList.Add(new TranslateItem
                {
                    IsTarget = tempTarget,
                    Comment = comment[0].Value,
                    Context = lineMatchesGroups[1].Value,
                    Number = lineMatchesGroups[2].Value,
                    Origin = lineMatchesGroups[3].Value
                });
            }

            return CurrentYamlList;
        }

        public YamlList Text()
        {
            return new YamlList();
        }

        public void Export()
        {
            if (CurrentYamlList.Count <= 0) return; // YamlList 비었을 때 무시하기

            var result = "l_english:";
            foreach (var yaml in CurrentYamlList)
            {
                result += "  ";

                if (yaml.IsTarget)
                {
                    if (yaml.Dest == null) result += $"{yaml.Context}:{yaml.Number} \"{yaml.Origin}\" ";
                    else result += $"{yaml.Context}:{yaml.Number} \"{yaml.Dest}\" ";
                }

                if (yaml.Comment != "") result += yaml.Comment;

                result += "\n";
            }

            var save = new SaveFileDialog {Filter = "Yaml (*.yml; *.yaml)|*.yml; *.yaml"};
            if (save.ShowDialog() == true)
                File.WriteAllText(save.FileName, result);
        }

        [Serializable]
        public class YamlList : List<TranslateItem>
        {
        }
    }
}