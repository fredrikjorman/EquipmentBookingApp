using EquipmentBookingApp;

var repository = new BookingRepository();
var bookingService = new BookingService(repository);

string customerName = "Anna Andersson";
string equipmentName = "Kamera Canon";

while (true)
{
    Console.Write("Vill du skapa en bokning? (ja/nej): ");
    string? choice = Console.ReadLine();

    if (choice?.Trim().ToLower() != "ja")
    {
        Console.WriteLine("Programmet avslutas.");
        break;
    }
    Console.Write("Ange startdatum (ÅÅÅÅ-MM-DD): ");
    string? startInput = Console.ReadLine();
    Console.Write("Ange slutdatum (ÅÅÅÅ-MM-DD): ");
    string? endInput = Console.ReadLine();

    if (!DateTime.TryParse(startInput, out DateTime startDate))
    {
        Console.WriteLine("Ogiltigt startdatum.");
        continue;
    }

    if (!DateTime.TryParse(endInput, out DateTime endDate))
    {
        Console.WriteLine("Ogiltigt slutdatum.");
        continue;
    }

    if (!bookingService.ValidateDates(startDate, endDate))
    {
        Console.WriteLine("Slutdatum kan inte vara före startdatum.");
        continue;

    }

    if (!bookingService.IsAvailable(equipmentName, startDate, endDate))
    {
        Console.WriteLine("Utrustningen är redan bokad under denna period.");
        continue;
    }

    decimal totalPrice = bookingService.CalculateTotalPrice(startDate, endDate);
    Console.WriteLine($"Totalpris: {totalPrice} kr");


    Console.Write("Vill du bekräfta bokningen? (ja/nej): ");
    string? confirmation = Console.ReadLine();

    if (confirmation?.Trim().ToLower() == "ja")
    {
        var booking = bookingService.CreateBooking(
            customerName,
            equipmentName,
            startDate,
            endDate);

        Console.WriteLine($"Bokning bekräftad!");
        Console.WriteLine($"Bokningsnummer: {booking.BookingNumber}");
        Console.WriteLine($"Totalpris: {booking.TotalPrice} kr");
    }
    else
    {
        Console.WriteLine("Bokningen avbröts.");
    }

}
