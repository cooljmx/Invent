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
using System.Windows.Shapes;
using DevExpress.Xpf.Core;
using InventUI.Models.References;

namespace InventUI.UI.References
{
    /// <summary>
    /// Interaction logic for Referense.xaml
    /// </summary>
    public partial class ReferenceSpUsers : DXWindow
    {
        private readonly ModelReferenceSpUsers model = new ModelReferenceSpUsers();
        public ModelReferenceSpUsers Model { get { return model; } }
        public ReferenceSpUsers()
        {
            InitializeComponent();
        }

        private void ReferenseSpUsers_OnClosed(object sender, EventArgs e)
        {
            model.Dispose();
        }

        private void ReferenseSpUsers_OnLoaded(object sender, RoutedEventArgs e)
        {
            model.Load();}
    }
}
