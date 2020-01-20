using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GameStore.Web.Attributes.Authorization
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class StoreAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly AuthorizePermission _permission;
        private readonly List<string> _roleList;

        public StoreAuthorizeAttribute(AuthorizePermission permission, params string[] roles)
        {
            _permission = permission;
            _roleList = roles.ToList();
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;
            var isUserInRole = false;

            // Checking if user in role and Authorize permission
            foreach (var role in _roleList)
            {
                if (user.Identity.IsAuthenticated)
                {
                    if (user.IsInRole(role) && _permission == AuthorizePermission.Allow)
                    {
                        isUserInRole = true;
                    }

                    if (!user.IsInRole(role) && _permission == AuthorizePermission.Disallow)
                    {
                        isUserInRole = true;
                    }
                }
            }

            // Check if User is not authenticated and permission
            if (_roleList.Contains("Guest") && _permission == AuthorizePermission.Allow && !user.Identity.IsAuthenticated)
            {
                isUserInRole = true;
            }
            else if (_permission == AuthorizePermission.Disallow && !user.Identity.IsAuthenticated)
            {
                isUserInRole = true;
            }

            // Allow or disallow access source in order to below algorithm
            if (isUserInRole)
            {
                return;
            }

            var returnUrl = context.HttpContext.Request.Headers["Referer"].ToString();
            var viewResult = new RedirectToActionResult("AccessDenied", "Account", new { returnUrl = returnUrl });

            context.Result = viewResult;
        }
    }
}