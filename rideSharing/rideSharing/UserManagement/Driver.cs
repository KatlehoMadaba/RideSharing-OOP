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
        public Driver(string username, string email, string password, string car, string noPlate, string currentLocation) : base(username, password, email)
        {
            Car = car;
            NoPlate = noPlate;
            Role = "Driver";
            isAvailable = true;
            CurrentLocation = currentLocation;
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

        public void UpdateLocation()
        {

        }
        public void UpdateAvailablityStatus()
        {

        }
        public void AcceptARide()
        {

        }
        public void ViewYourEarnings()
        {

        }


    }
}

