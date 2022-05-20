using System;

namespace FC.CodeFlix.Catalog.EndToEndTests.Extensions;
internal static class DateTimeExtensions
{
    public static DateTime TrimMillisseconds(this DateTime dateTime)
    {
        return new DateTime(
            dateTime.Year,
            dateTime.Month,
            dateTime.Day,
            dateTime.Hour,
            dateTime.Minute,
            dateTime.Second,
            0,
            dateTime.Kind
        );

    }
        
}
