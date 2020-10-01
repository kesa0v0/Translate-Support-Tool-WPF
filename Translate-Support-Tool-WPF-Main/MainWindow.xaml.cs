using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

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
        
        private void Dest_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            // 현재 정보를 _fileManager.CurrentYamlList에 저장
            ((TranslateItem) TextList.SelectedItem).Dest = Dest.Text;
        }
        
        private void TextList_SelectionChanged(object sender, RoutedEventArgs e) {
            if (TextList.SelectedItem != null)
            {
                // 선택된 아이템 내용 불러오기
                Context.Text = (TextList.SelectedItem as TranslateItem)?.Context;
                Origin.Text = (TextList.SelectedItem as TranslateItem)?.Origin;
                Dest.Text = (TextList.SelectedItem as TranslateItem)?.Dest;
            }
        }
        
        private void Dest_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && TextList.SelectedItem != null)
            {
                ConfirmTranslate();
            }
        }

        private void Confirm_OnClick(object sender, RoutedEventArgs e)
        {
            if (TextList.SelectedItem != null)
                ConfirmTranslate();
        }

        private void ConfirmTranslate()
        {
            // 현재 정보를 _fileManager.CurrentYamlList에 저장
            ((TranslateItem) TextList.SelectedItem).Dest = Dest.Text;
            // IsDone을 True로 바꾸기
            ((TranslateItem) TextList.SelectedItem).IsDone = true;
            // 현재 아이템의 배경 초록색으로 바꾸기
            object selectedItem = TextList.SelectedItem;
            ListBoxItem selectedListBoxItem = TextList.ItemContainerGenerator.ContainerFromItem(selectedItem) as ListBoxItem; 
            selectedListBoxItem.Background = Brushes.GreenYellow;
            // 다음 아이템으로 넘기기
            // TODO: 마지막 아이템일 때는 무시하기
            // TODO: Collapsed 된 아이템도 무시하기
            TextList.SelectedItem = TextList.Items.GetItemAt(TextList.SelectedIndex + 1);
        }

        private void MenuItem_New(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();

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