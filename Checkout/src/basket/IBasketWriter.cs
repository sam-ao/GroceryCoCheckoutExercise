namespace GroceryCo.Checkout;

/*
    An object that outputs the contents and prices of the basket
*/
public interface IBasketWriter
{
    /*
        Outputs the contents of the basket
    */
    public void WriteBasket(Basket basket);
}