using System;
using Barcode.Encoder;
using Invent.Interfaces;
using System.IO;
using bpac;

namespace Printer.BrotherРТ2700VP.Simple
{
    public class StickPrinter : Invent.Interfaces.IPrinter
    {
        public void Print(IRegisterDetail item)
        {
            string template = string.Concat(AppDomain.CurrentDomain.BaseDirectory, @"drivers\Simple.lbx");

            string barcode = Coder.Calculate(Convert.ToUInt32(item.Id));
            string name = item.DetailType.Id > 0 ? item.DetailType.Name : item.Name;

            bpac.DocumentClass doc = new DocumentClass();
            if (doc.Open(template))
            {
                doc.GetObject("Code").Text = barcode;
                doc.GetObject("InvNum").Text = item.Register.InvNumber;
                doc.GetObject("Name").Text = string.Format("{0}. {1}", item.Npp, name);

                doc.StartPrint("",  PrintOptionConstants.bpoChainPrint | PrintOptionConstants.bpoAutoCut);
                doc.PrintOut(1, PrintOptionConstants.bpoChainPrint | PrintOptionConstants.bpoAutoCut);
                doc.EndPrint();
                doc.Close();
            }
            else
            {
                throw new Exception("Невозможно открыть шаблон");
            }
        }
    }
}
