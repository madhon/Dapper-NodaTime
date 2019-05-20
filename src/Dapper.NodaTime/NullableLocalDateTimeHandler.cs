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
    public class NullableLocalDateTimeHandler : SqlMapper.TypeHandler<LocalDateTime?>
    {
        private NullableLocalDateTimeHandler()
        {}

        public static readonly NullableLocalDateTimeHandler Default = new NullableLocalDateTimeHandler();

        public override void SetValue(IDbDataParameter parameter, LocalDateTime? value)
        {
            parameter.Value = value?.ToDateTimeUnspecified();

            if (parameter is SqlParameter sqlParameter)
            {
                sqlParameter.SqlDbType = SqlDbType.DateTime2;
            }
        }

        public override LocalDateTime? Parse(object value)
        {
            if (value is DateTime dateTime)
            {
                return LocalDateTime.FromDateTime(dateTime);
            }

            if (value is DBNull)
            {
                return default(LocalDateTime?);
            }

            throw new DataException($"Cannot convert {value.GetType()} to {typeof(LocalDateTime?)}");
        }
    }
}