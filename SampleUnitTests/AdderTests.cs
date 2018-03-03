using UnitTestLibrary;

namespace SampleUnitTests
{
    [TestClass]
    public class AdderTests
    {
        [TestMethod]
        public void One_Plus_One_Should_Equal_Two()
        {
            // Arrange
            int a = 1;
            int b = 2;
            Adder adder = new Adder();
            // Act
            int result = adder.Add(a, b);
            // Assert
            Assert.ShouldEqual(2, result);
        }
    }
}
