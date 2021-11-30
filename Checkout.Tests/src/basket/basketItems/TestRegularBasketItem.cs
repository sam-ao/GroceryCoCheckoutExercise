using Xunit;
using System;
using GroceryCo.Checkout;
 
public class TestRegularBasketItem
{
    public const int TEST_ID = 1;
    public const string TEST_NAME = "NAME";
    public const double TEST_PRICE = 1.0; 

    [Fact]
    public void TestConstructor()
    {
        var item = new RegularBasketItem(TEST_ID, TEST_NAME, TEST_PRICE);
        Assert.Equal(TEST_ID, item.Id);        
        Assert.Equal(TEST_NAME, item.Name);
        Assert.Equal(1, item.Quantity);
        Assert.Equal(TEST_PRICE, item.Price);
    }

    [Fact]
    public void TestIncrementQuantityOverflow()
    {
        var item = new RegularBasketItem(TEST_ID, TEST_NAME, int.MaxValue, TEST_PRICE);
        Assert.Throws<OverflowException>(item.IncrementQuantity);
    }

    [Fact]
    public void TestIncrementQuantityBasic()
    {
        var item = new RegularBasketItem(TEST_ID, TEST_NAME, TEST_PRICE);
        item.IncrementQuantity();
        Assert.Equal(2, item.Quantity);
    }

    [Fact]
    public void TestIncrementQuantityMultiple()
    {
        var item = new RegularBasketItem(TEST_ID, TEST_NAME, TEST_PRICE);
        item.IncrementQuantity();
        item.IncrementQuantity();
        item.IncrementQuantity();
        Assert.Equal(4, item.Quantity);
    }

    [Fact]
    public void TestGetOutputPriceOverflow()
    {
        var item = new RegularBasketItem(TEST_ID, TEST_NAME, 2, double.MaxValue);
        Assert.Throws<OverflowException>(() => item.GetOutputPrice());
    }

    [Fact]
    public void TestGetOutputPriceBasic()
    {
        var item = new RegularBasketItem(TEST_ID, TEST_NAME, 3, 4.0);
        Assert.Equal("$12.00", item.GetOutputPrice());
    }

    [Fact]
    public void TestGetOutputPriceNegative()
    {
        var item = new RegularBasketItem(TEST_ID, TEST_NAME, 3, -4.0);
        Assert.Equal("($12.00)", item.GetOutputPrice());
    }
}