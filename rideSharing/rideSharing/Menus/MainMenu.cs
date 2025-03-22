using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using rideSharing.Menus;
using RideSharing;

namespace RideSharing.Menus
{

    public static class MainMenu
    {
        
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
            userManger.registerPassenger(username, email, password);
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
            userManger.registerDriver(username, email, password, car, noPlate);
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
                PassengerMenu.PassengerMainMenu();
            }

            ////Console.WriteLine($"This is the user type:stored {user.ToString()}");
            //var userRole = userList.FirstOrDefault(u => u.Login(username, password));
            //if (user != null)
            //{
            //    if (user is Passenger)
            //    {
            //        Console.WriteLine("Welcome Passanger.");
            //        PassengerMenu.PassengerMainMenu();
            //        Console.WriteLine("Returned from the passsenger menu");

            //    }
            //    else if (user is Driver)
            //    {
            //        Console.WriteLine("Welcome Driver");
            //        //Driver menu
            //    }
            //}
        }

    }
}
