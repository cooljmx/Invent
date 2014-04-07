using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventUI.NHibernate;

namespace InventUI.Models
{
    public class ItemInvent : ItemCommon
    {
        private bool inventDone;
        public virtual bool InventDone { get { return inventDone; } set { inventDone = value; OverridedPropertyChanged("InventDone"); } }
    }
}
