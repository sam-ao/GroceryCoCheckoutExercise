namespace GroceryCo.Checkout;

/*
    An IPromotion representing a sale promotion
*/
public class SalePromotion : IPromotion
{
    private int _saleItemId;

    public DiscountTypeEnum DiscountType;

    public double DiscountValue;

    public SalePromotion(int promotionId, int saleItemId, DiscountTypeEnum discountType, double discountValue)
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
            var pricePerUnit = item.Price;
            checked
            {
                switch (DiscountType)
                {
                    case DiscountTypeEnum.Flat:
                        pricePerUnit -= DiscountValue; 
                        break;
                    case DiscountTypeEnum.Percentage:
                        pricePerUnit *= DiscountValue;
                        break;
                    default:
                        break;
                }
            }
            basket.AddPromotionalItem(
                new SalePromotionalBasketItem(item.Id, item.Name, item.Quantity, pricePerUnit, item.Price, this));
        }
    }
}