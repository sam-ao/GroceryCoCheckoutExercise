namespace GroceryCo.Checkout;

/*
    An object representing a promotion
*/
public interface IPromotion
{
    public int PromotionId
    {
        get; set;
    }
    
    /*
        Applies the promotion to the basket
    */
    public void Apply(ref Basket basket);
}