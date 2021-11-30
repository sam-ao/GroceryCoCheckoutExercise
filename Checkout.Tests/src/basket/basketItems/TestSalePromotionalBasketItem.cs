using Xunit;
using System;
using GroceryCo.Checkout;
 
public class TestSalePromotionalBasketItem
{
    public const int TEST_ID = 1;
    public const int TEST_QUANTITY = 2;
    public const string TEST_NAME = "NAME";
    public const double TEST_PRICE = 1.0;
    public const double TEST_REGULAR_PRICE = 2.0;
    public static readonly SalePromotion TEST_PROMOTION_PERCENT = new SalePromotion(2, TEST_ID, DiscountTypeEnum.Percentage, 0.5);
    public static readonly SalePromotion TEST_PROMOTION_FLAT = new SalePromotion(2, TEST_ID, DiscountTypeEnum.Flat, 0.3);

    [Fact]
    public void TestConstructor()
    {
        var item = new SalePromotionalBasketItem(TEST_ID, TEST_NAME, TEST_QUANTITY, TEST_PRICE, TEST_REGULAR_PRICE, TEST_PROMOTION_PERCENT);
        Assert.Equal(TEST_ID, item.Id);
        Assert.Equal(TEST_NAME, item.Name);
        Assert.Equal(TEST_QUANTITY, item.Quantity);
        Assert.Equal(TEST_PRICE, item.Price);
        Assert.Equal(TEST_REGULAR_PRICE, item.RegularPrice);
        Assert.Equal(TEST_PROMOTION_PERCENT, item.Promotion);
    }

    [Fact]
    public void TestGetOutputNamePercentage()
    {
        var item = new SalePromotionalBasketItem(TEST_ID, TEST_NAME, TEST_QUANTITY, TEST_PRICE, TEST_REGULAR_PRICE, TEST_PROMOTION_PERCENT);
        Assert.Equal("NAME - 50.00 % OFF", item.GetOutputName());
    }

    [Fact]
    public void TestGetOutputNamePercentageNegative()
    {
        SalePromotion promotion = new SalePromotion(2, TEST_ID, DiscountTypeEnum.Percentage, -0.5);
        var item = new SalePromotionalBasketItem(TEST_ID, TEST_NAME, TEST_QUANTITY, TEST_PRICE, TEST_REGULAR_PRICE, promotion);
        Assert.Equal("NAME - 150.00 % OFF", item.GetOutputName());
    }

    [Fact]
    public void TestGetOutputNameFlat()
    {
        var item = new SalePromotionalBasketItem(TEST_ID, TEST_NAME, TEST_QUANTITY, TEST_PRICE, TEST_REGULAR_PRICE, TEST_PROMOTION_FLAT);
        Assert.Equal("NAME - $0.30 OFF", item.GetOutputName());
    }

    [Fact]
    public void TestGetOutputRegularPriceOverflow()
    {
        var item = new SalePromotionalBasketItem(TEST_ID, TEST_NAME, TEST_QUANTITY, TEST_PRICE, double.MaxValue, TEST_PROMOTION_FLAT);
        Assert.Throws<OverflowException>(() => item.GetOutputRegularPrice());
    }

    [Fact]
    public void TestGetOutputRegularPriceBasic()
    {
        var item = new SalePromotionalBasketItem(TEST_ID, TEST_NAME, TEST_QUANTITY, TEST_PRICE, TEST_REGULAR_PRICE, TEST_PROMOTION_FLAT);
        Assert.Equal("$4.00", item.GetOutputRegularPrice());
    }

    [Fact]
    public void TestGetOutputRegularPriceNegative()
    {
        var item = new SalePromotionalBasketItem(TEST_ID, TEST_NAME, TEST_QUANTITY, TEST_PRICE, TEST_REGULAR_PRICE * -1, TEST_PROMOTION_FLAT);
        Assert.Equal("($4.00)", item.GetOutputRegularPrice());
    }
}