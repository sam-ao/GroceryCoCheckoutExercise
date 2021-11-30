namespace GroceryCo.Checkout;

/*
    An object that parses a IPromotion
*/
public interface IPromotionParser
{
    /*
        The type that the promotion parser parses into
    */
    public PromotionTypesEnum PromotionType
    {
        get;
    }

    /*
        Parse the array of strings into an IPromotion
    */
    public IPromotion ParsePromotion(string[] promotionStringArray);
}