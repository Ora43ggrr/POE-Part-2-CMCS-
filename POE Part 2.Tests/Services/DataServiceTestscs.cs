using Xunit;

namespace POE_Part_2.Tests.Services
{
    public class DataServiceTests
    {
        [Fact]
        public void SimpleDataTest_ShouldPass()
        {
            // Arrange
            var expected = "test";

            // Act
            var actual = "test";

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void MathTest_ShouldPass()
        {
            // Arrange
            var a = 10;
            var b = 5;

            // Act
            var result = a * b;

            // Assert
            Assert.Equal(50, result);
        }
    }
}