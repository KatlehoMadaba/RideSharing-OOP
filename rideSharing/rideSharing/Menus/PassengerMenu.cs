using System;
using System.Collections.Generic;
using rideSharing.RideRequestSystem;
using RideSharing;
namespace rideSharing.Menus
{
    //Displays thh passanger menu
    public static class PassengerMenu
    {
        public static void PassengerMainMenu(Passenger passenger)
        {
            string option;
            do
            {
                Console.WriteLine("Please pick an option:");
                Console.WriteLine("1.Request a Ride (Enter pickup & drop-off location");
                Console.WriteLine("2.View Wallet Balance?");
                Console.WriteLine("3.Add Funds to your wallet?");
                Console.WriteLine("4.View your ride history");
                Console.WriteLine("5.Rate a Driver");
                Console.WriteLine("0.Exit");

                option = Console.ReadLine();
                Console.WriteLine("===========================");
                Console.WriteLine($"You picked option {option}");
                switch (option)
                {
                    case "1":
                        RideSystem.RequestRide(passenger);
                        break;
                    case "2":
                        Console.WriteLine($"This is your Balance:{passenger.WalletBalance}");
                        break;
                    case "3":
                        Console.WriteLine("Please enter how much you want to add:");
                        double amount = Convert.ToDouble(Console.ReadLine());
                        passenger.AddFunds(amount);
                        break;
                    case "4":
                        passenger.DisplayRideHistory();
                        break;
                    case "5":
                        //checking if the passenger has a trip history
                        if (passenger.TripHistory.Count == 0)
                        {
                            Console.WriteLine("You cannot rate a driver because you have no ride history.");
                            break;
                        }
                        //Allowing the passenger select the driver they want to rate 
                        Console.WriteLine("Select a trip to rate the driver from your ride history:");
                        for (int i = 0; i < passenger.TripHistory.Count; i++)
                        {
                            var trip = passenger.TripHistory[i];
                            Console.WriteLine($"{i + 1}. Driver: {trip.Driver.Username}, From: {trip.PickUp} to {trip.DropOff}, Cost: {trip.Cost:C}");
                        }
                        //Allowing the user to select the trip they want to rate 
                        Console.WriteLine("Enter the number of the trip to rate the driver:");
                        int tripChoice;
                        while (!int.TryParse(Console.ReadLine(), out tripChoice) || tripChoice < 1 || tripChoice > passenger.TripHistory.Count)
                        {
                            Console.WriteLine("Invalid input. Please enter a valid number.");
                        }

                        //giving the passenger the option to enter the rating 
                        var selectedTrip = passenger.TripHistory[tripChoice - 1];
                        Console.WriteLine($"You selected Driver: {selectedTrip.Driver.Username}. Enter a rating between 1 and 5:");
                        int rating;
                        while (!int.TryParse(Console.ReadLine(), out rating) || rating < 1 || rating > 5)
                        {
                            Console.WriteLine("Invalid input. Please enter a rating between 1 and 5.");
                        }

                        passenger.RateDriver(selectedTrip.Driver, rating);
                        break;
                    case "0":
                        break;
                    default:
                        Console.WriteLine("Please enter valid option between numbers 0-5");
                        break;
                }
            } while (option != "0");
        }

    }
}
