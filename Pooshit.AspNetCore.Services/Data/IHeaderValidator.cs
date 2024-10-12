namespace Pooshit.AspNetCore.Services.Data {

    /// <summary>
    /// interface for a validator of format data in stream
    /// </summary>
    public interface IHeaderValidator {

        /// <summary>
        /// determines whether the validator is to be executed as soon as the stream is available
        /// </summary>
        bool ValidateOnInit { get; }

        /// <summary>
        /// size of expected header in bytes
        /// </summary>
        int HeaderSize { get; }

        /// <summary>
        /// validates the header bytes
        /// </summary>
        void Validate(byte[] headerdata);
    }
}