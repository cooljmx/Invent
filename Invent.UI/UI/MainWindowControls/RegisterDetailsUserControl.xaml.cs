using DevExpress.Xpf.Bars;
using Invent.Entities;
using Invent.Models;
using InventUI.Models;
using InventUI.Tools;
using System.Windows;
using System.Windows.Input;

namespace InventUI.UI.MainWindowControls
{
    /// <summary>
    /// Interaction logic for RegisterDetailsUserControl.xaml
    /// </summary>
    public partial class RegisterDetailsUserControl : IDocumentPanelManager
    {
        private readonly ModelRegisterDetail<ItemCommon, Register> model = new ModelRegisterDetail<ItemCommon, Register>();

        public string PanelTitle { get { return "Общий реестр компонентов"; } }

        public RegisterDetailsUserControl()
        {
            InitializeComponent();
            DataContext = model;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            model.ReloadModel();
        }

        private void grid_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (!UIHelper.TestGridControlForRowCell(sender, e))
                return;
            var card = new CardRegister{
                Width = SystemParameters.PrimaryScreenWidth - 150,
                Height = SystemParameters.PrimaryScreenHeight - 150,
                MinWidth = 800,
                MinHeight = 600,
                Owner = Window.GetWindow(this)
            };
            card.Model.Load(model.FocusedGridRow);
            card.ShowDialog();
            model.ReloadItem(model.FocusedGridRow);
        }

        private void PrintButton_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // TODO: остановка события
            e.Handled = true;
        }

        private void PrintButton_OnClick(object sender, RoutedEventArgs e)
        {
            model.PrintFocusedItem();
        }

        private void gridMain_Loaded(object sender, RoutedEventArgs e)
        {
            //gridMain.RestoreLayoutFromXml();
        }

        private void BarButtonItemToExcel_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var path = string.Concat(System.IO.Path.GetTempPath(), "registerdetails.xls");
            MainGridView.ExportToXls(path);
            System.Diagnostics.Process.Start(path);
        }
    }
}
