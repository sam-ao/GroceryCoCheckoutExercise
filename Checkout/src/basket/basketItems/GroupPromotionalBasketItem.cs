namespace GroceryCo.Checkout;

/*
    An IBasketItem that was created as the result of group promotion
*/
public class GroupPromotionalBasketItem : PromotionalBasketItem
{
    public GroupPromotion Promotion
    {
        get; set;
    }

    public GroupPromotionalBasketItem(int saleItemId, string name, int quantity, 
    double price, double regularPrice, GroupPromotion promotion)
    : base(saleItemId, name, quantity, price, regularPrice)
    {
        Promotion = promotion;
    }

    public override string GetOutputName()
    {
        string discountPriceString = Promotion.DiscountedPrice.ToString("C",GroceryCoConstants.DEFAULT_CULTURE);

        return String.Format("{0} - BUY {1} FOR {2}", Name, Promotion.GroupSize, discountPriceString);
    }
}
