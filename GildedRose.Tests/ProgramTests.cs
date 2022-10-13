namespace GildedRose.Tests;

public class ProgramTests
{
    [Fact]
    public void NonLegendaryItem_EndOfDay_Quality_And_SellIn_Decrease()
    {
        var app = new Program(items: new List<Item>
        {
            new() { Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20 },
        });
        app.Items[0].SellIn.Should().Be(10);
        app.Items[0].Quality.Should().Be(20);
        
        app.UpdateQuality();
        
        app.Items[0].SellIn.Should().Be(9);
        app.Items[0].Quality.Should().Be(19);
    }
    
    [Fact]
    public void NonLegendaryItem_PastSellIn_Double_Quality_Decrease()
    {
        var app = new Program(items: new List<Item>
        {
            new() { Name = "+5 Dexterity Vest", SellIn = 0, Quality = 20 },
        });
        app.Items[0].SellIn.Should().Be(0);
        app.Items[0].Quality.Should().Be(20);
        
        app.UpdateQuality();
        
        app.Items[0].SellIn.Should().Be(-1);
        app.Items[0].Quality.Should().Be(18);
    }
    
    [Fact]
    public void NonLegendaryItem_Never_Negative_Quality()
    {
        var app = new Program(items: new List<Item>
        {
            new() { Name = "+5 Dexterity Vest", SellIn = 0, Quality = 5 },
        });
        app.Items[0].SellIn.Should().Be(0);
        app.Items[0].Quality.Should().Be(5);

        for (var i = 0; i < 20; i++)
        {
            app.UpdateQuality();
        }
        
        app.Items[0].SellIn.Should().Be(-20);
        app.Items[0].Quality.Should().Be(0);
    }
    
    [Fact]
    public void AgedBrie_EndOfDay_Quality_Increase()
    {
        var app = new Program(items: new List<Item>
        {
            new() { Name = "Aged Brie", SellIn = 10, Quality = 0 },
        });
        app.Items[0].SellIn.Should().Be(10);
        app.Items[0].Quality.Should().Be(0);

        for (var i = 0; i < 10; i++)
        {
            app.UpdateQuality();
        }
        
        app.Items[0].SellIn.Should().Be(0);
        app.Items[0].Quality.Should().Be(10);
    }
    
    [Fact]
    public void AgedBrie_EndOfDay_Double_Quality_Increase()
    {
        var app = new Program(items: new List<Item>
        {
            new() { Name = "Aged Brie", SellIn = 0, Quality = 0 },
        });
        app.Items[0].SellIn.Should().Be(0);
        app.Items[0].Quality.Should().Be(0);

        for (var i = 0; i < 20; i++)
        {
            app.UpdateQuality();
        }
        
        app.Items[0].SellIn.Should().Be(-20);
        app.Items[0].Quality.Should().Be(40);
    }
    
    [Fact]
    public void NonLegendaryItem_Quality_Never_Above_50()
    {
        var app = new Program(items: new List<Item>
        {
            new() { Name = "Aged Brie", SellIn = 0, Quality = 40 },
        });
        app.Items[0].SellIn.Should().Be(0);
        app.Items[0].Quality.Should().Be(40);

        for (var i = 0; i < 50; i++)
        {
            app.UpdateQuality();
        }
        
        app.Items[0].SellIn.Should().Be(-50);
        app.Items[0].Quality.Should().Be(50);
    }
    
    [Fact]
    public void LegendaryItem_EndOfDay_Unchanging_SellIn_And_Quality()
    {
        var app = new Program(items: new List<Item>
        {
            new() { Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80 },
            new() { Name = "Sulfuras, Hand of Ragnaros", SellIn = -5, Quality = 80 },
        });
        app.Items[0].SellIn.Should().Be(0);
        app.Items[0].Quality.Should().Be(80);
        
        app.Items[1].SellIn.Should().Be(-5);
        app.Items[1].Quality.Should().Be(80);

        for (var i = 0; i < 10; i++)
        {
            app.UpdateQuality();
        }
        
        app.Items[0].SellIn.Should().Be(0);
        app.Items[0].Quality.Should().Be(80);
        
        app.Items[1].SellIn.Should().Be(-5);
        app.Items[1].Quality.Should().Be(80);
    }
    
    [Fact]
    public void BackStagePasses_EndOfDay_Quality_Increase()
    {
        var app = new Program(items: new List<Item>
        {
            new() { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 11, Quality = 10 },
        });
        app.Items[0].SellIn.Should().Be(11);
        app.Items[0].Quality.Should().Be(10);
        
        app.UpdateQuality();

        app.Items[0].SellIn.Should().Be(10);
        app.Items[0].Quality.Should().Be(11);
        
        app.UpdateQuality();
        
        app.Items[0].SellIn.Should().Be(9);
        app.Items[0].Quality.Should().Be(13);
    }
    
    [Fact]
    public void BackStagePasses_EndOfDay_Quality_Zero_Exceeding_SellIn()
    {
        var app = new Program(items: new List<Item>
        {
            new() { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 4, Quality = 20 },
        });
        app.Items[0].SellIn.Should().Be(4);
        app.Items[0].Quality.Should().Be(20);
        
        app.UpdateQuality();

        app.Items[0].SellIn.Should().Be(3);
        app.Items[0].Quality.Should().Be(23);
        
        for (var i = 0; i < 4; i++)
        {
            app.UpdateQuality();
        }
        
        app.Items[0].SellIn.Should().Be(-1);
        app.Items[0].Quality.Should().Be(0);
    }
    
    [Fact]
    public void ConjuredItems_EndOfDay_Double_Quality_Decrease()
    {
        var app = new Program(items: new List<Item>
        {
            new() { Name = "Conjured Mana Cake", SellIn = 3, Quality = 6 },
        });
        app.Items[0].SellIn.Should().Be(3);
        app.Items[0].Quality.Should().Be(6);
        
        app.UpdateQuality();

        app.Items[0].SellIn.Should().Be(2);
        app.Items[0].Quality.Should().Be(4);
    }
    
    [Fact]
    public void ConjuredItems_EndOfDay_PastSellIn_Double_Quality_Decrease()
    {
        var app = new Program(items: new List<Item>
        {
            new() { Name = "Conjured Mana Cake", SellIn = 0, Quality = 6 },
        });
        app.Items[0].SellIn.Should().Be(0);
        app.Items[0].Quality.Should().Be(6);
        
        app.UpdateQuality();

        app.Items[0].SellIn.Should().Be(-1);
        app.Items[0].Quality.Should().Be(2);
    }
}