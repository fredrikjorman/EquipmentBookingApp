namespace EquipmentBookingApp;

public class BookingRepository
{
    private readonly List<Booking> _bookings = new();
    public void AddBooking(Booking booking)
    {
        _bookings.Add(booking);
    }
    public List<Booking> GetAllBookings()
    {
        return _bookings;
    }
}
