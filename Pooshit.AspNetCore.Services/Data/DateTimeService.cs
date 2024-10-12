using System;

namespace Pooshit.AspNetCore.Services.Data;

/// <inheritdoc />
public class DateTimeService : IDateTimeService {
        
    /// <inheritdoc />
    public DateTime Now() {
        return DateTime.Now;
    }
}