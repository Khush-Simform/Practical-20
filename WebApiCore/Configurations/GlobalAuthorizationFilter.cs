using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace WebApiCore.Controllers
{
    public class GlobalAuthorizationFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.ActionDescriptor.RouteValues["controller"] == "Customers")
            {
                if (!IsUserAuthenticated(context.HttpContext))
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }
            }
        }
        private bool IsUserAuthenticated(HttpContext httpContext)
        {
            return httpContext.Request.Headers.ContainsKey("Authorization");
        }
    }

}
