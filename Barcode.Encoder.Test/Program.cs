using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Barcode.Encoder.Test
{
    
    class Program
    {
        private static void Main(string[] args)
        {
            const string crc = "01920";
            const string id = "0000010";
            const string barcode = crc + id;
            Console.WriteLine(Decoder.Crc(barcode));
            Console.WriteLine(Decoder.Id(barcode));
            Console.WriteLine(Decoder.IsValidate(barcode) ? "Validate" : "Invalidate");
            Console.WriteLine(Coder.Calculate(Convert.ToUInt32(id)));
            Console.ReadKey();
        }
    }
}
