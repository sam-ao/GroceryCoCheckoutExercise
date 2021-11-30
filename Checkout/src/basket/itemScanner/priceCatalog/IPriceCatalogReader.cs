namespace GroceryCo.Checkout;

/*
    An object used to read the price catalog
*/
public interface IPriceCatalogReader
{
    /*
        Gets the price catalog

        @throws PriceCatalogReadException: An exception occurred while reading the price catalog
    */
    public PriceCatalog ReadPriceCatalog();
}