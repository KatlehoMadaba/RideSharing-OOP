using System;
using System.Collections.Generic;
using System.Linq;
using RideSharing;

namespace rideSharing.RideRequestSystem
{
    //  Any thing the system returns to the user whether driver or passenger
    public class RideSystem
    {
        private static readonly double ratePerKm = 10.0;

        public static List<string> locations = new List<string> { "CENTURION", "PRETORIA", "JHB", "HATFIELD", "MIDRAND" };
        public static void RateDriver(Passenger passenger, Driver driver, int stars)
        {
            try
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
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while rating the driver: + {ex}.Message");
            }
        }
        public static void RequestRide(Passenger passenger, List<string> locations)
        {
            // Get valid pick-up location
            string pickUp = GetValidLocation("Please choose your pick up location:", locations);
            // Get valid drop-off location and ensure it is not the same as pick-up.
            string dropOff = GetValidLocation("Please choose your drop off location:", locations);

            while (dropOff.Equals(pickUp.ToUpper()))
            {
                Console.WriteLine("Sorry, your pick-up and drop off locations cannot be the same.");
                dropOff = GetValidLocation("Please choose a different drop off location:", locations);
            }

            //Calculate trip amount
            var (distance, tripCost) = CalculateTripAmount();

            if (passenger.WalletBalance < tripCost)
            {
                Console.WriteLine($"Insufficient funds! Trip cost is {tripCost:C}, but your wallet balance is {passenger.WalletBalance:C}.");
                return;
            }
            //Filter and assign an available driver based on the pickup location.
            Driver assignedDriver = AssignDriverForPickup(pickUp);
            if (assignedDriver == null)
            {
                Console.WriteLine($"No available drivers at your pick-up location: {pickUp}");
                return;
            }
            else
            {
                Console.WriteLine($"Driver assigned: {assignedDriver.Username}, Car: {assignedDriver.Car}, Number Plate: {assignedDriver.NoPlate}");
            }
            passenger.AddRideToHistory(pickUp, dropOff, distance, tripCost);
            Console.WriteLine($"Ride request completed successfully! From {pickUp} to {dropOff} at the cost of {tripCost:C}");
            Console.WriteLine("=====================================");
            UserManger.Instance.UpdateUserData();

        }
        private static (double tripDistance, double tripCost) CalculateTripAmount()
        {
            try
            {
                Random random = new Random();
                double distance = random.Next(5, 101);// Random distance between 5 and 100 km
                double cost = distance * ratePerKm;
                return (distance, cost);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while calculating trip amount: " + ex.Message);
                return (0, 0);
            }
        }
        private static string GetValidLocation(string prompt, List<string> locations)
        {
            while (true)
            {
                Console.WriteLine(prompt);
                Console.WriteLine("====================================");
                foreach (var location in locations)
                {
                    Console.WriteLine(location);
                }
                string input = Console.ReadLine()?.ToUpper();
                if (IsValidLocation(input, locations))
                {
                    return input;
                }
                else
                {
                    Console.WriteLine("Invalid location entered. Please try again.");
                }
            }
        }
        private static Driver AssignDriverForPickup(string pickUp)
        {
            try
            {
                // Filter available drivers whose current location matches the pick-up location
                var availableDrivers = UserManger.LoadAvaibleDrivers()
                    .Where(d => d.isAvailable &&
                                !string.IsNullOrEmpty(d.CurrentLocation) &&
                                d.CurrentLocation.Equals(pickUp, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                if (availableDrivers.Count > 0)
                {
                    // Assign the first available driver.
                    var assignedDriver = availableDrivers.First();
                    assignedDriver.isAvailable = false;
                    UserManger.Instance.UpdateUserData();
                    return assignedDriver;
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while assigning a driver: {ex}.Message");
                return null;
            }

        }
        public static string DriversCurrentLocation(Driver driver)
        {
            try
            {
                string selectedLocation = GetValidLocation("Please select your current location:", locations);

                //using the user list to update the drivers current location
                var driverInList = User.userList.OfType<Driver>().FirstOrDefault(u => u.Username == driver.Username);

                if (driverInList != null)
                {
                    driverInList.CurrentLocation = selectedLocation;
                }
                UserManger.Instance.UpdateUserData();
                return selectedLocation;
            }

            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while setting the driver's current location:{ex}.Message");
                return null;
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
    }
}
