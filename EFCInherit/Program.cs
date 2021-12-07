// See https://aka.ms/new-console-template for more information
using EFCInherit;
Console.WriteLine("Hello, World!");

using ItemContext context = new ItemContext();
{
    context.Database.EnsureCreated();
    BomItem bomItem = new() { Sku = "part1" };
    context.BomItems.Add(bomItem);
    SLItem sLItem = new() { BomItem = bomItem, SerLot = "1234" };
    context.SLItems.Add(sLItem);
    foreach (Item item in context.Items)
    {
        Console.WriteLine(item.Name);
    }

}
