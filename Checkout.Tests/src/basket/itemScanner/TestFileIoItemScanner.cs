using Xunit;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using GroceryCo.Checkout;
using Moq;

public class TestFileIoItemScanner
{
    public const string TEST_FILEPATH = @"C:\temp\in.txt";
    public static readonly PriceCatalog TEST_PRICE_CATALOG_EMPTY = new PriceCatalog();
    private Basket _testBasket;
    private FileIoItemScanner _testFileIoItemScanner;
    private Mock<IFileSystem> _moqFileSystem;
    private MockFileSystem _mockFileSystem; 
    private Mock<IFile> _mockFile; 
    private Mock<StreamReader> _mockStreamReader;
    public TestFileIoItemScanner()
    {
        _moqFileSystem = new Mock<IFileSystem>();
        _mockFile = new Mock<IFile>();
        _mockStreamReader = new Mock<StreamReader>(new MemoryStream());

        _moqFileSystem.SetupGet(m => m.File).Returns(_mockFile.Object);
        _mockFile.Setup(m => m.OpenText(It.IsAny<string>())).Returns(_mockStreamReader.Object);
        _mockStreamReader.Setup(m => m.Peek()).Returns(1);
        _testFileIoItemScanner = new FileIoItemScanner(TEST_FILEPATH, TEST_PRICE_CATALOG_EMPTY, _moqFileSystem.Object);
        _testBasket = new Basket();

        _mockFileSystem = new MockFileSystem();
    }

    [Theory]
    [MemberData(nameof(GetOpenStreamReaderExceptions))]
    public void TestScanItemsOpenStreamReaderExceptions(Exception e)
    {
        _mockFile.Setup(m => m.OpenText(It.IsAny<string>())).Throws(e);
        Assert.Throws<ItemScanningException>(() => _testFileIoItemScanner.ScanItems(ref _testBasket));
    }

    [Theory]
    [MemberData(nameof(GetReadLineExceptions))]
    public void TestScanItemsReadLineExceptions(Exception e)
    {
        _mockStreamReader.Setup(m => m.ReadLine()).Throws(e);

        Assert.Throws<ItemScanningException>(() => _testFileIoItemScanner.ScanItems(ref _testBasket));
    }

    [Fact]
    public void TestScanItemsCatalogDoesntContainItem()
    {
        _mockStreamReader.Setup(m => m.ReadLine()).Returns("Apple");
    }

    [Fact]
    public void TestScanItemsIncrementQuantity()
    {
        var priceCatalog = new PriceCatalog();
        priceCatalog.AddEntry(new CatalogEntry("Apple", 1.0, 1));
        _testFileIoItemScanner = new FileIoItemScanner(TEST_FILEPATH, priceCatalog, _mockFileSystem);
        _testBasket.AddRegularItem(new RegularBasketItem(1, "Apple", 1, 1.0));
        var mockInputFile = new MockFileData("Apple\nApple\nApple\n");
        _mockFileSystem.AddFile(TEST_FILEPATH, mockInputFile);

        _testFileIoItemScanner.ScanItems(ref _testBasket);

        Assert.Equal(4, _testBasket.RegularItems[1].Quantity);
    }

    [Fact]
    public void TestScanItemsAddRegularItem()
    {
        var priceCatalog = new PriceCatalog();
        priceCatalog.AddEntry(new CatalogEntry("Apple", 1.0, 1));
        _testFileIoItemScanner = new FileIoItemScanner(TEST_FILEPATH, priceCatalog, _mockFileSystem);
        _testBasket.AddRegularItem(new RegularBasketItem(1, "Apple", int.MaxValue, 1.0));
        var mockInputFile = new MockFileData("Apple\nApple\nApple\n");
        _mockFileSystem.AddFile(TEST_FILEPATH, mockInputFile);

        Assert.Throws<ItemScanningException>(() => _testFileIoItemScanner.ScanItems(ref _testBasket));
    }

    [Fact]
    public void TestScanItemsIncrementQuantityOverflow()
    {
        var priceCatalog = new PriceCatalog();
        priceCatalog.AddEntry(new CatalogEntry("Apple", 1.0, 1));
        _testFileIoItemScanner = new FileIoItemScanner(TEST_FILEPATH, priceCatalog, _mockFileSystem);
        _testBasket.AddRegularItem(new RegularBasketItem(1, "Apple", 1, 1.0));
        var mockInputFile = new MockFileData("Apple\nApple\nApple\n");
        _mockFileSystem.AddFile(TEST_FILEPATH, mockInputFile);

        _testFileIoItemScanner.ScanItems(ref _testBasket);

        Assert.Equal(4, _testBasket.RegularItems[1].Quantity);
    }

    public static IEnumerable<object[]> GetOpenStreamReaderExceptions()
    {
        var data = new List<object[]>
        {
            new object[] {new ArgumentException()},
            new object[] {new ArgumentNullException()},
            new object[] {new FileNotFoundException()}, 
            new object[] {new DirectoryNotFoundException()},
            new object[] {new PathTooLongException()},
            new object[] {new UnauthorizedAccessException()}
        };

        return data;
    }
    public static IEnumerable<object[]> GetReadLineExceptions()
    {
        var data = new List<object[]>
        {
            new object[] {new IOException()},
            new object[] {new OutOfMemoryException()}
        };

        return data;
    }
}