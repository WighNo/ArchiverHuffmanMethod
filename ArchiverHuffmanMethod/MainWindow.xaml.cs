using System.IO;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;

namespace ArchiverHuffmanMethod
{

    public partial class MainWindow : Window
    {
        private Archiver _archiver = new Archiver();

        private string _filePath ;
        private string _fileName;

        private string _savePath;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void BorderMouseDown(object sender, MouseButtonEventArgs eventArgs)
        {
            if (eventArgs.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }
        private void HideApplication(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        private void CloseApplication(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void BrowseFile(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Files | *.huf; *.txt";

            bool? response = openFileDialog.ShowDialog();

            if (response == true)
                UpdateFilePath(openFileDialog.FileName, openFileDialog.SafeFileName);
        }

        private void UpdateFilePath(string path, string fileName)
        {
            _filePath = path;
            _fileName = fileName;

            SelectedFileLable.Content = _filePath;

            int endNameIndex = _filePath.IndexOf($@"\{fileName}");
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);

            if (Path.GetExtension(_filePath) == ".huf")
                UpdateSavePath($@"{_filePath.Substring(0, endNameIndex)}\{fileNameWithoutExtension}.txt");
            else
                UpdateSavePath($@"{_filePath.Substring(0, endNameIndex)}\{fileNameWithoutExtension}.huf");
        }

        private void UpdateSavePath(string path)
        {
            _savePath = path;
            SavePathLable.Content = _savePath;
        }

        private void Archive(object sender, RoutedEventArgs e)
        {
            if (MissingFile(_filePath) == true || WrongExtension(_filePath, ".txt") == true)
                return;

            _archiver.Archive(_filePath, _savePath);
        }

        private void Unpack(object sender, RoutedEventArgs e)
        {
            if (MissingFile(_filePath) == true || WrongExtension(_filePath, ".huf") == true)
                return;

            _archiver.Unpack(_filePath, _savePath);
        }

        private bool MissingFile(string path)
        {
            if(path is null == true)
            {
                MessageBox.Show("Файл не выбран", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return true;
            }

            return false;
        }

        private bool WrongExtension(string path, string targetExtension)
        {
            if(Path.GetExtension(_filePath) != targetExtension)
            {
                MessageBox.Show("Некорректное расширение файла", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return true;
            }

            return false;
        }
    }
}
