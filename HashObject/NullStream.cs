using System;
using System.IO;

namespace HashObject
{
    internal class NullStream : Stream
    {
        public override void Flush()
        {
            // Do nothing.
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotImplementedException();
        }

        public override void SetLength(long value)
        {
            throw new NotImplementedException();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            // Do nothing.
        }

        public override bool CanRead => NotImplementedException<bool>();
        public override bool CanSeek => NotImplementedException<bool>();
        public override bool CanWrite => true;
        public override long Length => NotImplementedException<long>();

        public override long Position
        {
            get { return NotImplementedException<long>(); }
            set { throw new NotImplementedException(); }
        }

        private static T NotImplementedException<T>()
        {
            throw new NotImplementedException();
        }
    }
}
