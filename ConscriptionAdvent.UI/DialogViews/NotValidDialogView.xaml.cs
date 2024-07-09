using System.Windows;

namespace ConscriptionAdvent.UI.DialogViews
{
    /// <summary>
    /// Interaction logic for NotValidDialogView.xaml
    /// </summary>
    public partial class NotValidDialogView : Window
    {
        public NotValidDialogView(string message)
        {
            InitializeComponent();

            validationMessage.Text = message;
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
