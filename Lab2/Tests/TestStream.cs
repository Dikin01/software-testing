namespace Tests;

public class TestStream : Stream
{
    private readonly MemoryStream _stream = new();
    private readonly StreamReader _streamReader;
    private readonly StreamWriter _streamWriter;

    private bool _isOpen;
    private bool _writable;
    private long _lastLinePosition;

    public override bool CanRead => _isOpen;
    public override bool CanSeek => _isOpen;
    public override bool CanWrite => _writable;
    public override long Length { get; }

    public override long Position
    {
        get => _stream.Position;
        set => _stream.Position = value;
    }

    public TestStream(bool canRead = true, bool canWrite = true)
    {
        _streamReader = new StreamReader(_stream);
        _streamWriter = new StreamWriter(_stream);

        _isOpen = canRead;
        _writable = canWrite;

        Length = 0;
    }

    public override void Flush()
    {
        _streamWriter.Flush();
        _stream.Flush();
    }

    public override int Read(byte[] buffer, int offset, int count)
    {
        return _stream.Read(buffer, offset, count);
    }

    public override long Seek(long offset, SeekOrigin origin)
    {
        return _stream.Seek(offset, origin);
    }

    public override void SetLength(long value)
    {
        _stream.SetLength(value);
    }

    public override void Write(byte[] buffer, int offset, int count)
    {
        _stream.Write(buffer, offset, count);
    }

    public void WriteLine(string value)
    {
        _lastLinePosition = _stream.Position;
        _streamWriter.WriteLine(value);
        Flush();
    }

    public string? ReadLastLine()
    {
        _stream.Position = _lastLinePosition;
        return _streamReader.ReadLine();
    }

    public override void Close()
    {
        _writable = false;
        _isOpen = false;
        _stream.Close();
        base.Close();
    }

    public void ResetPosition() => _stream.Position = 0;
}