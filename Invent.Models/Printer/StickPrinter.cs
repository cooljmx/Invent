using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Invent.Interfaces;

namespace InventUI.Printer
{
    class StickPrinter
    {
        public void Print(IRegisterDetail item)
        {
            var assembly = Assembly.LoadFrom(string.Concat(AppDomain.CurrentDomain.BaseDirectory, @"Drivers\Printer.BrotherРТ2700VP.Simple.dll"));
            Type t = assembly.GetExportedTypes().FirstOrDefault(x=>x.GetInterfaces().Contains(typeof(IPrinter)));
            if (t != null)
            {
                var printer = (IPrinter) Activator.CreateInstance(t);
                printer.Print(item);
            }
        }
    }
}
