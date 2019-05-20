namespace Dapper.NodaTime
{
    public static class DapperNodaTimeSetup
    {
        /// <summary>
        /// Convenience method to register all type handlers for Noda Time
        /// </summary>
        public static void Register()
        {
            SqlMapper.AddTypeHandler(InstantHandler.Default);
            SqlMapper.AddTypeHandler(NullableInstantHandler.Default);

            SqlMapper.AddTypeHandler(LocalDateHandler.Default);
            SqlMapper.AddTypeHandler(NullableLocalDateHandler.Default);

            SqlMapper.AddTypeHandler(LocalDateTimeHandler.Default);
            SqlMapper.AddTypeHandler(NullableLocalDateTimeHandler.Default);

            SqlMapper.AddTypeHandler(LocalTimeHandler.Default);
            SqlMapper.AddTypeHandler(NullableLocalTimeHandler.Default);

            SqlMapper.AddTypeHandler(OffsetDateTimeHandler.Default);
            SqlMapper.AddTypeHandler(NullableOffsetDateTimeHandler.Default);

            // TODO:   ZonedDateTime, DateTimeZone, Duration, Offset, Period
        }
    }
}
