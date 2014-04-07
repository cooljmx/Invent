using Common;
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
using System.Windows.Shapes;
using Invent.Entities;
using InventUI.Models;
using DevExpress.Xpf.Grid;
using InventUI.UI.References;

namespace InventUI.UI
{
    /// <summary>
    /// Interaction logic for CardRegister.xaml
    /// </summary>
    public partial class CardRegister : DXWindow
    {
        private readonly ModelCardRegister model = new ModelCardRegister();
        public ModelCardRegister Model { get { return model; } }

        public CardRegister()
        {
            InitializeComponent();
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var errors = Model.Validate();
                if (errors.Any())
                {
                    MessageBox.Show(string.Format("Обнаружены следующие ошибки:\n{0}", string.Join(Environment.NewLine, errors.Select(x=>string.Format(" - {0}", x)))));
                    return;
                }

                Model.Save();
                Close();
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message, "Ошибка сохранения", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ButtonAddDetail_Click(object sender, RoutedEventArgs e)
        {
            var item = new RegisterDetails
            {
                Npp = model.Details.Any() ? model.Details.Max(x => x.Npp) + 1 : 1,
                DetailType = model.DetailTypesList.FirstOrDefault(x => x.Id == 0),
                WorkPlace = model.WorkPlacesList.FirstOrDefault(x => x.Id == 0),
                //DetailType = Singleton<ReferencesCollection>.Instance.SpDetailTypesCollection.FirstOrDefault(x => x.Id == 0),
                //WorkPlace = Singleton<ReferencesCollection>.Instance.WorkPlacesCollection.FirstOrDefault(x => x.Id == 0),
                Parent = model.RegisterItem
            };
            model.Details.Add(item);
        }
        private void ButtonDeleteDetail_Click(object sender, RoutedEventArgs e)
        {
            if (model.DetailsFocusedGridRow != null && MessageBox.Show("Дейсвительно удалить запись?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                model.DeleteFocusedItem();
        }

        private void ButtonWorkPlaceReference_Click(object sender, RoutedEventArgs e)
        {
            var card = new ReferenceWorkPlaces() { Owner = this };
            card.Model.ChooseItem += (o, args) => model.DetailsFocusedGridRow.WorkPlace = args.Item;
            card.ShowDialog();
        }

        private void DXWindow_Closed(object sender, EventArgs e)
        {
            model.Dispose();
        }
    }
}
