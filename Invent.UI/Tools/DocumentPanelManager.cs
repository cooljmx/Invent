using System.Windows.Controls;
using System.Windows.Forms.VisualStyles;
using DevExpress.Xpf.Docking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InventUI.Tools
{
    class DocumentPanelManager
    {
        public static DocumentPanel SelectPanel(DockLayoutManager manager, DocumentGroup baseGroup, Type controlType, bool autoActivate = false)
        {
            DocumentPanel panel = null;
            if (controlType.GetInterfaces().Contains(typeof (IDocumentPanelManager)))
            {
                // Тип реализует интерфейс
                string controlId = controlType.FullName;
                foreach (
                    DocumentPanel localpanel in
                        baseGroup.Items.Where(
                            x => x is DocumentPanel && (x as DocumentPanel).Control is IDocumentPanelManager))
                {
                    var panelControlId = localpanel.Control.GetType().ToString();
                    if (panelControlId == controlId)
                    {
                        panel = localpanel;
                        break;
                    }
                }

                if (panel == null)
                {
                    // Не найден - создать
                    panel = manager.DockController.AddDocumentPanel(baseGroup);
                    panel.Content = Activator.CreateInstance(controlType);
                    panel.Caption = (panel.Control as IDocumentPanelManager).PanelTitle;
                }
                if (autoActivate)
                    manager.Activate(panel);
            }
            else
            {
                // Тип не реализует интерфейс
                panel = manager.DockController.AddDocumentPanel(baseGroup);
                panel.Content = Activator.CreateInstance(controlType);
                if (autoActivate)
                    manager.Activate(panel);
            }
            return
                panel;} 
    }

    interface IDocumentPanelManager
    {
        string PanelTitle { get; }
    }
}
