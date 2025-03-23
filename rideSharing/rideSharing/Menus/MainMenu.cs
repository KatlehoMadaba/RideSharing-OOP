using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using rideSharing.Menus;
using rideSharing.RideRequestSystem;
using RideSharing;

namespace RideSharing.Menus
{

    public static class MainMenu
    {
        //The content displayed for each users main menu
        public static void DisplayMainMenu()
        {
            Console.WriteLine("1.Enter number 1 to register as Passanger:");
            Console.WriteLine("2.Enter number 2 to register as Driver:");
            Console.WriteLine("3.Login");
            Console.WriteLine("0.Exit");
        }
        public static void DisplayRegPassengerMenu(UserManger userManger)
        {
            Console.WriteLine("Please enter your username:");
            string username = Console.ReadLine();
            Console.WriteLine("Please your email:");
            string email = Console.ReadLine();
            Console.WriteLine("Please your password:");
            string password = Console.ReadLine();
            Console.WriteLine("Please enter how much you will be adding to your account:");
            double intialBalance=Convert.ToDouble(Console.ReadLine());
            userManger.registerPassenger(username, email, password, intialBalance);
        }

        public static void DisplayRegDriverMenu(UserManger userManger)
        {
            Console.WriteLine("Please enter your username");
            string username = Console.ReadLine();
            Console.WriteLine("Please your email:");
            string email = Console.ReadLine();
            Console.WriteLine("Please your password:");
            string password = Console.ReadLine();
            Console.WriteLine("Please enter name of car");
            string car = Console.ReadLine();
            Console.WriteLine("Please enter number plate");
            string noPlate = Console.ReadLine();
            Console.Write("Please choose your current location:");
            var tempdriver = new Driver(username, email, password, car, noPlate,string.Empty);
            string currentLocation = RideSystem.DriversCurrentLocation(tempdriver);
            userManger.registerDriver(username, email, password, car, noPlate,currentLocation);
        }

        public static void DisplayLoginMenu(UserManger userManger)
        {
            Console.WriteLine("Enter username:");
            string username = Console.ReadLine();
            Console.WriteLine("Enter password:");
            string password = Console.ReadLine();
            var user = userManger.Login(username, password);
            if (user != null)
            {
                Console.WriteLine($"Welcome {username}");
                user.DisplayMenu();
            }
            else
            {
                Console.WriteLine("Invalid credintials.Please try again");
            }
        }

    }
}
