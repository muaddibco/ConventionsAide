namespace ConventionsAideGW.Middlewares
{
    public static class CorrelationIdInjectorExtensions
    {
        public static IApplicationBuilder UseCorrelationIdInjector(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CorrelationIdInjector>();
        }
    }
}
