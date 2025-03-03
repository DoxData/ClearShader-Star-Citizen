using System.IO;
// No changes needed, the file is already well-structured and functional
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ClearShader_SC;

// ...existing code...

public partial class MainWindow : Window
{
    private string? starCitizenFolderPath;

    public MainWindow()
    {
        InitializeComponent();
    }

    private void BuscarButton_Click(object sender, RoutedEventArgs e)
    {
        string username = UsernameTextBox.Text;
        string path = $@"C:\Users\{username}\AppData\Local\Star Citizen";

        if (Directory.Exists(path))
        {
            starCitizenFolderPath = path;
            MessageBox.Show("Shaders encontrados", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            PopulateTreeView(path);
        }
        else
        {
            starCitizenFolderPath = null;
            MessageBox.Show("Shaders no encontrados", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void EliminarButton_Click(object sender, RoutedEventArgs e)
    {
        if (starCitizenFolderPath != null && Directory.Exists(starCitizenFolderPath))
        {
            Directory.Delete(starCitizenFolderPath, true);
            MessageBox.Show("Carpeta eliminada", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            FoldersTreeView.Items.Clear();
        }
        else
        {
            MessageBox.Show("No se encontró la carpeta para eliminar", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void PopulateTreeView(string path)
    {
        FoldersTreeView.Items.Clear();
        var rootDirectoryInfo = new DirectoryInfo(path);
        FoldersTreeView.Items.Add(CreateDirectoryNode(rootDirectoryInfo));
    }

    private static TreeViewItem CreateDirectoryNode(DirectoryInfo directoryInfo)
    {
        var directoryNode = new TreeViewItem { Header = directoryInfo.Name, Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFC91D1D")) };
        foreach (var directory in directoryInfo.GetDirectories())
        {
            directoryNode.Items.Add(CreateDirectoryNode(directory));
        }
        return directoryNode;
    }

    private void TitleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (e.ClickCount == 2)
        {
            this.WindowState = this.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
        }
        else
        {
            this.DragMove();
        }
    }

    private void CloseButton_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
    }
}
