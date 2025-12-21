namespace Coding_Tracker.Tests
{
    public class DateTimeValidatorTests
    {
        [Fact]
        public void ValidateDateResponse_ValidDate_ReturnsDateTime()
        {
            // Arrange
            var validDate = "25-12-2023 14:30";
            var format = "dd-MM-yyyy HH:mm";
            // Act
            var result = DateTimeValidator.ValidateDateResponse(validDate, format);
            // Assert
            Assert.Equal(new DateTime(2023, 12, 25, 14, 30, 0), result);
        }

        [Fact]
        public void ValidateDateResponse_InvalidDate_ThrowsFormatException()
        {
            // Arrange
            var invalidDate = "41/13/2025 14:30";
            var format = "dd-MM-yyyy HH:mm";
            // Act & Assert
            Assert.Throws<FormatException>(() => DateTimeValidator.ValidateDateResponse(invalidDate, format));
        }
    }
}
