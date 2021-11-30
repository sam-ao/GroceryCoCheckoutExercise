namespace GroceryCo.Checkout;

/*
    An IPromotionParser that parses into BogoPromotions.
*/
public class BogoPromotionParser : IPromotionParser
{
    private const int PROMOTION_ID_INDEX = 1;
    private const int BOGO_ITEM_ID_INDEX = 2;
    private const int DISCOUNT_TYPE_ID_INDEX = 3;
    private const int DISCOUNT_VALUE_ID_INDEX = 4;

    public PromotionTypesEnum PromotionType
    {
        get { return PromotionTypesEnum.Bogo; }
    }

    public IPromotion ParsePromotion(string[] promotionStringArray)
    {
        BogoPromotion promotion;
        try 
        {
            var promotionId = Convert.ToInt32(promotionStringArray[PROMOTION_ID_INDEX]);
            var saleItemId = Convert.ToInt32(promotionStringArray[BOGO_ITEM_ID_INDEX]);
            var discountValue = Convert.ToDouble(promotionStringArray[DISCOUNT_VALUE_ID_INDEX]);

            var discountType = DiscountTypes.GetDiscountType(promotionStringArray[DISCOUNT_TYPE_ID_INDEX]);
            if (discountType == DiscountTypeEnum.Unknown)
            {
                throw new PromotionParseException("Unrecognized discount type.");
            }

            promotion = new BogoPromotion(promotionId, saleItemId, discountType, discountValue);
        }
        catch (Exception e) when (e is FormatException || e is OverflowException || e is KeyNotFoundException)
        {
            throw new PromotionParseException("Invalid bogo promotion entry format.", e);
        }

        return promotion;
    }
}