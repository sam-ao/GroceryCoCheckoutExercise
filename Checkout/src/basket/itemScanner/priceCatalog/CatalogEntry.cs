namespace GroceryCo.Checkout;

/*
    Object representing a single row of the price catalog
*/
public readonly struct CatalogEntry
{
    public CatalogEntry(string name, double price, int id)
    {
        Name = name;
        Price = price;
        Id = id;
    }

    public string Name { get; init;}

    public double Price { get; init; }

    public int Id { get; init; }
}