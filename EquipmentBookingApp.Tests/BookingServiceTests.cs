
using EquipmentBookingApp;
using Xunit;

namespace EquipmentBookingApp.Tests;

public class BookingServiceTests
{
    [Fact]
    public void ValidateDates_ShouldReturnTrue_WhenDatesAreValid()
    {
        // Arrange
        var repository = new BookingRepository();
        var service = new BookingService(repository);

        DateTime startDate = new DateTime(2026, 11, 10);
        DateTime endDate = new DateTime(2026, 11, 12);

        // Act
        bool result = service.ValidateDates(startDate, endDate);

        // Assert
        Assert.True(result);
    }
    [Fact]
    public void ValidateDates_ShouldReturnFalse_WhenEndDateIsBeforeStartDate()
    {
        // Arrange
        var repository = new BookingRepository();
        var service = new BookingService(repository);

        DateTime startDate = new DateTime(2026, 11, 15);
        DateTime endDate = new DateTime(2026, 11, 10);

        // Act
        bool result = service.ValidateDates(startDate, endDate);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void CalculateTotalPrice_ShouldReturnCorrectPrice()
    {
        // Arrange
        var repository = new BookingRepository();
        var service = new BookingService(repository);

        DateTime startDate = new DateTime(2026, 11, 10);
        DateTime endDate = new DateTime(2026, 11, 12);

        // Act
        decimal result = service.CalculateTotalPrice(startDate, endDate);

        // Assert
        Assert.Equal(300m, result);
    }

    [Fact]
    public void IsAvailable_ShouldReturnFalse_WhenDatesOverlap()
    {
        // Arrange
        var repository = new BookingRepository();
        var service = new BookingService(repository);

        service.CreateBooking(
            "Anna Andersson",
            "Kamera Canon",
            new DateTime(2026, 11, 10),
            new DateTime(2026, 11, 12));

        // Act
        bool result = service.IsAvailable(
            "Kamera Canon",
            new DateTime(2026, 11, 11),
            new DateTime(2026, 11, 13));

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void CreateBooking_ShouldThrowException_WhenEquipmentIsAlreadyBooked()
    {
        // Arrange
        var repository = new BookingRepository();
        var service = new BookingService(repository);

        service.CreateBooking(
            "Anna Andersson",
            "Kamera Canon",
            new DateTime(2026, 11, 10),
            new DateTime(2026, 11, 12));

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() =>
            service.CreateBooking(
                "Erik Svensson",
                "Kamera Canon",
                new DateTime(2026, 11, 11),
                new DateTime(2026, 11, 13)));
    }

    [Fact]
    public void CreateBooking_ShouldGenerateIdAndSaveBooking()
    {
        // Arrange
        var repository = new BookingRepository();
        var service = new BookingService(repository);

        // Act
        var booking = service.CreateBooking(
            "Anna Andersson",
            "Kamera Canon",
            new DateTime(2026, 11, 10),
            new DateTime(2026, 11, 12));

        // Assert
        Assert.NotEqual(Guid.Empty, booking.Id);
        Assert.False(string.IsNullOrWhiteSpace(booking.BookingNumber));
        Assert.Single(repository.GetAllBookings());
        Assert.Contains(booking, repository.GetAllBookings());
    }


    [Fact]
    public void CreateBooking_ShouldGenerateUniqueBookingNumbers()
    {
        // Arrange
        var repository = new BookingRepository();
        var service = new BookingService(repository);

        // Act
        var booking1 = service.CreateBooking(
            "Anna Andersson",
            "Kamera Canon",
            new DateTime(2026, 11, 10),
            new DateTime(2026, 11, 12));

        var booking2 = service.CreateBooking(
            "Erik Svensson",
            "Kamera Canon",
            new DateTime(2026, 11, 13),
            new DateTime(2026, 11, 15));

        // Assert
        Assert.NotEqual(booking1.Id, booking2.Id);
        Assert.NotEqual(booking1.BookingNumber, booking2.BookingNumber);
        Assert.Equal(2, repository.GetAllBookings().Count);
    }


}
