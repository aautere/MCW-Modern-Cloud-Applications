using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.AzureADB2C.UI;

namespace Contoso.Apps.SportsLeaque.Web.Controllers
{
    public class AccountController : Controller
    {
        // Controllers\AccountController.cs

        public readonly string _editProfilePolicyId;

        public AccountController(IConfiguration configuration)
        {
            _editProfilePolicyId = configuration.GetValue<string>("AzureADB2C:EditProfilePolicyId");
        }

        public IActionResult SignIn()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Challenge(
                    new AuthenticationProperties() { RedirectUri = "/" },
                    AzureADB2CDefaults.AuthenticationScheme);
            }

            return RedirectToAction("Index", "Home");
        }

        public IActionResult SignUp()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Challenge(
                    new AuthenticationProperties() { RedirectUri = "/" },
                    AzureADB2CDefaults.AuthenticationScheme);
            }

            return RedirectToAction("Index", "Home");
        }


        public IActionResult Profile()
        {
            if (User.Identity.IsAuthenticated)
            {
                var properties = new AuthenticationProperties() { RedirectUri = "/" };
                properties.Items[AzureADB2CDefaults.PolicyKey] = _editProfilePolicyId;
                return Challenge(
                    properties,
                    AzureADB2CDefaults.AuthenticationScheme);
            }

            return RedirectToAction("Index", "Home");
        }

        public IActionResult SignOut()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            string redirectUri = Url.Action("Index", "Home", null, Request.Scheme);
            var properties = new AuthenticationProperties
            {
                RedirectUri = redirectUri
            };
            return SignOut(properties, AzureADB2CDefaults.CookieScheme, AzureADB2CDefaults.OpenIdScheme);
        }
    }
}