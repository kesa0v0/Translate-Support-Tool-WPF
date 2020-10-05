using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Serialization;
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
            
        }
        
        private void Dest_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            // 현재 정보를 _fileManager.CurrentYamlList에 저장
            ((TranslateItem) TextList.SelectedItem).Dest = Dest.Text;
        }
        
        private void TextList_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (TextList.SelectedItem == null) return;
            // 선택된 아이템 내용 불러오기
            Context.Text = ((TranslateItem) TextList.SelectedItem).Context;
            Origin.Text = ((TranslateItem) TextList.SelectedItem).Origin;
            Dest.Text = ((TranslateItem) TextList.SelectedItem).Dest;
        }
        


        #region ConfirmTranslate

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
            var selectedItem = TextList.SelectedItem;
            var selectedListBoxItem = TextList.ItemContainerGenerator.ContainerFromItem(selectedItem) as ListBoxItem;
            if (selectedListBoxItem != null) selectedListBoxItem.Background = Brushes.GreenYellow;

            // 다음 아이템으로 넘기기
            // Collapsed 된 아이템 무시하기
            do
            {
                // 마지막 아이템일 때는 무시하기
                if (TextList.Items.Count - 1 != TextList.SelectedIndex)
                {
                    TextList.SelectedItem = TextList.Items.GetItemAt(TextList.SelectedIndex + 1);
                }
                else
                {
                    break;
                }
            } while (((TranslateItem) TextList.SelectedItem).Context == "");    
        }

        #endregion

        #region Menus
        
        private void MenuItem_New(object sender, RoutedEventArgs e)
        {
            var items = _fileManager.New();
            TextList.ItemsSource = items;
        }

        private void MenuItem_Open(object sender, RoutedEventArgs e)
        {
            var open = new OpenFileDialog();
            if (open.ShowDialog() != true) return ;
            
            var formatter = new XmlSerializer(typeof(FileManager));
            var file = new FileStream(open.FileName, FileMode.Open);
            var buffer = new byte[file.Length];
            file.Read(buffer, 0, (int)file.Length);
            var stream = new MemoryStream(buffer);
            _fileManager = (FileManager) formatter.Deserialize(stream);
            file.Close();
            // Xml로 저장된 FileManager class 여는 코드...?

            TextList.ItemsSource = _fileManager.CurrentYamlList;
            // 불러온 FileManager 리스트에 넣어주는 코드
        }

        private void MenuItem_Save(object sender, RoutedEventArgs e)
        {
            if (_fileManager.CurrentWorkingFile != "") // 이거 필요 있나 모르것네 ㅋㅋ 
            {
                var outFile = File.Create(_fileManager.CurrentWorkingFile);
                var formatter = new XmlSerializer(_fileManager.GetType());
                formatter.Serialize(outFile, _fileManager);
                outFile.Close();
            }
            else
            {
                SaveToNewFile();
            }
        }
        
        private void MenuItem_Save_As(object sender, RoutedEventArgs e)
        {
            SaveToNewFile();
        }
        
        private void SaveToNewFile()
        {
            var save = new SaveFileDialog {Filter = "XML (*.xml)|*.xml"};
            if (save.ShowDialog() != true) return;
            
            _fileManager.CurrentWorkingFile = save.FileName;
            
            var outFile = File.Create(_fileManager.CurrentWorkingFile);
            var formatter = new XmlSerializer(_fileManager.GetType());
            formatter.Serialize(outFile, _fileManager);
            outFile.Close();
        }
        
        private void MenuItem_Export(object sender, RoutedEventArgs e)
        {
            _fileManager.Export();
        }
        

        #endregion
    }
}