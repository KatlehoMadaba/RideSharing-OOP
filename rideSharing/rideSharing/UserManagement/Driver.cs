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
        public static void CompleteRide(Driver driver)
        {
            try
            {
                Console.WriteLine("Did you just complete a ride ? Type Yes OR NO");
                string status = Console.ReadLine()?.ToUpper().Trim();
                bool isComplete = false;
                while (!isComplete)
                {
                    switch (status)
                    {
                        case "YES":
                            driver.UpdateAvailablityStatus(true);
                            Console.WriteLine("Thank you for completing your ride");
                            isComplete = true;
                            break;
                        case "NO":
                            driver.UpdateAvailablityStatus(false);
                            Console.WriteLine("Your status has been updated to UNAVAILABLE");
                            isComplete = true;
                            break;
                        default:
                            Console.WriteLine("That is not a valid selection please type 'YES' OR 'NO'!");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"An error occured updating your completion status :{ex.Message}");
            }
        }
    }

}

