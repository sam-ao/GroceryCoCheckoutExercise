namespace GroceryCo.Checkout;

/*
    A catalog of all available promotions
*/
public class PromotionCatalog : IEnumerable<IPromotion>
{
    public PromotionCatalog()
    {
        promotions = new List<IPromotion>();
    }

    private List<IPromotion> promotions;

    public void AddPromotion(IPromotion promotion)
    {
        promotions.Add(promotion);
    }

    public IEnumerator<IPromotion> GetEnumerator() {
        return promotions.GetEnumerator();
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}