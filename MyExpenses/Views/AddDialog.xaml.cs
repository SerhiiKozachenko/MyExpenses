using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace MyExpenses.Views
{
    public sealed partial class AddDialog : ContentDialog
    {
        public AddDialog()
        {
            this.InitializeComponent();
        }

        private void AmountTextBoxLoaded(object sender, RoutedEventArgs e)
        {
            this.AmountTextBox.Focus(FocusState.Keyboard);
        }
    }
}
