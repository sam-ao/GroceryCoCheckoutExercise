
/*
    An exception that occurs when there was an error while scanning items into a basket
*/
public class ItemScanningException : Exception
{
    public ItemScanningException(string message, Exception inner) : base(message, inner)
    {
    }

    public ItemScanningException(string message) : base(message)
    {
    }
}