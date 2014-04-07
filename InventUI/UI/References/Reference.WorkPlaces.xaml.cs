using DevExpress.Xpf.Core;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Grid;
using InventUI.Models.References;
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

namespace InventUI.UI.References
{
    /// <summary>
    /// Interaction logic for WorkPlaces.xaml
    /// </summary>
    public partial class ReferenceWorkPlaces : DXWindow
    {
        private readonly ModelReferenceWorkPlaces model = new ModelReferenceWorkPlaces();

        public ModelReferenceWorkPlaces Model { get { return model; } }

        public ReferenceWorkPlaces()
        {
            InitializeComponent();
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            model.Save();
            Close();
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ButtonAddItem_Click(object sender, RoutedEventArgs e)
        {
            model.AddItem();
        }

        private void ButtonDeleteItem_Click(object sender, RoutedEventArgs e)
        {
            if (model.FocusedGridRow != null && MessageBox.Show("Дейсвительно удалить запись?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                model.DeleteFocusedItem();
        }

        private void DXWindow_Loaded(object sender, RoutedEventArgs e)
        {
            model.Load();
        }

        private void DXWindow_Closed(object sender, EventArgs e)
        {
            model.Dispose();
        }

        private void Grid_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (model.InvokeFocusedItem())
                Close();
        }
    }
}
