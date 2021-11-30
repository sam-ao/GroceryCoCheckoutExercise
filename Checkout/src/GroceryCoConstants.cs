namespace GroceryCo.Checkout;

/*
    This class will be used as place to hold constants that are used throughout the project.
    This will also be used instead of config file. With more time, one of things on my wishlist would be to use a config file instead of this constants file.
*/
public static class GroceryCoConstants
{
    public static readonly System.Globalization.CultureInfo DEFAULT_CULTURE = new System.Globalization.CultureInfo("en-US");
    public const string PRICE_CATALOG_FILE_PATH = "resources/priceCatalog.csv";
    public const string PROMOTION_CATALOG_FILE_PATH = "resources/promotionCatalog.csv";
}
