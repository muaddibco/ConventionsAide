namespace ConventionsAideGW.Middlewares
{
    public class CorrelationIdInjector
    {
        public const string CORRELATIONID = "CorrelationId";
        private readonly RequestDelegate _next;

        public CorrelationIdInjector(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            context.Request.Headers.Add(CORRELATIONID, Guid.NewGuid().ToString());
            await _next(context);
        }
    }
}
