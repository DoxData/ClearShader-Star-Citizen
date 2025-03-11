using System.IO;
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
            ShowCustomMessage("Shaders encontrados");
            PopulateTreeView(path);
        }
        else
        {
            starCitizenFolderPath = null;
            ShowCustomMessage("Shaders no encontrados");
        }
    }

    private void EliminarButton_Click(object sender, RoutedEventArgs e)
    {
        var confirmDeleteDialog = new ConfirmDeleteDialog();
        confirmDeleteDialog.Owner = this;
        confirmDeleteDialog.ShowDialog();

        if (confirmDeleteDialog.IsConfirmed && starCitizenFolderPath != null && Directory.Exists(starCitizenFolderPath))
        {
            Directory.Delete(starCitizenFolderPath, true);
            ShowCustomMessage("Eliminación completada");
            FoldersTreeView.Items.Clear();
        }
        else if (!confirmDeleteDialog.IsConfirmed)
        {
            ShowCustomMessage("Eliminación cancelada");
        }
        else
        {
            ShowCustomMessage("No se encontró la carpeta para eliminar");
        }
    }

    private void ShowCustomMessage(string message)
    {
        var customMessageDialog = new CustomMessageDialog(message);
        customMessageDialog.Owner = this;
        customMessageDialog.ShowDialog();
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
