namespace GroceryCo.Checkout;

/*
    A regular price IBasketItem
*/
public class RegularBasketItem : BasketItem
{
    public RegularBasketItem(int id, string name, int quantity, double price) : 
    base(id, name, quantity, price)
    { 
    }

    public RegularBasketItem(int id, string name, double price) : this(id, name, 1, price)
    {
    }
    
    /*
        Increment the quantity of the item by one.
    */
    public void IncrementQuantity()
    {
        Quantity = checked(Quantity + 1);
    }

    public override string GetOutputName()
    {
        return Name;
    }
}
