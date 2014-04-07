using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Invent.Interfaces;

namespace InventUI.Printer
{
    class PrinterUser : IUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Job { get; set; }
    }
}
