using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using rideSharing.RideRequestSystem;

namespace RideSharing
{
    //Parent class of the users Driver and Passenger
    public abstract class User : IAuth
    {
        //not static so that all objects have its own instance of these properties
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public List<ITrip> TripHistory { get; private set; } = new List<ITrip>();

        public static List<User> userList = new List<User>();

        public User(string username, string password, string email)
        {
            Username = username;
            Password = password;
            Email = email;
        }

        public virtual bool Login(string username, string password)
        {
            return Username == username && Password == password;
        }

        public void AddTripToHistory(ITrip trip)
        {
            TripHistory.Add(trip);
            UserManger.Instance.UpdateUserData();
        }

        public void DisplayTripHistory()
        {
            if (TripHistory.Count == 0)
            {
                Console.WriteLine("No trip history available.");
                return;
            }

            Console.WriteLine("\nTrip History:");
            Console.WriteLine("====================================");
            foreach (var trip in TripHistory)
            {
                Console.WriteLine(trip);
            }
            Console.WriteLine("====================================");
        }

        public abstract void DisplayMenu();
    }
}
