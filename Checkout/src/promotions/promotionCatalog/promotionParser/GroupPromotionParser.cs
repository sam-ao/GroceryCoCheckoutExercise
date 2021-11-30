namespace GroceryCo.Checkout;

/*
    An IPromotionParser that parses into SalePromotions.
*/
public class GroupPromotionParser : IPromotionParser
{
    private const int PROMOTION_ID_INDEX = 1;
    private const int SALE_ITEM_ID_INDEX = 2;
    private const int GROUP_SIZE_INDEX = 3;
    private const int DISCOUNTED_PRICE_ID_INDEX = 4;

    public PromotionTypesEnum PromotionType
    {
        get { return PromotionTypesEnum.GroupSale; }
    }

    public IPromotion ParsePromotion(string[] promotionStringArray)
    {
        GroupPromotion promotion;
        try 
        {
            var promotionId = Convert.ToInt32(promotionStringArray[PROMOTION_ID_INDEX]);
            var saleItemId = Convert.ToInt32(promotionStringArray[SALE_ITEM_ID_INDEX]);
            var groupSize = Convert.ToInt32(promotionStringArray[GROUP_SIZE_INDEX]);
            var discountedPrice = Convert.ToDouble(promotionStringArray[DISCOUNTED_PRICE_ID_INDEX]);

            promotion = new GroupPromotion(promotionId, saleItemId, groupSize, discountedPrice);
        }
        catch (Exception e) when (e is FormatException || e is OverflowException || e is KeyNotFoundException)
        {
            throw new PromotionParseException("Invalid group promotion entry format.", e);
        }

        return promotion;
    }
}