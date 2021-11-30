namespace GroceryCo.Checkout;

public enum DiscountTypeEnum
{
    Flat,
    Percentage,
    Unknown
}

public static class DiscountTypes
{
    private const string FLAT_STRING = "Flat";
    private const string PERCENTAGE_STRING = "Percentage";

    /*
        Converts the string into the corresponding DiscountTypeEnum
    */
    public static DiscountTypeEnum GetDiscountType(string discountTypeString)
    {
        switch (discountTypeString)
        {
            case FLAT_STRING:
                return DiscountTypeEnum.Flat;
            case PERCENTAGE_STRING:
                return DiscountTypeEnum.Percentage;
            default:
                return DiscountTypeEnum.Unknown;
        }
    }
}