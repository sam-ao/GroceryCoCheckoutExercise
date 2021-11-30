namespace GroceryCo.Checkout;

/*
    Object representing a price catalog
*/
public class PriceCatalog
{
    public PriceCatalog()
    {
        _catalogEntries = new Dictionary<string, CatalogEntry>();
    }

    private Dictionary<string, CatalogEntry> _catalogEntries;

    /*
        Add a row to the price catalog
    */
    public void AddEntry(CatalogEntry entry)
    {
        _catalogEntries.Add(entry.Name, entry);
    }

    /*
        Get the CatalogEntry for the item with the specified name
    */
    public CatalogEntry GetCatalogEntry(string name)
    {
        return _catalogEntries[name];
    }

    /*
        Checks if the price catalog contains a CatalogEntry for the item with the specified name
    */
    public bool ContainsItem(string name)
    {
        return _catalogEntries.ContainsKey(name);
    }
}