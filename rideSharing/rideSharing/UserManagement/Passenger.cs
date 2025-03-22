using System;
using System.Collections.Generic;
using System.Linq;
using rideSharing.Menus;
using rideSharing.RideRequestSystem;

namespace RideSharing
{
    public class Passenger : User, IRideable
    {
        public double WalletBalance { get; set; }

        private const double RatePerKm = 10.0;

        public Passenger(string username, string email, string password, double initialBalance) : base(username, password, email)
        {
            Role = "Passenger";
            WalletBalance = initialBalance;
        }

        //Checking for valid location input
        private static bool IsValidLocation(string locationInput, List<string> locations)
        {
            if (!locations.Contains(locationInput))
            {
                Console.WriteLine($"Error:{locationInput} is a invalid location please try again");
                return false;

            }
            return true;
        }
        public void requestRide(List<string> locations)
        {
            bool validRideRequest = false;
            while (!validRideRequest)
            {
                string pickUp = "";
                string dropOff = "";

                //Entering pickup location
                bool validPickUp = false; bool validDropOff = false;
                while (!validPickUp)
                {
                    Console.WriteLine("\nPlease choose your pick up location");
                    Console.WriteLine("===================================");
                    foreach (var location in locations)
                    {
                        Console.WriteLine(location);
                    }
                    pickUp = Console.ReadLine().ToUpper();

                    if (IsValidLocation(pickUp, locations))
                    {
                        validPickUp = true;

                    }
                }

                //Entering drop off location
                while (!validDropOff)
                {
                    Console.WriteLine("Please choose your dropoff location");
                    Console.WriteLine("====================================");
                    foreach (var location in locations)
                    {

                        Console.WriteLine(location);

                    }
                    dropOff = Console.ReadLine().ToUpper();

                    if (!IsValidLocation(dropOff, locations))
                    {
                        continue; //if input is invalid return the main menu
                    }
                    if (pickUp == dropOff)
                    {
                        Console.WriteLine("Sorry but your pickup and drop of location cannot be the same");
                        continue;//return the drop off menu again
                    }
                    validDropOff = true;//if input is valid and different 
                }

                //Successuful
                //Calculating trip amount :
                // Generate random distance for the trip
                Random random = new Random();
                double distance = random.Next(5, 101);

                // Calculate the cost of the trip
                double tripCost = distance * RatePerKm;

                if (WalletBalance >= tripCost)
                {
                    WalletBalance -= tripCost;

                    // Add trip to the history
                    TripHistory.Add((pickUp, dropOff, distance, tripCost));
                    Console.WriteLine("===========================");
                    Console.WriteLine($"Ride request was successful!");
                    Console.WriteLine($"From {pickUp} to {dropOff}");
                    Console.WriteLine($"Distance: {distance} km | Cost: {tripCost:C}");
                    Console.WriteLine($"Remaining Wallet Balance: {WalletBalance:C}");

                    Console.WriteLine($"This is the available driver that will pick you up:");
                    //Display list of drivers that are avaliable and use random function to pickone to come pick them up

                    validRideRequest = true;
                    //Display list of drivers that are avaliable and use random function to pickone to come pick them up
                }
                else
                {
                    Console.WriteLine($"Insufficient funds! Trip cost is {tripCost:C}, but your wallet balance is {WalletBalance:C}.");
                    Console.WriteLine("Please add funds to your wallet and try again.");
                    break;
                }
            }
        }


        public void DisplayTripHistory(User user)
        {
            if (user.TripHistory.Count == 0)
            {
                Console.WriteLine("\nNo trip history avaialble");
                return;
            }
            else
            {
                Console.WriteLine("====================================");
                Console.WriteLine("\nTrip History:");
                foreach (var trip in user.TripHistory)
                {
                    Console.WriteLine($"From {trip.PickUp} to {trip.DropOff} | Distance: {trip.Distance} km | Cost: {trip.Cost:C}");
                }
                // Total cost of all trips
                double totalCost = user.TripHistory.Sum(t => t.Cost);
                Console.WriteLine($"Total Amount Spent: {totalCost:C}");
                Console.WriteLine("====================================");
            }

        }

        public override void DisplayMenu()
        {
            PassengerMenu.PassengerMainMenu(this);
        }

        public void AddFunds(double amount)
        {
            WalletBalance += amount;
            Console.WriteLine($"{amount:C}added to your wallet");
        }
    }



}