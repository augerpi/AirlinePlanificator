using System.Windows;

namespace AirlinePlanificator
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Views.Company wnd = new Views.Company();
            wnd.DataContext = new ViewModels.CompanyViewModel();
            wnd.Show();
        }
    }
}
