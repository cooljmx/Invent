using DevExpress.Xpf.Grid;
using System.Windows;
using System.Windows.Input;

namespace InventUI.Tools
{
    class UIHelper
    {
        public static bool TestGridControlForRowCell(object source, MouseButtonEventArgs e)
        {
            return
                (((TableView) ((GridControl) e.Source).View).CalcHitInfo(e.OriginalSource as DependencyObject))
                    .InRowCell;
        }
    }
}
