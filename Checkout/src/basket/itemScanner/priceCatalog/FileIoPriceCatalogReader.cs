namespace GroceryCo.Checkout;

using System.IO;
using System.IO.Abstractions;

/*
    IPriceCatalogReader that reads the price catalog from a file
*/
public class FileIoPriceCatalogReader : IPriceCatalogReader
{
    private const int NAME_INDEX = 0;
    private const int PRICE_INDEX = 1;
    private const int ID_INDEX = 2;
    private const char CATALOG_FIELD_SEPARATOR = ',';

    public FileIoPriceCatalogReader(string filePath, IFileSystem fileSystem)
    {
        _filePath = filePath;
        _fileSystem = fileSystem;
    }

    private string _filePath;
    private IFileSystem _fileSystem;

    public PriceCatalog ReadPriceCatalog()
    {
        var priceCatalog = new PriceCatalog();
        try
        {
            using (var streamReader = _fileSystem.File.OpenText(_filePath))
            {
                while(!streamReader.EndOfStream)
                {
                    #pragma warning disable 8602
                    var catalogFields = streamReader.ReadLine().Split(CATALOG_FIELD_SEPARATOR);
                    #pragma warning restore 8602
                    priceCatalog.AddEntry(new CatalogEntry(catalogFields[NAME_INDEX], Convert.ToDouble(catalogFields[PRICE_INDEX]), Convert.ToInt32(catalogFields[ID_INDEX])));
                } 
            }
        }
        catch (Exception e) when (e is IOException || e is OutOfMemoryException)
        {
            throw new PriceCatalogReadException("Exception while reading file.", e);
        }
        catch (Exception e) when (e is FormatException || e is OverflowException || e is KeyNotFoundException)
        {
            throw new PriceCatalogReadException("Invalid price catalog entry format.", e);
        }
        catch (Exception e) when (e is ArgumentException || e is ArgumentNullException || e is FileNotFoundException 
        || e is DirectoryNotFoundException || e is PathTooLongException || e is UnauthorizedAccessException)
        {
            throw new PriceCatalogReadException("Exception while opening file.", e);
        }

        return priceCatalog;
    }
}