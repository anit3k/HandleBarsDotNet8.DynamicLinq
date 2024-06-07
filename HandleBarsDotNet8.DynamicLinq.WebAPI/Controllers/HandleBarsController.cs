using HandlebarsDotNet.Helpers;
using HandlebarsDotNet;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Diagnostics;

namespace HandleBarsDotNet8.DynamicLinq.WebAPI.Controllers
{
    public class Item
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Location> Locations { get; set; }
    }

    public class Location
    {
        public string Warehouse { get; set; }
        public double Quantity { get; set; }
    }

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HandleBarsController : ControllerBase
    {
        [HttpGet]
        public IActionResult Test()
        {
            var locations = new List<Location>
            {
                new Location { Warehouse = "A", Quantity = 10 },
                new Location { Warehouse = "B", Quantity = 0 },
                new Location { Warehouse = null, Quantity = 5 },
                new Location { Warehouse = "", Quantity = 145 },
                new Location { Warehouse = "E", Quantity = -145 },
                new Location { Warehouse = "F", Quantity = 20 }
            };

            var item = new Item
            {
                Name = "Some Product",
                Description = "This item is a product",
                Locations = locations
            };

            // Register Handlebars.Net.Helpers
            var handlebars = Handlebars.Create();
            handlebars.Configuration.NoEscape = true;
            // Original helper registration
            //HandlebarsHelpers.Register(handlebars);

            // Workaround provide by https://github.com/StefH
            // Currently works in debug mode
            HandlebarsHelpers.Register(handlebars, c => c.CustomHelperPaths = new BindingList<string>
                {
                    Path.GetDirectoryName(Process.GetCurrentProcess().MainModule!.FileName)!
                });

            var result = string.Empty;

            result += "HandleBars Block Helpers: " + handlebars.Configuration.BlockHelpers.Count + "/n"; // 97
            result += "HandleBars Helpers: " + handlebars.Configuration.Helpers.Count + "/n"; // 193

            // Template with Dynamic Linq
            try
            {
                string template = "{\"ItemName\":\"{{ Name }}\",\"ItemDescription\":\"{{ Description }}\",\"Locations\": [{{#each (DynamicLinq.Where Locations \"Warehouse != null && Warehouse != String.Empty && Quantity > 0\")}}{\"Location\":\"{{ Warehouse }}\",\"Quantity\":\"{{ Quantity }}\" }{{#unless @last}},{{/unless}}{{/each}}]}";
                var compiledTemplate = handlebars.Compile(template);
                result += compiledTemplate(item);
                Console.WriteLine("OUTPUT: " + result);
            }
            catch (Exception ex)
            {
                return BadRequest(result + ex.Message);
                throw;
            }

            return Ok(result);
        }
    }
}
