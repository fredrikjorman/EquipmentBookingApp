
# EquipmentBookingApp

A simple equipment booking console application built with C# and .NET 10.

## Features

- Create equipment bookings for a selected rental period.
- Validate start and end dates.
- Check equipment availability.
- Prevent overlapping bookings.
- Calculate the total rental price.
- Confirm or cancel a booking.
- Generate a booking ID and booking number.
- Create multiple bookings during the same session.

## Technologies

- C#
- .NET 10
- Visual Studio
- xUnit

## Getting Started

### Requirements

- .NET 10 SDK
- Visual Studio or another compatible IDE

### Run the application

1. Clone the repository.
2. Open `EquipmentBookingApp.slnx` in Visual Studio.
3. Set `EquipmentBookingApp` as the startup project.
4. Press `F5` or `Ctrl + F5` to run the application.

Alternatively, use the terminal:

```bash
dotnet run --project EquipmentBookingApp
```

## Running Tests

The solution includes an xUnit test project that verifies booking validation, availability, price calculation and booking creation.

Run all tests using:

```bash
dotnet test
```

All 7 unit tests passed during development.

## How It Works

The application uses a predefined customer and equipment item.

The user enters a start date and an end date. The system validates the dates, checks availability and calculates the total price.

The rental price is fixed at 100 SEK per day, including both the start and end dates.

If the booking is confirmed, it is saved in memory and assigned a booking number.

## Project Structure

- `Booking.cs` – Booking model.
- `BookingService.cs` – Booking logic and validation.
- `BookingRepository.cs` – In-memory booking storage.
- `Program.cs` – Console interface.
- `EquipmentBookingApp.Tests` – Unit tests.

## Limitations

This is a simplified implementation of the booking functionality.

- Customer and equipment information are hardcoded.
- Bookings are stored in memory and are not saved after the application closes.
- There is no database or separate equipment inventory.
- The application is intended for demonstrating and testing the booking workflow.
