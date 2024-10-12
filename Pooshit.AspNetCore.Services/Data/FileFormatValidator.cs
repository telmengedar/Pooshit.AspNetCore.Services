namespace Pooshit.AspNetCore.Services.Data;

/// <inheritdoc />
public class FileFormatValidator : IHeaderValidator {

    /// <inheritdoc />
    public bool ValidateOnInit => true;

    public int HeaderSize => 1;

    public void Validate(byte[] headerdata) {
        switch ((char)headerdata[0]) {
            case '[':
            case '{':
                IsJson = true;
                break;
        }
    }

    /// <summary>
    /// determines whether the data could be json
    /// </summary>
    public bool IsJson { get; private set; }
}