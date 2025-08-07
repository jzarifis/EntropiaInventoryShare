using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using EntropiaInventoryShareWeb.Dto.EntropiaNexus;
using static System.Net.WebRequestMethods;

namespace EntropiaInventoryShareWeb.Services
{
    public class EntropiaNexusService(ILogger<EntropiaNexusService> logger, HttpClient httpClient)
    {
        
        public async Task<GenericItem?> GetGenericItemInfo(string item)
        {
            return await httpClient.GetFromJsonAsync<GenericItem>($"https://api.entropianexus.com/items/{ProcessItemName(item)}");

        }

        public async Task<T?> GetItemInfo<T>(string item) where T: class
        {

            return await httpClient.GetFromJsonAsync<T>($"https://api.entropianexus.com/{typeof(T).GetType().BaseType.Name}s/{ProcessItemName(item)}");

        }


        private string ProcessItemName(string item)
        {
            return Uri.EscapeDataString(item.Replace(" (M,L)", " (L)").Replace(" (F,L)", " (L)").Replace(" (M)", "").Replace(" (F)", ""));
        }

    }
}
