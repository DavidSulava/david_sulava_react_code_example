using DesignGear.Contractor.Core.Data.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DesignGear.Contractor.Core.Helpers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public string? Policy;

        //public AuthorizeAttribute(string policy)
        //{
        //    Policy = policy;
        //}

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = (User)context.HttpContext.Items["User"];
            var organizationId = context.HttpContext.Items["OrganizationId"];
            if (user == null || (organizationId == null && Policy == "OrganizationSelected"))
            {
                // not logged in
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }
    }
}
