﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Invent.Interfaces;

namespace InventUI.Printer
{
    class PrinterPlace : IBaseUnit
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
