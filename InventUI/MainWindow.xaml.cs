using System.Windows.Forms.VisualStyles;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DevExpress.Xpf.Docking;
using Common;
using InventUI.NHibernate;
using System.Collections.ObjectModel;
using InventUI.Tools;
using InventUI.UI.MainWindowControls;
using NHibernate.Transform;
using InventUI.UI.References;

namespace InventUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : DXWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void buttonOpenRegister(object sender, ItemClickEventArgs e)
        {
            DocumentPanelManager.SelectPanel(DockLayoutManager, DocumentContainer, typeof (RegisterUserControl), true);
        }

        private void buttonOpenRegisterDetails(object sender, ItemClickEventArgs e)
        {
            DocumentPanelManager.SelectPanel(DockLayoutManager, DocumentContainer, typeof(RegisterDetailsUserControl), true);
        }

        private void buttonOpenInvent(object sender, ItemClickEventArgs e)
        {
            DocumentPanelManager.SelectPanel(DockLayoutManager, DocumentContainer, typeof(InventUserControl), true);
        }

        private void buttonOpenWorkPlacesReference(object sender, ItemClickEventArgs e)
        {
            (new ReferenceWorkPlaces() { Owner = this }).ShowDialog();
        }

        private void buttonOpenSpUsersReference(object sender, ItemClickEventArgs e)
        {
            (new ReferenceSpUsers() { Owner = this }).ShowDialog();
        }
    }
}
