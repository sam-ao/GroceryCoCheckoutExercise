namespace GroceryCo.Checkout;

/*
    An IPromotion representing a group promotion
*/
public class GroupPromotion : IPromotion
{
    private int _saleItemId;

    public GroupPromotion(int promotionId, int saleItemId, int groupSize, double discountedPrice)
    {
        PromotionId = promotionId;
        _saleItemId = saleItemId;
        GroupSize = groupSize;
        DiscountedPrice = discountedPrice;
    }

    public int PromotionId
    {
        get; set;
    }

    public int GroupSize
    {
        get; set;
    }

    public double DiscountedPrice
    {
        get; set;
    }

    public void Apply(ref Basket basket)
    {
        RegularBasketItem? item;
        if (basket.RegularItems.Remove(_saleItemId, out item))
        {
            var remainder = item.Quantity % GroupSize;
            if (item.Quantity >= GroupSize)
            {
                checked
                {
                    basket.AddPromotionalItem(
                        new GroupPromotionalBasketItem(item.Id, item.Name, item.Quantity / GroupSize, 
                            DiscountedPrice, item.Price * GroupSize, this));
                }
           }
            if (remainder > 0)
            {
                item.Quantity = remainder;
                basket.AddRegularItem(item);
            }
        }
    }
}