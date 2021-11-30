namespace GroceryCo.Checkout;
using System.IO.Abstractions;

/*
    A IPromotionCatalog that reads the promotion catalog from a file
*/
public class FileIoPromotionCatalogReader : IPromotionCatalogReader
{    
    private const int TYPE_INDEX = 0;
    private const char PROMOTION_FIELD_SEPARATOR = ',';

    public FileIoPromotionCatalogReader(string filePath, IFileSystem fileSystem, 
    Dictionary<PromotionTypesEnum, IPromotionParser> promotionParserDict)
    {
        _filePath = filePath;
        _fileSystem = fileSystem;
        _promotionParserDict = promotionParserDict;
    }

    public FileIoPromotionCatalogReader(string filePath, IFileSystem fileSystem) : 
    this(filePath, fileSystem, new Dictionary<PromotionTypesEnum, IPromotionParser>())
    {}

    private Dictionary<PromotionTypesEnum, IPromotionParser> _promotionParserDict;
    private string _filePath;
    private IFileSystem _fileSystem;

    public void AddPromotionParser(IPromotionParser promotion)
    {
        _promotionParserDict.Add(promotion.PromotionType, promotion);
    }

    public PromotionCatalog ReadPromotionCatalog()
    {
        PromotionCatalog promotions = new PromotionCatalog();
        try
        {
            using (var streamReader = _fileSystem.File.OpenText(_filePath))
            {
                while (!streamReader.EndOfStream)
                {
                    #pragma warning disable 8602
                    var promotionFields = streamReader.ReadLine().Split(PROMOTION_FIELD_SEPARATOR);
                    #pragma warning restore 8602
                    var promotionType = PromotionTypes.GetPromotionType(promotionFields[TYPE_INDEX]);
                    if (promotionType == PromotionTypesEnum.Unknown)
                    {
                        throw new PromotionParseException("Unrecognized promotion type.");
                    }
                    if (!_promotionParserDict.ContainsKey(promotionType))
                    {
                        throw new PromotionParseException("No parser available for promotion type.");
                    }
                    promotions.AddPromotion(_promotionParserDict[promotionType].ParsePromotion(promotionFields));
                } 
            }
        }
        catch (Exception e) when (e is IOException || e is OutOfMemoryException)
        {
            throw new PromotionCatalogReadException("Exception while reading file.", e);
        }
        catch (Exception e) when (e is ArgumentException || e is ArgumentNullException || e is FileNotFoundException 
        || e is DirectoryNotFoundException || e is PathTooLongException || e is UnauthorizedAccessException)
        {
            throw new PromotionCatalogReadException("Exception while opening file.", e);
        }

        return promotions;
    }
}