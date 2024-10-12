using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Pooshit.AspNetCore.Services.Data;

/// <summary>
/// checks the header of a stream while data is requested
/// </summary>
public class HeaderCheckStream : Stream {
    readonly Stream basestream;
    readonly IHeaderValidator validator;
    int checkmode = 1;

    readonly List<byte> header = [];

    /// <summary>
    /// creates a new <see cref="HeaderCheckStream"/>
    /// </summary>
    /// <param name="basestream">stream used to serve data</param>
    /// <param name="validator">validator for header data</param>
    public HeaderCheckStream(Stream basestream, IHeaderValidator validator) {
        this.basestream = basestream;
        this.validator = validator;
        if(validator.ValidateOnInit) {
            byte[] buffer = new byte[validator.HeaderSize];
            int result = basestream.Read(buffer, 0, validator.HeaderSize);
            if(result != validator.HeaderSize)
                throw new InvalidOperationException("Stream does not provide enough data to check for header information");
            header.AddRange(buffer);
            validator.Validate(buffer);
            checkmode = 2;
        }
    }

    /// <inheritdoc />
    public override void Flush() {
        basestream.Flush();
    }

    /// <inheritdoc />
    public override int Read(byte[] buffer, int offset, int count) {
        int result = 0;
        if(checkmode == 2) {
            for(int i = 0; i < count && i < header.Count; ++i)
                buffer[offset + i] = header[i];

            if(count < header.Count) {
                header.RemoveRange(0, count);
                return count;
            }

            checkmode = 0;
            offset += header.Count;
            count -= header.Count;
            result = header.Count;
        }

        result += basestream.Read(buffer, offset, count);
        if(checkmode == 1) {
            if(result == 0)
                throw new InvalidOperationException("Stream ended before header was successfully validated");

            header.AddRange(buffer.Skip(offset).Take(count));
            if(header.Count >= validator.HeaderSize) {
                validator.Validate(header.ToArray());
                checkmode = 0;
            }
        }

        return result;
    }

    /// <inheritdoc />
    public override long Seek(long offset, SeekOrigin origin) {
        if(checkmode > 0)
            throw new InvalidOperationException("Can't seek in stream which is still checked for header");

        return basestream.Seek(offset, origin);
    }

    /// <inheritdoc />
    public override void SetLength(long value) {
        throw new System.NotImplementedException();
    }

    /// <inheritdoc />
    public override void Write(byte[] buffer, int offset, int count) {
        throw new System.NotImplementedException();
    }

    /// <inheritdoc />
    public override bool CanRead => basestream.CanRead;

    /// <inheritdoc />
    public override bool CanSeek => basestream.CanSeek;

    /// <inheritdoc />
    public override bool CanWrite => basestream.CanWrite;

    /// <inheritdoc />
    public override long Length => basestream.Length;

    /// <inheritdoc />
    public override long Position {
        get => basestream.Position;
        set => basestream.Position = value;
    }
}