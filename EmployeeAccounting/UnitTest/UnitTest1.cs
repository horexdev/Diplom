using EmployeeAccounting;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var bytes = new byte[]
            {
                1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15
            };
            var bitmapImage = ImageConverter.ConvertBinaryToImage(bytes);

            Assert.IsNotNull(bitmapImage);
        }

        [TestMethod]
        public void TestMethod2()
        {
            var bytes2 = ImageConverter.ConvertImageToBinary(@"C:\Users\horex\Desktop\Images.jpg");
            Assert.IsNotNull(bytes2);
        }
    }
}