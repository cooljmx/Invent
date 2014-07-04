using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Invent.Interfaces;

namespace InventUI.Printer
{
    class PrinterRegister : IRegister
    {
        public int Id { get; set; }
        public string InvName { get; set; }
        public string InvNumber { get; set; }
        public DateTime DateInput { get; set; }
        public DateTime DateStatus { get; set; }
        public string Comment { get; set; }
        public IUser UserMaterial { get; set; }
        public IUser UserOwner { get; set; }
        public IBaseUnit Status { get; set; }
    }
}
