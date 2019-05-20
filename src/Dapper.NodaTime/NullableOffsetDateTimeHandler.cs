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
    public class NullableOffsetDateTimeHandler : SqlMapper.TypeHandler<OffsetDateTime?>
    {
        private NullableOffsetDateTimeHandler()
        {}

        public static readonly NullableOffsetDateTimeHandler Default = new NullableOffsetDateTimeHandler();

        public override void SetValue(IDbDataParameter parameter, OffsetDateTime? value)
        {
            parameter.Value = value?.ToDateTimeOffset();

            if (parameter is SqlParameter sqlParameter)
            {
                sqlParameter.SqlDbType = SqlDbType.DateTimeOffset;
            }
        }

        public override OffsetDateTime? Parse(object value)
        {
            if (value is DateTimeOffset dateTimeOffset)
            {
                return OffsetDateTime.FromDateTimeOffset(dateTimeOffset);
            }

            if (value is DBNull)
            {
                return default(OffsetDateTime?);
            }

            throw new DataException($"Cannot convert {value.GetType()} to {typeof(OffsetDateTime?)}");
        }
    }
}