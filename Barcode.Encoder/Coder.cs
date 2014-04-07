using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rade.Hash;

namespace Barcode.Encoder
{
    public static class Coder
    {
        public static string Calculate(uint id)
        {
            var buffer = BitConverter.GetBytes(id).Reverse().ToArray();
            var crc16 = ComputeCrc(buffer);
            return string.Format("{0:00000}{1:0000000}", crc16, id);
        }
        private static ushort ComputeCrc(byte[] val)
        {
            var crc16 = new Crc16(Crc16Mode.Standard);
            return crc16.ComputeChecksum(val);
        }
    }
}
