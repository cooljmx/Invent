using System;
using System.Collections.Generic;
using System.IO;
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
using System.Xml.Serialization;
using Common;
using DevExpress.Xpf.Bars;
using InventUI.Models;
using InventUI.Tools;
using Microsoft.Win32;

namespace InventUI.UI.MainWindowControls
{
    /// <summary>
    /// Interaction logic for InventUserControl.xaml
    /// </summary>
    public partial class InventUserControl : UserControl, IDocumentPanelManager
    {
        private ModelInvent model = new ModelInvent();
        private string inputCache = string.Empty;
        public string PanelTitle { get { return "Инвентаризация"; } }
        public InventUserControl()
        {
            InitializeComponent();
            DataContext = model;
        }

        private void InventUserControl_OnLoaded(object sender, RoutedEventArgs e)
        {
            model.ReloadFilters();
        }

        private void BarButtonItemNew_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var filterCard = new CardInventFilter(model)
            {
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                //Width = SystemParameters.PrimaryScreenWidth - 150,
                //Height = SystemParameters.PrimaryScreenHeight - 150,
                MinWidth = 800,
                MinHeight = 600
            };
            filterCard.ShowDialog();
            if (filterCard.OkClosed)
                model.ReloadModel();
        }

        private void BarButtonItemSave_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var dialog = new SaveFileDialog {Filter = "ItemInvent file|*.inv", DefaultExt = "inv"};
            if ((bool) !dialog.ShowDialog()) return;
            using (var stream = new FileStream(dialog.FileName, FileMode.Create, FileAccess.Write))
            {
                var serializer = new XmlSerializer(typeof (ModelInvent));
                serializer.Serialize(stream, model);
            }
        }

        private void BarButtonItemLoad_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var dialog = new OpenFileDialog {Filter = "ItemInvent file|*.inv"};
            if ((bool) !dialog.ShowDialog()) return;
            using (var stream = new FileStream(dialog.FileName, FileMode.Open, FileAccess.Read))
            {
                var serializer = new XmlSerializer(typeof (ModelInvent));
                model = (ModelInvent) serializer.Deserialize(stream);
                DataContext = model;
            }
        }

        private void LocateBarcode(string barcode)
        {
            var id = Barcode.Encoder.Decoder.Id(barcode);
            var index = MainGridControl.FindRowByValue("DetailId", id);
            if (index >= -1)
            {
                MainGridControl.View.FocusedRowHandle = index;
                var item = model.Data.FirstOrDefault(a => a.DetailId == id);
                if (item != null) item.InventDone = true;
            }
        }
        private void InventUserControl_OnKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter)
                {
                    LocateBarcode(inputCache);
                    inputCache = string.Empty;
                }
                else
                {
                    switch (e.Key)
                    {
                        case Key.D0:
                            inputCache += '0';
                            break;
                        case Key.D1:
                            inputCache += '1';
                            break;
                        case Key.D2:
                            inputCache += '2';
                            break;
                        case Key.D3:
                            inputCache += '3';
                            break;
                        case Key.D4:
                            inputCache += '4';
                            break;
                        case Key.D5:
                            inputCache += '5';
                            break;
                        case Key.D6:
                            inputCache += '6';
                            break;
                        case Key.D7:
                            inputCache += '7';
                            break;
                        case Key.D8:
                            inputCache += '8';
                            break;
                        case Key.D9:
                            inputCache += '9';
                            break;
                    }
                }
            }
            catch
            {
                inputCache = string.Empty;
            }
        }
    }
}