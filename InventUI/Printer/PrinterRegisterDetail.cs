using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Invent.Interfaces;

namespace InventUI.Printer
{
    class PrinterRegisterDetail : IRegisterDetail
    {
        public int Id { get; set; }
        public int Npp { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
        public bool Questions { get; set; }
        public IRegister Register { get; set; }
        public IUser User { get; set; }
        public IBaseUnit WorkPlace { get; set; }
        public IBaseUnit Place { get; set; }
        public IBaseUnit DetailType { get; set; }
    }
}
