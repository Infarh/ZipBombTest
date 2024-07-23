namespace ZipBombTest.IO;

internal sealed class EnumToStream(IEnumerable<byte> bytes) : Stream
{
    private IEnumerator<byte>? _Enumerator;

    public override bool CanRead => true;

    public override bool CanSeek => false;

    public override bool CanWrite => false;

    public override long Length => throw new NotSupportedException();

    public override long Position { get => throw new NotSupportedException(); set => throw new NotSupportedException(); }

    public override void Flush() => throw new NotSupportedException();

    public override int Read(byte[] buffer, int offset, int count)
    {
        _Enumerator ??= bytes.GetEnumerator();

        for(var i = 0; i < count; i++)
        {
            if(!_Enumerator.MoveNext()) 
                return i;
            buffer[offset + i] = _Enumerator.Current;
        }

        return count;
    }

    public override long Seek(long offset, SeekOrigin origin) => throw new NotSupportedException();

    public override void SetLength(long value) => throw new NotSupportedException();

    public override void Write(byte[] buffer, int offset, int count) => throw new NotSupportedException();

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);

        if(!disposing) return;

        _Enumerator?.Dispose();
    }
}