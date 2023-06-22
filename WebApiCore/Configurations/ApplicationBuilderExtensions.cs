namespace WebApiCore.Configurations
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder AppGlobalErrorHandler(this IApplicationBuilder applicationBuilder)  
            => applicationBuilder.UseMiddleware<GlobalExceptionHandlingMiddleware>();

        
    }
}
    