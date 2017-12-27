using System.Windows;

namespace AirlinePlanificator.Views
{
    /// <summary>
    /// Interaction logic for LinePlanificator.xaml
    /// </summary>
    public partial class LinePlanificator : Window
    {
        public LinePlanificator()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
