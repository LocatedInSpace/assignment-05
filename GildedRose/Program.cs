namespace GildedRose;

public class Program
{
    public IList<Item> Items;

    public Program(IList<Item> items)
    {
        Items = items;
    }
    
    private static void Main(string[] args)
    {
        Console.WriteLine("OMGHAI!");

        var app = new Program(items: new List<Item>
        {
            new() { Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20 },
            new() { Name = "Aged Brie", SellIn = 2, Quality = 0 },
            new() { Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7 },
            new() { Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80 },
            new() { Name = "Sulfuras, Hand of Ragnaros", SellIn = -1, Quality = 80 },
            new()
            {
                Name = "Backstage passes to a TAFKAL80ETC concert",
                SellIn = 15,
                Quality = 20
            },
            new()
            {
                Name = "Backstage passes to a TAFKAL80ETC concert",
                SellIn = 10,
                Quality = 49
            },
            new()
            {
                Name = "Backstage passes to a TAFKAL80ETC concert",
                SellIn = 5,
                Quality = 49
            },
            new() { Name = "Conjured Mana Cake", SellIn = 3, Quality = 6 }
        });

        for (var i = 0; i < 31; i++)
        {
            Console.WriteLine("-------- day " + i + " --------");
            Console.WriteLine("name, sellIn, quality");
            for (var j = 0; j < app.Items.Count; j++)
                Console.WriteLine(app.Items[j].Name + ", " + app.Items[j].SellIn + ", " + app.Items[j].Quality);
            Console.WriteLine("");
            app.UpdateQuality();
        }
    }

    private bool DecreasesInQuality(string s)
    {
        var nonDecreasingItems = new[] { "Aged Brie", "Backstage passes to a TAFKAL80ETC concert" };
        // notice negated
        return !nonDecreasingItems.Any(i => i.Equals(s));
    }

    private bool IsLegendary(string s)
    {
        return s == "Sulfuras, Hand of Ragnaros";
    }
    
    private int QualityIncrease(Item item)
    {
        if (item.Name == "Backstage passes to a TAFKAL80ETC concert")
        {
            switch (item.SellIn)
            {
                case < 6:
                    return 3;
                case < 11:
                    return 2;
            }
        }

        return 1;
    }
    
    private int PastSellInQuality(Item item)
    {
        if (item.Name == "Backstage passes to a TAFKAL80ETC concert")
        {
            return 0;
        }
        if (item.Name == "Aged Brie")
        {
            return item.Quality + 1;
        }

        if (item.Name.StartsWith("Conjured"))
        {
            return item.Quality - 2;
        }

        return item.Quality - 1;
    }
    
    public void UpdateQuality()
    {
        foreach (var item in Items)
        {
            if (IsLegendary(item.Name))
                continue;
            
            if (DecreasesInQuality(item.Name))
            {
                item.Quality -= item.Name.StartsWith("Conjured") ? 2 : 1;
            }
            else if (item.Quality < 50)
            {
                item.Quality += QualityIncrease(item);
            }

            item.SellIn -= 1;

            if (item.SellIn < 0)
            {
                item.Quality = PastSellInQuality(item);
            }

            item.Quality = item.Quality < 0 ? 0 : item.Quality;
            item.Quality = item.Quality > 50 ? 50 : item.Quality;
        }
    }

}

public class Item
{
    public string Name { get; set; }

    public int SellIn { get; set; }

    public int Quality { get; set; }
}