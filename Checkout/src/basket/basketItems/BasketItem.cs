namespace GroceryCo.Checkout;

/*
    Object representing an item in a basket
*/
public abstract class BasketItem
{
    public BasketItem(int id, string name, int quantity, double price)
    {
        Id = id;
        Name = name;
        Quantity = quantity;
        Price = price;
    }
        
    public int Id
    {
        get; set;
    }
    
    public string Name
    {
        get; set;
    }

    public int Quantity
    {
        get; set;
    }

    public double Price
    {
        get; set;
    }

    /*
        The string to be used as the name of this object when outputting the receipt
    */
    public abstract string GetOutputName();

    /*
        The string to be used as the price of this object when outputting the receipt
    */
    public string GetOutputPrice()
    {
        var totalPrice = Price * Quantity;
        if (totalPrice > double.MaxValue || totalPrice < double.MinValue)
        {
            throw new OverflowException();
        }
        return totalPrice.ToString("C", GroceryCoConstants.DEFAULT_CULTURE);
    }
}
