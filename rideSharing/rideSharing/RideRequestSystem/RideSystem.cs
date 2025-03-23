using System;
using System.Collections.Generic;
using System.Linq;
using RideSharing;

namespace rideSharing.RideRequestSystem
{
    //  Any thing the sdriver returns to the user whether driver or passenger
    public class RideSystem
    {
        private static readonly double ratePerKm = 10.0;

        public static List<string> locations = new List<string> { "CENTURION", "PRETORIA", "JHB", "HATFIELD", "MIDRAND" };
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
                UserManger.Instance.UpdateUserData();
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
                //Calculating trip amount :
                Random random = new Random();
                double distance = random.Next(5, 101);// Generate random distance for the trip between 5 to 100km
                double tripCost = distance * ratePerKm;

                if (passenger.WalletBalance < tripCost)
                {
                    Console.WriteLine($"Insufficient funds! Trip cost is {tripCost:C}, but your wallet balance is {passenger.WalletBalance:C}.");
                    return;
                }
                if (!DisplayAvaibleDrivers())
                {
                    return;//exit the request if there are no available drivers
                }
                else
                {
                    passenger.AddRideToHistory(pickUp, dropOff, distance, tripCost);
                    Console.WriteLine($"Ride request completed successfully! From {pickUp} to {dropOff}");
                    Console.WriteLine("=====================================");
                    UserManger.Instance.UpdateUserData();
                    break;
                }
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
        public static bool DisplayAvaibleDrivers()
        {
            try
            {
                var availableDrivers = UserManger.LoadAvaibleDrivers();
                Console.WriteLine("=====================================");
                if (availableDrivers.Count > 0)
                {
                    foreach (var driver in availableDrivers)
                    {
                        Console.WriteLine("Driver The details of driver to pick you up:");
                        Console.WriteLine($"Name: {driver.Username}, Car: {driver.Car}, Number Plate: {driver.NoPlate}");
                    }
                    return true;
                }
                else
                {
                    Console.WriteLine("Sorry there are no driver available right now");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Sorry there was an error loading the drivers :{ex.Message}");
                return false;
            }
        }
        public static string DriversCurrentLocation(Driver driver)
        {
            Console.WriteLine("Please choose your current location:");
            foreach (var location in locations)
            {
                Console.WriteLine(location);
            }
            string selectedLocation = Console.ReadLine()?.ToUpper();
            while (!IsValidLocation(selectedLocation,locations))
            {
                foreach (var location in locations)
                {
                    Console.WriteLine(location);
                }
            selectedLocation = Console.ReadLine()?.ToUpper();
            }
            var driversLocationList = User.userList.OfType<Driver>().FirstOrDefault(u => u.Username == driver.Username);
            if (driversLocationList != null)
            {
                driversLocationList.CurrentLocation = selectedLocation;

            }
            UserManger.Instance.UpdateUserData();
            return selectedLocation;    
        }
        public static void ViewRequests()
        {

        }
    }
}
