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
    context.SLItems.Add(sLItem);
    Console.WriteLine(context.SaveChanges().ToString());
    foreach (Item item in context.Items)
    {
        Console.WriteLine(item.Name);
    }

}
