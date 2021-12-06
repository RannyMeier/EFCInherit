// See https://aka.ms/new-console-template for more information
using EFCInherit;
Console.WriteLine("Hello, World!");

using ItemContext context = new ItemContext();
{
    BomItem bomItem = new();
    context.BomItems.Add(bomItem);
    SLItem sLItem = new() { BomItem = bomItem, SerLot = "1234" };
    context.SLItems.Add(sLItem);


}
