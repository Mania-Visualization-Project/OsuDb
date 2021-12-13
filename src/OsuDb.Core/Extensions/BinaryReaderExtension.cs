using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsuDb.Core.Extensions
{
    internal static class BinaryReaderExtension
    {
        public static string ReadOsuString(this BinaryReader reader)
        {
            var head = reader.ReadByte();
            if (head == 0x00) return string.Empty;
            if (head != 0x0B) throw new InvalidDataException("invalid string format.");
            return reader.ReadString();
        }
    }
}
