using EntropiaInventoryShareWeb.Dto;
using EntropiaInventoryShareWeb.Entities;
using EntropiaInventoryShareWeb.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using MudBlazor;
using static MudBlazor.CategoryTypes;

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

        private string avatar { get; set; }

        private DateTimeOffset? timestamp { get; set; }

        private string SessionTag { get; set; }

        MudTabs tabs;

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
                tabs.ActivatePanel(1);
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

        private string InventoryValid(string arg)
        {
            if (_items.Count == 0)
            {
                timestamp = null;
                return "Cannot find any item";
            }

            return null;
        }
    }
}
