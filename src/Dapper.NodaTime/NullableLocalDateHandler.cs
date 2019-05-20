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
    public class NullableLocalDateHandler : SqlMapper.TypeHandler<LocalDate?>
    {
        private NullableLocalDateHandler()
        {}

        public static readonly NullableLocalDateHandler Default = new NullableLocalDateHandler();

        public override void SetValue(IDbDataParameter parameter, LocalDate? value)
        {
            parameter.Value = value?.AtMidnight().ToDateTimeUnspecified();

            if (parameter is SqlParameter sqlParameter)
            {
                sqlParameter.SqlDbType = SqlDbType.Date;
            }
        }

        public override LocalDate? Parse(object value)
        {
            if (value is DateTime dateTime)
            {
                return LocalDateTime.FromDateTime(dateTime).Date;
            }

            if (value is DBNull)
            {
                return default(LocalDate?);
            }

            throw new DataException($"Cannot convert {value.GetType()} to {typeof(LocalDate?)}");
        }
    }
}