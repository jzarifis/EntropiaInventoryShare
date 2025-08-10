using EntropiaInventoryShareWeb.Dto;
using EntropiaInventoryShareWeb.Entities;
using EntropiaInventoryShareWeb.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;
using MudBlazor;
using System.Security.Claims;
using static MudBlazor.CategoryTypes;
using Avatar = EntropiaInventoryShareWeb.Entities.Avatar;

namespace EntropiaInventoryShareWeb.Components.Pages
{
    public partial class UpdateInventory
    {

        private List<InventoryItemDto> _items = new List<InventoryItemDto>();

        [Inject]
        private ILogger<UpdateInventory> logger { get; set; }

        [Inject]
        private MainBackgroundService backgroundService { get; set; }

        [Inject]
        private ParsingService parsingService { get; set; }

        [Inject]
        AuthenticationStateProvider authStateProvider { get; set; }

        private ClaimsPrincipal? user;

        private string avatar { get; set; }

        private DateTimeOffset? timestamp { get; set; }

        private string SessionTag { get; set; }

        MudTabs tabs;

        private string? licenseAvatar { get; set; } = string.Empty;


        protected override async Task OnInitializedAsync()
        {
            var authState = await authStateProvider.GetAuthenticationStateAsync();
            user = authState.User;
        }


        public async Task ParseData(string text)
        {
            _items = parsingService.ParseItems(text);
            var firstLine = text.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();
            if (firstLine?.StartsWith("Avatar:") == true)
            {
                avatar = firstLine.Replace("Avatar:", "").Trim();
            }
            if (_items.Count() > 0)
            {
                timestamp = DateTimeOffset.UtcNow;
                await backgroundService.HandleSharedItemsAsync(_items, avatar);
                if (_items.Exists(u => u.Shared))
                {
                    tabs.ActivatePanel(0);
                }
                else
                {
                    tabs.ActivatePanel(1);
                }

            }
            else
            {
                timestamp = null;
            }

        }


        private async Task SharedItemChanged(InventoryItemDto item)
        {
            await backgroundService.HandleItemSharedStateAsync(item, avatar);
        }

        private async Task SharedItemValueChanged(InventoryItemDto item)
        {
            await backgroundService.HandleItemSharedValueChangedAsync(item, avatar);
        }

        private string InventoryValid(string arg)
        {
            if (avatar != user.Identity.Name)
            {
                timestamp = null;
                _items.Clear();
                StateHasChanged();
                return "Pasted avatar doesn’t match your current account.";
            }
            if (_items.Count == 0)
            {
                timestamp = null;
                return "Cannot find any item";
            }


            return null;
        }
    }
}
