namespace GroceryCo.Checkout;

public enum PromotionTypesEnum
{
    Sale,
    Bogo,
    GroupSale,
    Unknown
}

public static class PromotionTypes
{
    private const string SALE_STRING = "Sale";
    private const string BOGO_STRING = "Bogo";
    private const string GROUP_SALE_STRING = "Group";
    
    /*
        Converts the string into the corresponding PromotionTypeEnum
    */
    public static PromotionTypesEnum GetPromotionType(string promotionTypeString)
    {
        switch (promotionTypeString)
        {
            case SALE_STRING:
                return PromotionTypesEnum.Sale;
            case BOGO_STRING:
                return PromotionTypesEnum.Bogo;
            case GROUP_SALE_STRING:
                return PromotionTypesEnum.GroupSale;
            default:
                return PromotionTypesEnum.Unknown;
        }
    }

}