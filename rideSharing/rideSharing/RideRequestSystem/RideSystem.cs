using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;

using RideSharing;

namespace rideSharing.RideRequestSystem
{
    //  Any thing the system returns to the user whether driver or passenger
    public static class RideSystem
    {
        public static List<string> locations = Ride.ValidLocations;
        public static List<Ride> AvailableRides = new List<Ride>(); 
        public static bool UsernameValidation(string username)
        {
            if (User.userList.Any(user => user.Username == username))
            {
                Console.WriteLine("Username already exists registration failed please try again");
                return true;
            }
            return false;
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
                while (!isStatusUpdated)
                {
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
        public static void CompleteRide(Driver driver)
        {
            try
            {
                //Displaying active ride id to the driver
                Console.WriteLine($"The active ride id: {driver.ActiveRide.Id}");
                //Creating object of the active ride 
                var ObjActiveRide = AvailableRides.FirstOrDefault(p => p.Id == driver.ActiveRide.Id);
                //Having the earnings of the driver and trip history updated
                RideSystem.CalculateDriversEarnings(ObjActiveRide);
                ObjActiveRide.Status = "Complete";
                //Changing Availabilty of the dtiver
                driver.UpdateAvailablityStatus(true);
                driver.AddTripToHistory(ObjActiveRide);
                UserManger.Instance.UpdateUserData();
                Console.WriteLine("Thank you for completing your ride");
            }
            catch (Exception ex)
            {

                Console.WriteLine($"An error occured updating your completion status :{ex.Message}");
            }
        }
        public static void AcceptRide(Driver driver)
        {
            try
            {
                Console.WriteLine("==========================================================");
                Console.WriteLine("Accepting a ride request...");
                if (!driver.isAvailable)
                {
                    Console.WriteLine("You are currently unavailable to accept a ride request.");
                    return;
                }
                //filtering rides by the current location
                var nearbyRides = AvailableRides
                    .Where(ride => ride.PickUp.Equals(driver.CurrentLocation, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                if (nearbyRides.Count == 0)
                {
                    Console.WriteLine("==========================================================");
                    Console.WriteLine("No ride requests are available near your location.");
                    return;
                }

                // showing  available ride requests
                Console.WriteLine("==========================================================");
                Console.WriteLine("Select a ride request to accept:");
                //display the request that are near the driver
                for (int i = 0; i < nearbyRides.Count; i++)
                {
                    var ride = nearbyRides[i];
                    Console.WriteLine($"{i + 1}. Passenger: {ride.Passenger.Username}, From: {ride.PickUp} To: {ride.DropOff}, Distance: {ride.Distance} km, Cost: {ride.Cost:C}");
                }

                //Validation of the accepting or canceling the ride 
                Console.WriteLine("Enter the number of the ride to accept or type 0 to cancel:");
                int choice;
                while (!int.TryParse(Console.ReadLine(), out choice) || choice < 0 || choice > nearbyRides.Count)
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                }

                Console.WriteLine("==========================================================");
                if (choice == 0)
                {
                    Console.WriteLine("You have chosen not to accept any ride requests.");
                    return;
                }

                var selectedRide = nearbyRides[choice - 1];

                //Changing status of the ride 
                var rideObj = AvailableRides.FirstOrDefault(ride => ride.Id == selectedRide.Id);

                if (rideObj != null)
                {
                    driver.ActiveRide = rideObj;
                    rideObj.Status = "Accepted";
                }
                else
                {
                    Console.WriteLine("The ride is not there!");
                }
                //assigning driver to a ride 
                selectedRide.Driver = driver;
                //updating availablty of driver that accepted the request
                driver.UpdateAvailablityStatus(false);

                selectedRide.Passenger.AddTripToHistory(selectedRide);
                UserManger.Instance.UpdateUserData();

                Console.WriteLine($"Ride request from {selectedRide.Passenger.Username} accepted.");
                Console.WriteLine($"Pick-Up: {selectedRide.PickUp}, Drop-Off: {selectedRide.DropOff}, Distance: {selectedRide.Distance} km, Cost: {selectedRide.Cost:C}");

                //Having the ride removed from the avaiable rides 
                AvailableRides.Remove(selectedRide);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while accepting the ride request: {ex.Message}");
            }
        }
        public static void RequestRide(Passenger passenger)
        {
            // Get valid pick-up location
            string pickUp = GetValidLocation("Please choose your pick up location:", locations);
            // Get valid drop-off location and ensuring it is not the same as pick-up.
            string dropOff;
            do
            {
                dropOff = GetValidLocation("Please choose your drop-off location:", locations);

                if (pickUp.Equals(dropOff, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Sorry, your pick-up and drop-off locations cannot be the same. Please try again.");
                }
            } while (pickUp.Equals(dropOff, StringComparison.OrdinalIgnoreCase));


            int rideId = AvailableRides.Count + 1;
            var ride = new Ride(rideId, passenger, null, pickUp, dropOff);
            double tripCost = ride.CalculateTripCost();

            if (passenger.WalletBalance < tripCost)
            {
                Console.WriteLine($"Insufficient funds! Trip cost is {tripCost:C},wallet balance is {passenger.WalletBalance:C}.");
                Console.WriteLine("Please top up your wallet to proceed with the ride request.");
                return;
            }
            // Create a new Ride object and calculate trip amount
            // Deduct trip cost from wallet
            passenger.WalletBalance -= tripCost;

            //updating the list for rides the drivers can accept
            AvailableRides.Add(ride);
            Console.WriteLine("Your ride request has been created!");
            Console.WriteLine("===================================");

        }
        private static void CalculateDriversEarnings(Ride ride)
        {
            try
            {
                double earnings = ride.Cost * 0.5;
                ride.Driver.Earnings = earnings;
                var driver = User.userList.OfType<Driver>().ToList().FirstOrDefault(d => d.Username == ride.Driver.Username);
                if (driver != null)
                {
                    driver.Earnings += earnings;
                    Console.WriteLine($"Drivers {driver.Username}'s updated  totals earnings are: {driver.Earnings:C} ");
                }
                else
                {
                    Console.WriteLine($"Driver {ride.Driver.Username} you were not found oon the list for earnings.");
                }

            }
            catch (Exception ex)
            {

                Console.WriteLine($"An error occurred while calculating drivers earnings:{ex.Message}");
            }
        }
        public static string DriversCurrentLocation(Driver driver)
        {
            try
            {
                string selectedLocation = GetValidLocation("\nHere are locations to select from:", locations);

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
    }
}
