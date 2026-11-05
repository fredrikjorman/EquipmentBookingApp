
namespace EquipmentBookingApp;

public class BookingService
{
    private readonly BookingRepository _repository;

    public BookingService(BookingRepository repository)
    {
        _repository = repository;
    }

    public bool IsAvailable(string equipmentName, DateTime startDate, DateTime endDate)
    {
        var bookings = _repository.GetAllBookings();

        foreach (var booking in bookings)
        {
            if (booking.EquipmentName == equipmentName &&
                booking.Status == "Active" &&
                startDate.Date <= booking.EndDate.Date &&
                endDate.Date >= booking.StartDate.Date)
            {
                return false;
            }
        }

        return true;
    }

    public bool ValidateDates(DateTime startDate, DateTime endDate)
    {
        return startDate.Date <= endDate.Date;
    }

    public decimal CalculateTotalPrice(DateTime startDate, DateTime endDate)
    {
        if (!ValidateDates(startDate, endDate))
        {
            throw new ArgumentException(
                "Start date cannot be after end date.");
        }

        int days = (endDate.Date - startDate.Date).Days + 1;

        return days * 100m;
    }

    public Booking CreateBooking(
        string customerName,
        string equipmentName,
        DateTime startDate,
        DateTime endDate)
    {
        if (!ValidateDates(startDate, endDate))
        {
            throw new ArgumentException(
                "Start date cannot be after end date.");
        }

        if (string.IsNullOrWhiteSpace(customerName) ||
            string.IsNullOrWhiteSpace(equipmentName))
        {
            throw new ArgumentException(
                "Customer and equipment are required.");
        }

        customerName = customerName.Trim();
        equipmentName = equipmentName.Trim();

        if (!IsAvailable(equipmentName, startDate, endDate))
        {
            throw new InvalidOperationException(
                "Equipment is not available for the selected period.");
        }

        var booking = new Booking
        {
            BookingNumber = Guid.NewGuid().ToString(),
            CustomerName = customerName,
            EquipmentName = equipmentName,
            StartDate = startDate.Date,
            EndDate = endDate.Date,
            TotalPrice = CalculateTotalPrice(startDate, endDate)
        };

        _repository.AddBooking(booking);

        return booking;
    }
}
