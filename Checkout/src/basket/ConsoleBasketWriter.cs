namespace GroceryCo.Checkout;

/*
    IBasketWriter that prints the receipt to console
*/
public class ConsoleBasketWriter : IBasketWriter
{
    private IBasketOutputFormatter _formatter;
    
    public ConsoleBasketWriter(IBasketOutputFormatter formatter)
    {
        _formatter = formatter;
    }

    public void WriteBasket(Basket basket)
    {
        var formattedBasketOutput = _formatter.FormatBasket(basket);
        Console.WriteLine(formattedBasketOutput);
    }
}