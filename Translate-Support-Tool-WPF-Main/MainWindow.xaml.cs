using System;
using System.IO;
using System.Windows;

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
                Context.Text = (TextList.SelectedItem as TranslateItem)?.Context;
                Origin.Text = (TextList.SelectedItem as TranslateItem)?.Origin;
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