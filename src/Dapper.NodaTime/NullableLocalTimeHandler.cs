using System;
using System.Data;
using System.Data.SqlClient;
using NodaTime;

#if NETSTANDARD1_3
using DataException = System.InvalidOperationException;
#endif

namespace Dapper.NodaTime
{
    // TODO: Test
    public class NullableLocalTimeHandler : SqlMapper.TypeHandler<LocalTime?>
    {
        private NullableLocalTimeHandler()
        {}

        public static readonly NullableLocalTimeHandler Default = new NullableLocalTimeHandler();

        public override void SetValue(IDbDataParameter parameter, LocalTime? value)
        {
            parameter.Value = value.HasValue
                ? TimeSpan.FromTicks(value.Value.TickOfDay)
                : default(TimeSpan?);

            if (parameter is SqlParameter sqlParameter)
            {
                sqlParameter.SqlDbType = SqlDbType.Time;
            }
        }

        public override LocalTime? Parse(object value)
        {
            if (value is TimeSpan timeSpan)
            {
                return LocalTime.FromTicksSinceMidnight(timeSpan.Ticks);
            }

            if (value is DateTime dateTime)
            {
                return LocalTime.FromTicksSinceMidnight(dateTime.TimeOfDay.Ticks);
            }

            if (value is DBNull)
            {
                return default(LocalTime?);
            }

            throw new DataException($"Cannot convert {value.GetType()} to {typeof(LocalTime?)}");
        }
    }
}