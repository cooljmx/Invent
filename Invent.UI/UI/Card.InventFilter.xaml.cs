using System.Windows;
using DevExpress.Xpf.Core;
using InventUI.Models;

namespace InventUI.UI
{
    /// <summary>
    /// Interaction logic for ItemInvent.xaml
    /// </summary>
    public partial class CardInventFilter : DXWindow
    {
        private readonly ModelInvent model = null;
        public ModelInvent Model { get { return model; } }
        public bool OkClosed { get; private set; }

        public CardInventFilter(ModelInvent modelInvent)
        {
            InitializeComponent();
            model = modelInvent;
            OkClosed = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
            OkClosed = true;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
