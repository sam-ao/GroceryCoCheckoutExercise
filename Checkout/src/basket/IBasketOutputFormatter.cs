namespace GroceryCo.Checkout;

/*
    An object that formats the contents of a basket into a string
*/
public interface IBasketOutputFormatter
{
    /*
        Formats the contents of the basket into a string
    */
    public string FormatBasket(Basket basket);
}