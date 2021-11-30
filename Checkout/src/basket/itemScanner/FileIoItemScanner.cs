namespace GroceryCo.Checkout;
using System.IO.Abstractions;

/*
    An IItemScanner that reads the grocery list from a file
*/
public class FileIoItemScanner : IItemScanner
{
    public FileIoItemScanner(string filepath, PriceCatalog priceCatalog, IFileSystem fileSystem)
    {
        _filePath = filepath;
        _priceCatalog = priceCatalog;
        _fileSystem = fileSystem;
    }
    
    private string _filePath;
    private PriceCatalog _priceCatalog;
    private IFileSystem _fileSystem;

    public void ScanItems(ref Basket basket)
    {
        try
        {
            using (var streamReader = _fileSystem.File.OpenText(_filePath))
            {
                while(streamReader.Peek() > 0)
                {
                    var item = streamReader.ReadLine();

                    #pragma warning disable 8604
                    if (!_priceCatalog.ContainsItem(item))
                    {
                        throw new ItemScanningException("Item not found in catalog.");
                    }
                    #pragma warning restore 8604

                    var catalogItem = _priceCatalog.GetCatalogEntry(item);

                    if (basket.RegularItems.ContainsKey(catalogItem.Id))
                    {
                        var basketItem = basket.RegularItems[catalogItem.Id];
                        basketItem.IncrementQuantity();
                    } 
                    else
                    {
                        basket.AddRegularItem(new RegularBasketItem(catalogItem.Id, catalogItem.Name, catalogItem.Price));
                    }
                } 
            }
        }
        catch (OverflowException e)
        {
            throw new ItemScanningException("Exception while adding item to basket.", e);
        }
        catch (Exception e) when (e is IOException || e is OutOfMemoryException)
        {
            throw new ItemScanningException("Exception while reading file.", e);
        }
        catch (Exception e) when (e is ArgumentException || e is ArgumentNullException || e is FileNotFoundException 
        || e is DirectoryNotFoundException || e is PathTooLongException || e is UnauthorizedAccessException)
        {
            throw new ItemScanningException("Exception while opening file.", e);
        }
    }
}
