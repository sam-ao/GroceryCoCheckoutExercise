namespace GroceryCo.Checkout;
using System.IO.Abstractions;

class GroceryCoCheckout
{
    static int Main(string[] args)
    {
        if (args.Length == 0)
        {
            System.Console.WriteLine("Please provide the to a file with a list of items to scan.");
            return 1;
        }
        var groceryListFilePath = args[0];
        IFileSystem fileSystem = new FileSystem();
        IPriceCatalogReader priceCatalogReader = new FileIoPriceCatalogReader(GroceryCoConstants.PRICE_CATALOG_FILE_PATH, fileSystem);
        IPromotionCatalogReader promotionCatalogReader = new FileIoPromotionCatalogReader(GroceryCoConstants.PROMOTION_CATALOG_FILE_PATH, fileSystem);
        promotionCatalogReader.AddPromotionParser(new SalePromotionParser());
        promotionCatalogReader.AddPromotionParser(new BogoPromotionParser());
        promotionCatalogReader.AddPromotionParser(new GroupPromotionParser());

        PriceCatalog priceCatalog;
        PromotionCatalog promotionCatalog;
        try
        {
            priceCatalog = priceCatalogReader.ReadPriceCatalog();
            promotionCatalog = promotionCatalogReader.ReadPromotionCatalog();
            IItemScanner itemScanner = new FileIoItemScanner(groceryListFilePath, priceCatalog, fileSystem);
            IPromotionApplier promotionApplier = new OrderedPromotionApplier(promotionCatalog);
        
            IBasketOutputFormatter basketOutputFormatter = new BasicBasketOutputFormatter();
            IBasketWriter basketWriter = new ConsoleBasketWriter(basketOutputFormatter);
            Basket basket = new Basket();

            itemScanner.ScanItems(ref basket);
            promotionApplier.ApplyPromotions(ref basket);
            basketWriter.WriteBasket(basket);
        }
        catch (PriceCatalogReadException e)
        {
            System.Console.WriteLine("Error occurred while reading price catalog. {0}", e);
            return 1;
        }
        catch (PromotionCatalogReadException e)
        {
            System.Console.WriteLine("Error occurred while reading promotion catalog file. {0}", e);
            return 1;
        }
        catch (PromotionParseException e)
        {
            System.Console.WriteLine("Error occurred while parsing promotion catalog. {0}", e);
            return 1;
        }
        catch (ItemScanningException e)
        {
            System.Console.WriteLine("Error occurred while scanning grocery list. {0}", e);
            return 1;
        }

        return 0;
    }
}