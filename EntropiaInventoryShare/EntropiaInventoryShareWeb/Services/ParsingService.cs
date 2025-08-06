using EntropiaInventoryShareWeb.Dto;
using System.Globalization;
using System.Text.RegularExpressions;

namespace EntropiaInventoryShareWeb.Services
{
    public class ParsingService
    {
        public List<InventoryItemDto> ParseItems(string text)
        {
            var items = new List<InventoryItemDto>();

            // Regular expression to match the rows of the table
            string pattern = @"(?<Name>[^\t\n\r]+)\t(?<Quantity>\d+)\t(?<Value>[\d.]+)\t(?<Container>[^\t\n\r]+)";
            Regex regex = new Regex(pattern, RegexOptions.Compiled);

            // Find the table headers
            string headersPattern = $"Name{'\t'}Quantity{'\t'}Value{'\t'}Container";
            int headersIndex = text.IndexOf(headersPattern);

            if (headersIndex < 0)
            {
                return items;
            }
            // Extract the table content starting from after the headers
            text = text.Substring(headersIndex + headersPattern.Length);
            string footerPattern = "Total items value";
            int footerIndex = text.IndexOf(footerPattern);
            if (footerIndex < 0)
            {
                return items;
            }

            var tableContent = text.Substring(0, footerIndex);
            // Match each row and extract data
            var lines = tableContent.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < lines.Length; i++)
            {
                var data = lines[i].Split(new[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);
                items.Add(new InventoryItemDto
                {
                    Name = data[0],
                    Quantity = int.Parse(data[1], CultureInfo.InvariantCulture),
                    Value = double.Parse(data[2], CultureInfo.InvariantCulture),
                    Container = data[3].Replace("▣", "")
                });
            }



            return items;
        }
    }
}
