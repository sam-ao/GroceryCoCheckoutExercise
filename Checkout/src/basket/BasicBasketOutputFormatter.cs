namespace GroceryCo.Checkout;

/*
    The basic basket output formatter that outputs name and price for regular items, and 
    name, price and regular price for promotional items. Additionally the total price is printed at the end.
*/
public class BasicBasketOutputFormatter : IBasketOutputFormatter
{
    public BasicBasketOutputFormatter()
    {}

    public string FormatBasket(Basket basket)
    {
        var outputString = "";
        foreach (RegularBasketItem regularBasketItem in basket.RegularItems.Values)
        {
            outputString += String.Format("{0}   {1}\n", regularBasketItem.GetOutputName(), regularBasketItem.GetOutputPrice());
        }

        foreach (PromotionalBasketItem promotionalBasketItem in basket.PromotionalItems)
        {
            outputString += String.Format("{0}   {1} --> {2}\n", promotionalBasketItem.GetOutputName(), 
            promotionalBasketItem.GetOutputRegularPrice(), promotionalBasketItem.GetOutputPrice());
        }
        outputString += String.Format("TOTAL PRICE: {0}", basket.CalculateTotalCost().ToString("C", GroceryCoConstants.DEFAULT_CULTURE));
        return outputString;
    }
}