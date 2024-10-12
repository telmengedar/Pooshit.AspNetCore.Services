using System;

namespace Pooshit.AspNetCore.Services.Errors.Exceptions {

    /// <summary>
    /// exception triggered when requested data was not found
    /// </summary>
    public class NotFoundException : Exception {

        /// <summary>
        /// creates a new <see cref="NotFoundException"/>
        /// </summary>
        /// <param name="dataType">type of data not found</param>
        /// <param name="id">id of data not found</param>
        public NotFoundException(Type dataType, long id)
            : this(dataType, id.ToString()) {
        }

        /// <summary>
        /// creates a new <see cref="NotFoundException"/>
        /// </summary>
        /// <param name="dataType">type of data not found</param>
        /// <param name="id">id of data not found</param>
        public NotFoundException(Type dataType, string id)
        : this(dataType, id, $"'{dataType.Name}' with id '{id}' not found") {
        }

        /// <summary>
        /// creates a new <see cref="NotFoundException"/>
        /// </summary>
        /// <param name="dataType">type of data not found</param>
        /// <param name="id">id of data not found</param>
        /// <param name="message">message to include</param>
        public NotFoundException(Type dataType, string id, string message)
            : base(message) {
            DataType = dataType;
            Id = id;
        }

        /// <summary>
        /// type of data not found
        /// </summary>
        public Type DataType { get; set; }

        /// <summary>
        /// id of data not found
        /// </summary>
        public string Id { get; set; }
    }
}