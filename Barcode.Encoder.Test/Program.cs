using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using br.com.arcnet.barcodegenerator;
using System.Drawing.Printing;

namespace Barcode.Encoder.Test
{
    
    class Program
    {
        private static void Main(string[] args)
        {
            /*using (var stream = new FileStream("d:\\barcode.bmp", FileMode.Create, FileAccess.Write))
            {
                var b = new br.com.arcnet.barcodegenerator.Barcode();
                b.IncludeLabel = true;
                b.Encode(TypeBarcode.Code128C, "195840000238");
                Console.WriteLine(b.EncodedImage.Size);
                b.SaveImage(stream, SaveTypes.Bmp);
            }
            Console.ReadKey();*/
            
            var doc = new PrintDocument();
            doc.PrintPage += doc_PrintPage;
            doc.Print();/*const string crc = "19584";}
            const string id = "0000238";
            const string barcode = crc + id;
            Console.WriteLine(Decoder.Crc(barcode));
            Console.WriteLine(Decoder.Id(barcode));
            Console.WriteLine(Decoder.IsValidate(barcode) ? "Validate" : "Invalidate");
            Console.WriteLine(Coder.Calculate(Convert.ToUInt32(id)));
            Console.ReadKey();*/
        }

        static void doc_PrintPage(object sender, PrintPageEventArgs e)
        {
            using (var stream = new MemoryStream())
            {
                var b = new br.com.arcnet.barcodegenerator.Barcode {IncludeLabel = true};
                b.Encode(TypeBarcode.Code128C, Coder.Calculate(Convert.ToUInt32(238)));
                b.SaveImage(stream, SaveTypes.Bmp);
                stream.Seek(0, 0);
                var img = Image.FromStream(stream); 
                var loc = new Point(100, 100);
                e.Graphics.DrawImage(img, loc);
            }
        }
    }
}
