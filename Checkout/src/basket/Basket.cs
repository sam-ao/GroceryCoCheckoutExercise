namespace GroceryCo.Checkout;

/*
    The container of scanned items and promotional items during a checkout session
*/
public class Basket
{
    public Basket()
    {
        RegularItems = new Dictionary<int, RegularBasketItem>();
        PromotionalItems = new List<PromotionalBasketItem>();
    }

    public void AddRegularItem(RegularBasketItem item)
    {
        RegularItems.Add(item.Id, item);
    }
    public void AddPromotionalItem(PromotionalBasketItem item)
    {
        PromotionalItems.Add(item);
    }

    public double CalculateTotalCost()
    {
        double totalCost = 0;
        foreach (RegularBasketItem regularBasketItem in RegularItems.Values)
        {
            totalCost += checked(regularBasketItem.Price * regularBasketItem.Quantity);
        }

        foreach (PromotionalBasketItem promotionalBasketItem in PromotionalItems)
        {
            totalCost += checked(promotionalBasketItem.Price * promotionalBasketItem.Quantity);
        }

        return totalCost;
    }

    public Dictionary<int, RegularBasketItem> RegularItems { get; }

    public List<PromotionalBasketItem> PromotionalItems { get; }
}