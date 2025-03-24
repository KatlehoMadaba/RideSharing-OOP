using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using RideSharing;

namespace rideSharing.RideRequestSystem
{
    //  Any thing the system returns to the user whether driver or passenger
    public class RideSystem
    {
        public static List<string> locations = Ride.ValidLocations;

        public static void RequestRide(Passenger passenger)
        {
            // Get valid pick-up location
            string pickUp = GetValidLocation("Please choose your pick up location:", locations);
            // Get valid drop-off location and ensure it is not the same as pick-up.
            string dropOff;
            do
            {
                dropOff = GetValidLocation("Please choose your drop-off location:", locations);
                if (pickUp.Equals(dropOff, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Sorry, your pick-up and drop-off locations cannot be the same. Please try again.");
                }
            } while (pickUp.Equals(dropOff, StringComparison.OrdinalIgnoreCase));


            // Create a new Ride object and calculate trip amount
            var ride = new Ride(passenger, null, pickUp, dropOff);

            double tripCost = ride.CalculateTripCost();


            if (passenger.WalletBalance < tripCost)
            {
                Console.WriteLine($"Insufficient funds! Trip cost is {tripCost:C}, but your wallet balance is {passenger.WalletBalance:C}.");
                Console.WriteLine("Please top up your wallet to proceed with the ride request.");
                return;
            }
            passenger.WalletBalance -= tripCost; // Deduct trip cost from wallet

            //Filter and assign an available driver based on the pickup location.
            Driver assignedDriver = AssignDriverForPickup(pickUp);
            if (assignedDriver == null)
            {
                Console.WriteLine($"No available drivers at your pick-up location: {pickUp}.Please try again later");
                return;
            }
            //setting the driver in the ride object
            ride.Driver = assignedDriver;

            assignedDriver.AddTripToHistory(ride);
            passenger.AddTripToHistory(ride);
            //update the earnings of the driver 
            CalculateDriversEarnings(ride);
            // Display ride details
            Console.WriteLine($"Ride request completed successfully!");
            Console.WriteLine($"Driver assigned: {assignedDriver.Username}, Car: {assignedDriver.Car}, Number Plate: {assignedDriver.NoPlate}");
            Console.WriteLine($"From {pickUp} to {dropOff} at the cost of {tripCost:C}");
            Console.WriteLine($"Distance: {ride.Distance} km");
            UserManger.Instance.UpdateUserData();
            Console.WriteLine("=====================================");
            //updating th system
        }
        public static void DisplayDriversHistory(Driver driver)
        {
            try
            {
                var driversObject = User.userList.OfType<Driver>().FirstOrDefault(u => u.Username == driver.Username);
                //Checking first if this driver exists
                if (driversObject == null)
                {
                    Console.WriteLine("Driver not found");
                    return;
                }
                //Checking if this driver has history
                if (driversObject.TripHistory == null || driversObject.TripHistory.Count == 0)
                {
                    Console.WriteLine("No history found for this driver");
                    return;
                }
                Console.WriteLine("Here is your drip history");
                Console.WriteLine("=============================================");
                foreach (var trip in driversObject.TripHistory)
                {
                    Console.WriteLine($"Passenger: {trip.Passenger.Username}| {trip.PickUp} to {trip.DropOff} | Distance: {trip.Distance} km | Cost: {trip.Cost:C}");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Sorry but there was a problem loading the history:{ex.Message}");
            }

        }
        private static string GetValidLocation(string prompt, List<string> locations)
        {
            while (true)
            {
                Console.WriteLine(prompt);
                Console.WriteLine("====================================");
                Console.WriteLine("The avaliable locations are:");
                Console.WriteLine(string.Join(", ", locations));
                //foreach (var location in locations)
                //{
                //    Console.WriteLine(location);
                //}
                string input = Console.ReadLine()?.ToUpper();
                //if (IsValidLocation(input, locations))
                //{
                //    return input;
                //}
                if (!string.IsNullOrWhiteSpace(input) && locations.Contains(input))
                {
                    return input;
                }
                else
                {
                    Console.WriteLine("Invalid location entered. Please try again.");
                }
            }
        }
        public static void GetAvaliablityStatus(Driver driver)
        {

            try
            {
                Console.WriteLine("If you are avaliable? type 'YES' if not 'NO' ");
                string status = Console.ReadLine()?.ToUpper().Trim();
                bool isStatusUpdated = false;
                while (!isStatusUpdated) {
                    switch (status)
                    {
                        case "YES":
                            driver.UpdateAvailablityStatus(true);
                            Console.WriteLine("Your status has been updated to AVAILABLE");
                            isStatusUpdated = true;
                            break;
                        case "NO":
                            driver.UpdateAvailablityStatus(false);
                            Console.WriteLine("Your status has been updated to UNAVAILABLE");
                            isStatusUpdated = true;
                            break;
                        default:
                            Console.WriteLine("That is not a valid selection please type 'YES' OR 'NO'!");
                            break;
                    }
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while trying to to update your status :{ex.Message}");
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
                Console.WriteLine($"An error occurred while assigning a driver: {ex.Message}");
                return null;
            }

        }
        private static double CalculateDriversEarnings(Ride ride)
        {
            try
            {
                double earnings = ride.Cost * 0.5;
                var driversList = User.userList.OfType<Driver>().ToList().FirstOrDefault(d => d.Username == ride.Driver.Username);
                if (driversList != null)
                {
                    driversList.Earnings += earnings;
                    Console.WriteLine($"Drivers {ride.Driver.Username}'s updated  totals earnings are: {ride.Driver.Username} ");
                }
                else 
                {
                    Console.WriteLine($"Driver {ride.Driver.Username} you were not found oon the list for earnings.");
                }
                    return earnings;

            }
            catch (Exception ex)
            {

                Console.WriteLine($"An error occurred while calculating drivers earnings:{ex.Message}");
                return 0;
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
                Console.WriteLine("======================");
                Console.WriteLine($"Your current location is :{driver.CurrentLocation}");
                Console.WriteLine("======================");
                UserManger.Instance.UpdateUserData();
                return selectedLocation;
            }

            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while setting the driver's current location:{ex}.Message");
                return null;
            }
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
        public static void AddHistoryForDriver(Driver driver, Passenger passenger, string pickup, string dropOff, double distance, double earnings)
        {
            var trip = new Ride(passenger, driver, pickup, dropOff)
            {
                Distance = distance,
                Cost = earnings
            };
            driver.TripHistory.Add(trip);
        }

    }
}
