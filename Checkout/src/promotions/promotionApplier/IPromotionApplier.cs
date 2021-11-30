namespace GroceryCo.Checkout;

/*
    An object that applies promotions to a basket
*/
public interface IPromotionApplier
{
    /*
        Apply the promotions to the provided basket
    */
    public void ApplyPromotions(ref Basket basket);
}