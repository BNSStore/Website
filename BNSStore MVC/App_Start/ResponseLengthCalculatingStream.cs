using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace SLouple.App_Start
{
    public class ResponseLengthCalculatingStream : MemoryStream
    {
        private readonly Stream responseStream;
        public long responseSize = 0;
        public ResponseLengthCalculatingStream(Stream responseStream)
        {
            this.responseStream = responseStream;
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            this.responseSize += count;
            this.responseStream.Write(buffer, offset, count);
        }

        public override void Flush()
        {
            var responseSize = this.responseSize;
            base.Flush();
        }
    }
}