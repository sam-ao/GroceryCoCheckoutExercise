namespace GroceryCo.Checkout;

/*
    An IBasketItem that was created as the result of sale promotion
*/
public class SalePromotionalBasketItem : PromotionalBasketItem
{
    public SalePromotion Promotion
    {
        get; set;
    }

    public SalePromotionalBasketItem(int saleItemId, string name, int quantity,
    double price, double regularPrice, SalePromotion promotion) 
    : base(saleItemId, name, quantity, price, regularPrice)
    {
        Promotion = promotion;
    }

    public override string GetOutputName()
    {
        string discountString = "";

        if (Promotion.DiscountType == DiscountTypeEnum.Flat)
        {
            discountString = Promotion.DiscountValue.ToString("C",GroceryCoConstants.DEFAULT_CULTURE);
        }
        else if (Promotion.DiscountType == DiscountTypeEnum.Percentage)
        {
            discountString = (1 - Promotion.DiscountValue).ToString("P", System.Globalization.CultureInfo.InvariantCulture);
        }
        return String.Format("{0} - {1} OFF", Name, discountString);
    }
}
