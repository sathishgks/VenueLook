using Microsoft.AspNetCore.Authorization;
using SK.VenueBooking.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SK.VenueBooking.API.MiddleWare
{
    public class CustomAuthorizeUser : AuthorizationHandler<CustomAuthorizeUser>, IAuthorizationRequirement
    {

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CustomAuthorizeUser requirement)
        {
            var user = context?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
          
            if (user == null)
            {
                // not logged in
                context.Fail();
            }
            else
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}

