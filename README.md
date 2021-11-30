# GroceryCo Checkout App
Author: Sam Ao

### Assumptions:
 * Promotion input format
 * Price catalog input format
 * Currency uses US style
 * Order of output on receipt doesn't matter

### Wish List:
 * Logging
 * Better thought out folder structure
 * Fleshed out unit tests
 * Automated application level tests
 * Config to describe catalog locations instead of constants file
 
### Setup:
 1. Run `dotnet build`
 2. Go to build output directory
 3. Run `Checkout.exe <PATH TO GROCERY LIST>` ie. `Checkout.exe ..\..\..\demoGroceryList.txt`

 `dotnet test` to run unit tests


#### Price Catalog Format:
 * CSV format
 * name,price,id

#### Promotion Catalog Format:
 * CSV format
 * First index is always promotion type (Sale|Bogo|Group)
 * Sale promotion
    * promotionType,promotionId,saleItemId,discountType(Flat|Percentage),discountValue
 * BOGO promotion
    * promotionType,promotionId,saleItemId,discountType(Flat|Percentage),discountValue
 * Group promotion
    * promotionType,promotionId,saleItemId,groupSize,discountedPrice
