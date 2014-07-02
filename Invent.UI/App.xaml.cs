using System.Windows;
using InventUI.UI;

namespace InventUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        public App()
        {
            var login = new LoginUI();
            login.ShowDialog();
            if (!login.Model.IsLogin)
                Current.Shutdown();
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
        }
    }
}
