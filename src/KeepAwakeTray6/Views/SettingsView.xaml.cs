using System.Text.RegularExpressions;
using KeepAwakeTray6.ViewModels;
using System.Windows;
using System.Windows.Input;

namespace KeepAwakeTray6.Views
{
    /// <summary>
    /// Interaction logic for SettingsView.xaml
    /// </summary>
    public partial class SettingsView : Window
    {
        private readonly Regex allowdCharachters = new Regex("[^0-9]+");

        public SettingsView(SettingsViewModel vm)
        {
            InitializeComponent();

            DataContext = vm;
            vm.CloseRequested += (sender, args) => Close();
        }

        private void MoveWindow(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void UIElement_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = allowdCharachters.IsMatch(e.Text);
        }
    }
}
