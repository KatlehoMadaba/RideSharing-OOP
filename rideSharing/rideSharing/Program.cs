using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RideSharing
{
    class Program
    {
        static void Main(string[] args)
        {
            UserManger userManger = new UserManger();   
            string option = "";
            do
            {
                Console.WriteLine("==Welcome to ride sharing system===");
                Console.WriteLine("1.Enter number 1 to register as Passanger:");
                Console.WriteLine("2.Enter number 2 to register as Driver:");
                Console.WriteLine("3.Login");
                Console.WriteLine("0.Exit");
                option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        Console.WriteLine("Please enter your username:");
                        string pUserame = Console.ReadLine();
                        Console.WriteLine("Please your email:");
                        string pEmail = Console.ReadLine();
                        Console.WriteLine("Please your password:");
                        string pPassword = Console.ReadLine();
                        userManger.registerPassenger(pUserame, pEmail,pPassword);
                        break;
                    case "2":
                        Console.WriteLine("Please enter your username");
                        string dUserame = Console.ReadLine();
                        Console.WriteLine("Please your email:");
                        string dEmail = Console.ReadLine();
                        Console.WriteLine("Please your password:");
                        string dPassword = Console.ReadLine();
                        Console.WriteLine("Please enter name of car");
                        string car = Console.ReadLine();
                        Console.WriteLine("Please enter number plate");
                        string noPlate = Console.ReadLine();
                        userManger.registerDriver(dUserame, dEmail, dPassword,car,noPlate);
                        break;
                    case "3":
                        Console.WriteLine("Enter username:");
                        string lUsername= Console.ReadLine();
                        Console.WriteLine("Enter password:");
                        string lEmail = Console.ReadLine();
                        var user = userManger.Login(lUsername, lEmail);
                        if (user != null)
                        {
                            if (user is Passenger)
                            {
                                Console.WriteLine("Welcome Passanger.");
                                //Passenger menu
                            }
                            else if (user is Driver)
                            {
                                Console.WriteLine("Welcome Driver");
                                //Driver menu
                            }
                        }
                        break;
                    case "0":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Please enter a valid option");
                        break;
                }
            }
            while (option != "0");


        }
    }
}
