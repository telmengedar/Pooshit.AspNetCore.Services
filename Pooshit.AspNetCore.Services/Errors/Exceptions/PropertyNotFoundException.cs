using System;

namespace Pooshit.AspNetCore.Services.Errors.Exceptions {
    
    /// <summary>
    /// thrown when a requested property was not found on a type
    /// </summary>
    public class PropertyNotFoundException : Exception {
        
        /// <summary>
        /// creates a new <see cref="PropertyNotFoundException"/>
        /// </summary>
        /// <param name="propertyName">name of property which was not found</param>
        /// <param name="dataType">type of data where property was supposed to be</param>
        public PropertyNotFoundException(Type dataType, string propertyName)
        : base($"Property '{propertyName}' not found on '{dataType.Name}'")
        {
            PropertyName = propertyName;
            DataType = dataType;
        }

        /// <summary>
        /// name of property which was not found
        /// </summary>
        public string PropertyName { get; }

        /// <summary>
        /// type of data where property was supposed to be
        /// </summary>
        public Type DataType { get; }
    }
}