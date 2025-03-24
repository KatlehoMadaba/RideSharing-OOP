using System;
using System.Collections.Generic;
using System.Linq;
using rideSharing.Menus;
using rideSharing.RideRequestSystem;

namespace RideSharing
{
    public class Passenger : User 
    {
        public double WalletBalance { get; set; }

        public Passenger(string username, string email, string password, double initialBalance) 
            : base(username, password, email)
        {
            Role = "Passenger";
            WalletBalance = initialBalance;
        }
        public override void DisplayMenu()
        {
            PassengerMenu.PassengerMainMenu(this);
        }
        public void DisplayRideHistory()
        {
            if (TripHistory.Count == 0)
            {
                Console.WriteLine("\nNo trip history avaialble");
                return;
            }
            else
            {
                Console.WriteLine("====================================");
                Console.WriteLine("\nTrip History:");
                foreach (var trip in TripHistory)
                {
                    Console.WriteLine($" Picked Up by {trip.Driver.Username}| From {trip.PickUp} to {trip.DropOff} | Distance: {trip.Distance} km | Cost: {trip.Cost:C}");
                }
                // Total cost of all trips
                double totalCost = TripHistory.Sum(t => t.Cost);
                Console.WriteLine($"Total Amount Spent: {totalCost:C}");
                Console.WriteLine("====================================");
            }

        }
        public void AddFunds(double amount)
        {
            WalletBalance += amount;
            // Find and update the passenger in the user list (ensures persistence in UserManager)
            var passengerInList = userList.OfType<Passenger>().FirstOrDefault(u => u.Username == this.Username);
            if (passengerInList != null)
            {
                passengerInList.WalletBalance = WalletBalance;
            }
            Console.WriteLine($"{amount:C} added to your wallet. New Balance: {WalletBalance:C}");
            UserManger.Instance.UpdateUserData();
        }
        public  void RateDriver(Driver driver, int stars)
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
                    Console.WriteLine("================================================");
                    Console.WriteLine($"Thank you for rating your driver {stars} stars");
                    driver.Ratings.Add(stars);
                    UserManger.Instance.UpdateUserData();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while rating the driver: + {ex}.Message");
            }
        }
    }

}