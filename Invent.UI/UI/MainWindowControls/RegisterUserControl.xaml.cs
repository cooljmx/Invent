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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Common;
using DevExpress.Xpf.Bars;
using Invent.Entities;
using InventUI.Models;
using DevExpress.Xpf.Grid;
using InventUI.Tools;

namespace InventUI.UI.MainWindowControls
{
    /// <summary>
    /// Interaction logic for RegisterUserControl.xaml
    /// </summary>
    public partial class RegisterUserControl : UserControl, IDocumentPanelManager
    {
        //private readonly RegisterCollection model = new RegisterCollection();
        private readonly ModelRegister<ItemCommon, Register> model = new ModelRegister<ItemCommon, Register>();

        public string PanelTitle { get { return "Реестр учета"; } }

        public RegisterUserControl()
        {
            InitializeComponent();
            this.DataContext = model;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                model.ReloadModel();
            }
            catch (Exception ex)
            {
                // TODO:
                throw ex;
            }
        }

        private void grid_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (!UIHelper.TestGridControlForRowCell(sender, e))
                    return;
                var card = new CardRegister()
                {
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
            catch (Exception)
            {
                // TODO
                throw;
            }

/*
            if (!(((TableView)((GridControl) e.Source).View).CalcHitInfo(e.OriginalSource as DependencyObject)).InRowCell)
                return;
            var row = (ItemCommon)((GridControl)sender).GetFocusedRow();
            if (row != null)
            {
                try
                {
                    var card = new CardRegister()
                    {
                        Width = SystemParameters.PrimaryScreenWidth - 150,
                        Height = SystemParameters.PrimaryScreenHeight - 150,
                        MinWidth = 800,
                        MinHeight = 600,
                        Owner = Window.GetWindow(this)
                    };
                    card.Model.Load(row);
                    card.ShowDialog();
                    model.ReloadItem(row);
                }
                catch (Exception)
                {
                    // TODO:
                    throw;
                }
            }
 * */
        }

        private void BarButtonItemAdd_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var card = new CardRegister
            {
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                Width = SystemParameters.PrimaryScreenWidth - 150,
                Height = SystemParameters.PrimaryScreenHeight - 150,
                MinWidth = 800,
                MinHeight = 600,
                Owner = Window.GetWindow(this)
            };
            card.Model.Create();
            card.ShowDialog();

            try
            {
                model.ReloadModel();
            }
            catch (Exception)
            {
                // TODO:
                throw;
            }
        }
        private void BarButtonItemRemove_OnItemClick(object sender, ItemClickEventArgs e)
        {
            if (model.FocusedGridRow != null &&
                MessageBox.Show("Дейсвительно удалить запись?", "Подтверждение", MessageBoxButton.YesNo,
                    MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    model.DeleteFocusedItem<int>();
                }
                catch (Exception)
                {
                    // TODO:
                    throw;
                }
            }
        }

        private void BarButtonItemToExcel_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var path = string.Concat(System.IO.Path.GetTempPath(), "register.xls");
            MainGridView.ExportToXls(path);
            System.Diagnostics.Process.Start(path);
        }
    }
}
