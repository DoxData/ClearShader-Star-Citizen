using System.Windows;

namespace ClearShader_SC
{
    public partial class CustomMessageDialog : Window
    {
        public CustomMessageDialog(string message)
        {
            InitializeComponent();
            MessageTextBlock.Text = message;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
