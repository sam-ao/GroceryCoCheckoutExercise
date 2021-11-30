namespace GroceryCo.Checkout;

/*
    An exception that occurs when there was an error while reading the promotion catalog
*/
public class PromotionCatalogReadException : Exception
{
    public PromotionCatalogReadException(string message, Exception inner) : base(message, inner)
    {
    }
}