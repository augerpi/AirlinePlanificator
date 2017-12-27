using System.Windows;

namespace AirlinePlanificator.Views
{
    /// <summary>
    /// Interaction logic for SelectAirport.xaml
    /// </summary>
    public partial class SelectAirport : Window
    {        
        public SelectAirport()
        {
            InitializeComponent();
        }

        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
