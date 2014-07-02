using DevExpress.Xpf.Bars;
using System.Windows;
using InventUI.Tools;
using InventUI.UI;
using InventUI.UI.MainWindowControls;
using InventUI.UI.References;

namespace InventUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonOpenRegister(object sender, ItemClickEventArgs e)
        {
            DocumentPanelManager.SelectPanel(DockLayoutManager, DocumentContainer, typeof (RegisterUserControl), true);
        }

        private void ButtonOpenRegisterDetails(object sender, ItemClickEventArgs e)
        {
            DocumentPanelManager.SelectPanel(DockLayoutManager, DocumentContainer, typeof(RegisterDetailsUserControl), true);
        }

        private void ButtonOpenInvent(object sender, ItemClickEventArgs e)
        {
            DocumentPanelManager.SelectPanel(DockLayoutManager, DocumentContainer, typeof(InventUserControl), true);
        }

        private void ButtonOpenWorkPlacesReference(object sender, ItemClickEventArgs e)
        {
            (new ReferenceWorkPlaces { Owner = this }).ShowDialog();
        }

        private void ButtonOpenSpUsersReference(object sender, ItemClickEventArgs e)
        {
            (new ReferenceSpUsers { Owner = this }).ShowDialog();
        }

        private void DXWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var login = new LoginUI();
            login.ShowDialog();
            if (!login.Model.IsLogin)
                Application.Current.Shutdown();
        }
    }
}
