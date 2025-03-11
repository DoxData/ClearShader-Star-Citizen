using System.Windows;

namespace ClearShader_SC
{
    public partial class ConfirmDeleteDialog : Window
    {
        public bool IsConfirmed { get; private set; }

        public ConfirmDeleteDialog()
        {
            InitializeComponent();
        }

        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            IsConfirmed = true;
            this.Close();
        }

        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            IsConfirmed = false;
            this.Close();
        }
    }
}

