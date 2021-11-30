namespace GroceryCo.Checkout;

/*
    An IPromotion representing a sale promotion
*/
public class BogoPromotion : IPromotion
{
    private int _saleItemId;

    public DiscountTypeEnum DiscountType;

    public double DiscountValue;

    public BogoPromotion(int promotionId, int saleItemId, DiscountTypeEnum discountType, double discountValue)
    {
        PromotionId = promotionId;
        _saleItemId = saleItemId;
        DiscountType = discountType;
        DiscountValue = discountValue;
    }

    public int PromotionId
    {
        get; set;
    }

    public void Apply(ref Basket basket)
    {
        RegularBasketItem? item;
        if (basket.RegularItems.Remove(_saleItemId, out item))
        {
            var remainder = item.Quantity % 2;
            if (item.Quantity > 1)
            {
                var pricePerUnit = item.Price;
                switch (DiscountType)
                {
                    case DiscountTypeEnum.Flat:
                        pricePerUnit = checked((item.Price * 2) - DiscountValue); 
                        break;
                    case DiscountTypeEnum.Percentage:
                        pricePerUnit = checked((pricePerUnit * DiscountValue) + pricePerUnit);
                        break;
                    default:
                        break;
                }
                checked
                {
                    basket.AddPromotionalItem(
                        new BogoPromotionalBasketItem(item.Id, item.Name, item.Quantity / 2, pricePerUnit, 
                            item.Price * 2, this));
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