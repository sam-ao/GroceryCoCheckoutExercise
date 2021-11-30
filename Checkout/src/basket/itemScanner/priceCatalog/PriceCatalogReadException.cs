namespace GroceryCo.Checkout;

/*
    An exception that occurs while reading the price catalog file
*/
public class PriceCatalogReadException : Exception
{
    public PriceCatalogReadException(string message, Exception inner) : base(message, inner)
    {
    }
}