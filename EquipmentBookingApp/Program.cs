using EquipmentBookingApp;

var repository = new BookingRepository();
var bookingService = new BookingService(repository);

Console.WriteLine("Equipment Booking App");
Console.WriteLine("Application started!");