using ConscriptionAdvent.Presentation.ViewModels;
using ConscriptionAdvent.Views;
using System.Windows;

namespace ConscriptionAdvent.UI.DialogViews
{
    /// <summary>
    /// Interaction logic for MessageDialogView.xaml
    /// </summary>
    public partial class MessageDialogView : Window
    {

        public MessageDialogView()
        {
            InitializeComponent();
            Message.Text = MainViewModel.Message;
        }

        private void Yes_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void No_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
