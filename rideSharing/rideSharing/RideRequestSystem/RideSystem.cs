using System;
using System.Collections.Generic;
using System.Linq;
using RideSharing;

namespace rideSharing.RideRequestSystem
{
    public class RideSystem
    {
        static double RatePerKm = 10.0;

        public static void RateDriver(Passenger passenger, Driver driver, int stars)
        {
            if (stars < 1 || stars > 5)
            {
                Console.WriteLine("This is an invalid rating your number must be between 1-5");
                return;
            }
            else
            {
                Console.WriteLine($"Thank you for raring your driver {stars} stars");
                driver.Ratings.Add(stars);
            }

        }
        public static void RequestRide(Passenger passenger, List<string> locations)
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
                Random random = new Random();
                double distance = random.Next(5, 101);// Generate random distance for the trip between 5 to 100km
                double tripCost = distance * RatePerKm;

                if (passenger.WalletBalance < tripCost)
                {
                    Console.WriteLine($"Insufficient funds! Trip cost is {tripCost:C}, but your wallet balance is {passenger.WalletBalance:C}.");
                    return;
                }
                passenger.AddRideToHistory(pickUp, dropOff, distance, tripCost);
                Console.WriteLine($"Ride request completed successfully! From {pickUp} to {dropOff}");
                UserManger userManager = new UserManger();
                userManager.UpdateUserData();
                break;
            }


        }
        private static bool IsValidLocation(string locationInput, List<string> locations)
        {
            if (!locations.Contains(locationInput))
            {
                Console.WriteLine($"Error:{locationInput} is a invalid location please try again");
                return false;

            }
            return true;
        }

        public static void ViewRequests()
        {

        }
    }
}
