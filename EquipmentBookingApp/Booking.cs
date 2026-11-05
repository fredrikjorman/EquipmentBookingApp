namespace EquipmentBookingApp;

public class Booking
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string BookingNumber { get; set; } = string.Empty;

    public string CustomerName { get; set; } = string.Empty;
    public string EquipmentName { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal TotalPrice { get; set; }
    public string Status { get; set; } = "Active";
}