using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace EntropiaInventoryShareWeb.Components.Layout
{
    public partial class MainLayout
    {
        [Inject]
        AuthenticationStateProvider authStateProvider { get; set; }

        private ClaimsPrincipal? user;

        protected override async Task OnInitializedAsync()
        {
            var authState = await authStateProvider.GetAuthenticationStateAsync();
            user = authState.User;
        }
    }
}
