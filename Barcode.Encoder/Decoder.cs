using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rade.Hash;

namespace Barcode.Encoder
{
    public static class Decoder
    {
        public static uint Id(string barcode)
        {
            if (barcode.Length != 12) throw new Exception();
            return Convert.ToUInt32(barcode.Substring(5, 7));
        }
        public static ushort Crc(string barcode)
        {
            if (barcode.Length != 12) throw new Exception();
            return Convert.ToUInt16(barcode.Substring(0, 5));
        }
        public static bool IsValidate(string barcode)
        {
            var id = Convert.ToUInt32(Id(barcode));
            var buffer = BitConverter.GetBytes(id).Reverse().ToArray();
            var crc16 = ComputeCrc(buffer);

            return crc16 == Crc(barcode);
        }
        private static ushort ComputeCrc(byte[] val)
        {
            var crc16 = new Crc16(Crc16Mode.Standard);
            return crc16.ComputeChecksum(val);
        }
    }
}
