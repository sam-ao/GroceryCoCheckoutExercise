namespace GroceryCo.Checkout;

/*
    A type of IBasketItem that is the result of a promotion being applied
*/
public abstract class PromotionalBasketItem : BasketItem
{
    public PromotionalBasketItem(int id, string name, int quantity, double price, double regularPrice) 
    : base(id, name, quantity, price)
    {
        RegularPrice = regularPrice;
    }

    public double RegularPrice
    {
        get; set;
    }

    /*
        The string to be used as the regular price when outputting on the receipt
    */
    
    public string GetOutputRegularPrice()
    {        
        var totalPrice = RegularPrice * Quantity;
        if (totalPrice > double.MaxValue || totalPrice < double.MinValue)
        {
            throw new OverflowException();
        }
        return totalPrice.ToString("C", GroceryCoConstants.DEFAULT_CULTURE);
    }
}
