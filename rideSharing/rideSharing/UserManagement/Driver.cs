using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using rideSharing.Menus;

namespace RideSharing
{
    public class Driver : User
    {
        public string Car { get; set; }
        public string NoPlate { get; set; }
        public bool isAvailable { get; set; }
        public List<int> Ratings { get; set; } = new List<int>();
        public string CurrentLocation { get; set; }
        public double Earnings { get; set; }
        public Driver(string username, string email, string password, string car, string noPlate, string currentLocation) : base(username, password, email)
        {
            Car = car;
            NoPlate = noPlate;
            Role = "Driver";
            isAvailable = true;
            CurrentLocation = currentLocation;
            Earnings = 0;
        }
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
        public double GetAverageRating()
        {
            if (Ratings.Count == 0)
                return 0;
            return Ratings.Average();
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
        public void AcceptARide(string rideDetails)
        {
            if (!isAvailable)
            {
                Console.WriteLine($"Accpected ride:{rideDetails}");
                isAvailable = false;
            }
            else
            {
                Console.WriteLine("Driver is currrent unavailable");
            }
        }
    }
}

