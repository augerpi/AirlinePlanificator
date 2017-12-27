using System.ComponentModel;
using System.Windows;
using System.Windows.Data;

namespace AirlinePlanificator.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Company : Window
    {
        public Company()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ICollectionView pView = CollectionViewSource.GetDefaultView(cboHub.Items);
            pView.SortDescriptions.Add(new SortDescription("CompleteHubName", ListSortDirection.Ascending));
            var productview = (ICollectionViewLiveShaping)CollectionViewSource.GetDefaultView(cboHub.Items);
            productview.IsLiveSorting = true;
        }
    }
}
