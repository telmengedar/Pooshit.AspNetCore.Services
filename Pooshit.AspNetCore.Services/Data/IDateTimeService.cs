using System;

namespace Pooshit.AspNetCore.Services.Data {
    
    /// <summary>
    /// service handling time operations
    /// </summary>
    public interface IDateTimeService {
        
        /// <summary>
        /// provides the current time
        /// </summary>
        /// <returns>current datetime</returns>
        DateTime Now();
    }
}