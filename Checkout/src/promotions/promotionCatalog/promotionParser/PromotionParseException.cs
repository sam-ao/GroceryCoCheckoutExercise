namespace GroceryCo.Checkout;

/*
    Exception that occurs when there was an error while parsing the promotion
*/
public class PromotionParseException : Exception
{
    public PromotionParseException(string message) : base(message)
    {
    }
    
    public PromotionParseException(string message, Exception inner) : base(message, inner)
    {
    }
}