using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RideSharing;
namespace rideSharing.Menus
{
    class DriverMenu
    {
        public static void DriverMainMenu(Driver driver)
        {
            string option;
            do
            {
                Console.WriteLine("Please pick an option:");
                Console.WriteLine("1.View available requests");
                Console.WriteLine("2.Accept a ride");
                Console.WriteLine("3.Compelete a ride");
                Console.WriteLine("4.View Earnings");
                Console.WriteLine("5.Update avaliablity status");
                Console.WriteLine("6.Update your current location");
                Console.WriteLine("7.View trip history");
                Console.WriteLine("0.Logout");
                option = Console.ReadLine();
                switch (option)
                {

                    case "1":
                        break;
                    case "2":
                        break;
                    case "3":
                        break;
                    case "4":
                        break;
                    case "5":
                        break;
                    case "6":
                        RideRequestSystem.RideSystem.DriversCurrentLocation(driver);
                        break;
                    case "7":
                        RideRequestSystem.RideSystem.DisplayDriversHistory(driver);
                        break;
                    case "0":
                        break;
                    default:
                        Console.WriteLine("Please enter a valid answer!");
                        break;
                }
            }
            while (option != "0");
        }
    }
}
