using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using rideSharing.Menus;
using rideSharing.RideRequestSystem;

namespace RideSharing
{
    public class Driver : User
    {
        public Driver(string username, string email, string password, string car, string noPlate, string currentLocation) : base(username, password, email)
        {
            Car = car;
            NoPlate = noPlate;
            Role = "Driver";
            isAvailable = true;
            CurrentLocation = currentLocation;
            Earnings = 0;
        }
        public string Car { get; set; }
        public string NoPlate { get; set; }
        public bool isAvailable { get; set; }
        public List<int> Ratings { get; set; } = new List<int>();

        public Ride ActiveRide { get; set; }
        public string CurrentLocation { get; set; }
        public double Earnings { get; set; }
        public static void ViewDriverEarnings(Driver driver)
        {
            try
            {
                Console.WriteLine("===================================");
                Console.WriteLine($"Total earnings: {driver.Earnings:C}");
                Console.WriteLine("===================================");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occured while fetching your earnings : {ex.Message}");
            }
        }

        public override void DisplayMenu()
        {
            DriverMenu.DriverMainMenu(this);

        }
        public void UpdateLocation(string newLocation)
        {
            if (!string.IsNullOrEmpty(newLocation))
            {
                CurrentLocation = newLocation;
                Console.WriteLine($"Location updated to: {CurrentLocation}");
            }
        }
        public void UpdateAvailablityStatus(bool availabilty)
        {
            isAvailable = availabilty;
        }
      
    }

}

