// See https://aka.ms/new-console-template for more information
using EFCInherit;
Console.WriteLine("Hello, World!");

using ItemContext context = new ItemContext();
{
    await context.Database.EnsureDeletedAsync();
    await context.Database.EnsureCreatedAsync();
    BomItem bomItem = new() { Name = "Part", Sku = "part1" };
    context.BomItems.Add(bomItem);
    SLItem sLItem = new() { BomItem = bomItem, Name = "SItem", SerLot = "1234" };
    context.Items.Add(sLItem); //apparently we can add to either Items or SLItems
    Console.WriteLine(context.SaveChanges().ToString());
    foreach (Item item in context.Items)
    {
        Console.WriteLine(item.Name);
    }
    Console.WriteLine(String.Format("Items.OfType<SLItem>().Count() = {0}, SLItem count = {1}", 
        context.Items.OfType<SLItem>().Count(), context.SLItems.Count()));
    //Having the SLItem set exposed may either be a feature of code first or EF Core ?
}
