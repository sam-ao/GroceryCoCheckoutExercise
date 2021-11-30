namespace GroceryCo.Checkout;

/*
    A IPromotionApplier that applies promotions in the order that they are found in the promotion catalog
*/
public class OrderedPromotionApplier : IPromotionApplier
{
    private PromotionCatalog _promotions;

    public OrderedPromotionApplier(PromotionCatalog promotionCatalog)
    {
        _promotions = promotionCatalog;
    }

    public void ApplyPromotions(ref Basket basket)
    {
        foreach (var promotion in _promotions)
        {
            promotion.Apply(ref basket);
        }
    }
}