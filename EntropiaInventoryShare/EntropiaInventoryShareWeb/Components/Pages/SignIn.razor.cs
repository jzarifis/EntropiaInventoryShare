using EntropiaInventoryShareWeb.Entities;
using EntropiaInventoryShareWeb.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.JSInterop;
using System.Security.Claims;

namespace EntropiaInventoryShareWeb.Components.Pages
{
    public partial class SignIn
    {
        private string avatar { get; set; }
        private string license { get; set; }
        private string errorAvatar { get; set; } = string.Empty;
        private string errorLicense { get; set; } = string.Empty;

        private bool locked = false;

        [Inject]
        MainBackgroundService mainBackgroundService { get; set; }

        [Inject]
        IJSRuntime JS { get; set; }

        [Inject]
        NavigationManager navigationManager { get; set; }

        [Inject] 
        IHttpContextAccessor httpContextAccessor { get; set; }


        [Inject]
        IDataProtectionProvider dataProtectionProvider { get; set; } = null!;

        public async Task SignInAvatar()
        {
            if (!string.IsNullOrEmpty(avatar))
            {
                try
                {
                    var avatarModel = await mainBackgroundService.SigninAvatar(avatar);
                    license = avatarModel.License.ToString();
                    locked = true;
                }
                catch (Exception ex)
                {
                    errorAvatar = ex.Message;
                }
            }
            else
            {
                errorAvatar = "Required";
            }
        }

        private async Task CopyKey()
        {
            await JS.InvokeVoidAsync("navigator.clipboard.writeText", license);
        }

        public async Task SignInLicense()
        {
            if (!string.IsNullOrEmpty(license))
            {
                try
                {
                    var avatarModel = await mainBackgroundService.SigninLicense(license);
                    await SignInAvatar(avatarModel);
                }
                catch (Exception ex)
                {
                    errorLicense = ex.Message;
                }
            }
            else
            {
                errorLicense = "Required";
            }
        }

        private async Task SignInAvatar(Avatar avatar)
        {

            var data = avatar.License.ToString();

            var parsedQuery =
                System.Web.HttpUtility.ParseQueryString(
                    new Uri(navigationManager.Uri).Query);
            var protector =
                dataProtectionProvider.CreateProtector("SignIn");

            var pdata = protector.Protect(data);


            navigationManager.NavigateTo($"/login?t={pdata}", forceLoad: true);
        }
    }

}
