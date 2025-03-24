using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using rideSharing.RideRequestSystem;
using RideSharing;
namespace rideSharing.Menus
{
    public static class DriverMenu
    {
        public static void DriverMainMenu(Driver driver)
        {
            string option;
            do
            {
                Console.WriteLine("Please pick an option:");
                Console.WriteLine("1.Accept a ride");
                Console.WriteLine("2.Compelete a ride");
                Console.WriteLine("3.View Earnings");
                Console.WriteLine("4.Update avaliablity status");
                Console.WriteLine("5.Update your current location");
                Console.WriteLine("6.View trip history");
                Console.WriteLine("0.Logout");
                option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                        RideRequestSystem.RideSystem.AcceptRide(driver);
                        break;
                    case "2":
                        RideRequestSystem.RideSystem.CompleteRide(driver);
                        break;
                    case "3":
                        Console.WriteLine($"Your total earnings are:{driver.Earnings:C}");
                        break;
                    case "4":
                        RideRequestSystem.RideSystem.GetAvaliablityStatus(driver);
                        break;
                    case "5":
                        RideRequestSystem.RideSystem.DriversCurrentLocation(driver);
                        break;
                    case "6":
                        RideRequestSystem.RideSystem.DisplayDriversHistory(driver);
                        break;
                    case "0":
                        break;
                    default:
                        Console.WriteLine("Please enter a valid option between 0-6!");
                        break;
                }
            }
            while (option != "0");
        }    
    }


}
