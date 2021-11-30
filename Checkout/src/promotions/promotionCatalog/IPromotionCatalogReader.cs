namespace GroceryCo.Checkout;

/*
    An object that forms the promotion catalog
*/
public interface IPromotionCatalogReader
{
    /*
        Adds a parser for a type of promotion
    */
    public void AddPromotionParser(IPromotionParser promotion);

    /*
        Reads the promotional catalog
    */
    public PromotionCatalog ReadPromotionCatalog();
}