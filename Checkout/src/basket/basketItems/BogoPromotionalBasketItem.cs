namespace GroceryCo.Checkout;

/*
    An IBasketItem that was created as the result of BOGO promotion
*/
public class BogoPromotionalBasketItem : PromotionalBasketItem
{
    public BogoPromotion Promotion
    {
        get; set;
    }

    public BogoPromotionalBasketItem(int saleItemId, string name, int quantity,
    double price, double regularPrice, BogoPromotion promotion)
    : base(saleItemId, name, quantity, price, regularPrice)
    {
        Id = saleItemId;
        Quantity = quantity;
        Name = name;
        Price  = price;
        RegularPrice = regularPrice;
        Promotion = promotion;
    }

    public override string GetOutputName()
    {
        string discountString = "";

        if (Promotion.DiscountType == DiscountTypeEnum.Flat)
        {
            discountString = String.Format("{0} OFF", 
            Promotion.DiscountValue.ToString("C",GroceryCoConstants.DEFAULT_CULTURE));
        }
        else if (Promotion.DiscountType == DiscountTypeEnum.Percentage)
        {
            if (Promotion.DiscountValue == 0.0)
            {
                discountString = "FREE";
            }
            else
            {
                discountString = String.Format("{0} OFF", 
                (1 - Promotion.DiscountValue).ToString("P", System.Globalization.CultureInfo.InvariantCulture));
            }
        }
        return String.Format("{0} - BUY ONE GET ONE {1}", Name, discountString);
    }
}
