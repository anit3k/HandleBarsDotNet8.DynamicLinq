using HandlebarsDotNet;
using HandlebarsDotNet.Helpers;

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

class Program
{
    static void Main(string[] args)
    {
        // Sample data
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
        HandlebarsHelpers.Register(handlebars);

        Console.WriteLine("HandleBars Block Helpers: " + handlebars.Configuration.BlockHelpers.Count); // 134
        Console.WriteLine("HandleBars Helpers: " + handlebars.Configuration.Helpers.Count); // 265

        // Template with Dynamic Linq
        string template = "{\"ItemName\":\"{{ Name }}\",\"ItemDescription\":\"{{ Description }}\",\"Locations\": [{{#each (DynamicLinq.Where Locations \"Warehouse != null && Warehouse != String.Empty && Quantity > 0\")}}{\"Location\":\"{{ Warehouse }}\",\"Quantity\":\"{{ Quantity }}\" }{{#unless @last}},{{/unless}}{{/each}}]}";
        var compiledTemplate = handlebars.Compile(template);
        var result = compiledTemplate(item);
        Console.WriteLine("OUTPUT: " + result);

        Console.WriteLine("\nPress any key to exit");
        Console.ReadKey();
    }
}
