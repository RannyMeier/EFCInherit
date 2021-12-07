// See https://aka.ms/new-console-template for more information
using EFCInherit;
Console.WriteLine("Hello, World!");

using ItemContext context = new ItemContext();
{
    await context.Database.EnsureDeletedAsync();
    await context.Database.EnsureCreatedAsync();
    BomItem bomItem = new() { Name = "Part", Sku = "part1" };
    context.BomItems.Add(bomItem);
    SLItem sLItem = new() { BomItem = bomItem, Name = "SerItem", SerLot = "1234" };
    context.Items.Add(sLItem); //apparently we can add to either Items or SLItems
    Console.WriteLine(String.Format("Objects affected {0}", context.SaveChanges()));
    foreach (Item item in context.Items)
    {
        Console.WriteLine(item.Name);
    }
    Console.WriteLine(String.Format("Items.OfType<SLItem>().Count() = {0}, SLItem count = {1}", 
        context.Items.OfType<SLItem>().Count(), context.SLItems.Count()));
    Console.WriteLine("Bom to SLI = {0} - {1}, SLI to BOM = {2} - {3}",
        context.BomItems.FirstOrDefault().Name,
        context.BomItems.FirstOrDefault().SLItems.FirstOrDefault().Name,
        context.SLItems.FirstOrDefault().Name,
        context.SLItems.FirstOrDefault().BomItem.Name);
}
