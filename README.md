Assumptions:
 * Promotion input format
 * Price catalog input format
 * Currency uses US style
 * Order of output on receipt doesn't matter

Wish List:
 * Logging
 * Better thought out folder structure
 * Fleshed out unit tests
 * Automated application level tests

Setup:
    1. Run `dotnet build`
    2. Go to build output directory
    3. Run `Checkout.exe <PATH TO GROCERY LIST>` ie. `Checkout.exe ..\..\..\demoGroceryList.txt`

    - `dotnet test` to run unit tests