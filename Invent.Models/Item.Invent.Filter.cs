using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Invent.Entities;
using InventUI.NHibernate;

namespace InventUI.Models
{
    public class ItemInventFilter : VirtualNotifyPropertyChanged
    {
        private bool checkFlag;
        private int id;
        private string name;

        public bool Checked {
            get { return checkFlag; }
            set { checkFlag = value; NotifyPropertyChanged("Checked"); }
        }
        public int Id {
            get { return id; }
            set { id = value; NotifyPropertyChanged("Id"); }
        }
        public string Name {
            get { return name; }
            set { name = value; NotifyPropertyChanged("Name"); }
        }
    }
}
