namespace GroceryCo.Checkout;

/*
    An object that scans a grocery list and adds them to a basket
*/
public interface IItemScanner
{
    /*
        Scans the items and adds them to the provided basket

        @throws ItemScanningException: An exception occurred while reading the grocery list
    */
    public void ScanItems(ref Basket basket);
}
