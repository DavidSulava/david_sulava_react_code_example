using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGear.Common.Extensions {
    public static class StreamExtensions {
        public static byte[] ToByteArray(this Stream stream) {
            using (MemoryStream ms = new MemoryStream()) {
                stream.CopyTo(ms);
                stream.Dispose();
                return ms.ToArray();
            }
        }
    }
}
