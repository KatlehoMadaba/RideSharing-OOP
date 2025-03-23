using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace RideSharing
{
    //Management of the resources of and for both driver and passenger
    public class UserManger
    {
        private static readonly UserManger _instance = new UserManger();

        public static UserManger Instance => _instance;

        public static List<Driver> availableDriversList { get; private set; } = new List<Driver>();

        private const string FilePath = "users.json";

        public UserManger()
        {
            //Loading users from json file when program starts
            loadUsers();
        }
        public void saveUsers()
        {
            //User class is an abstract class so type handling is needed to to deserilize a list of an instance of a abstract class.
            var setting = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects
            };
            var jsonData = JsonConvert.SerializeObject(User.userList, setting);
            File.WriteAllText(FilePath, jsonData);
        }
        public void UpdateUserData()
        {
            saveUsers();
        }
        private void loadUsers()
        {
            if (File.Exists(FilePath))
            {
                var jsonData = File.ReadAllText(FilePath);
                var setting = new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Objects
                };
                //loading the userList with data from the file 
                User.userList = JsonConvert.DeserializeObject<List<User>>(jsonData, setting);
            }
        }

        public void registerPassenger(string username, string email, string password, double initialBalance)
        {

            //validating if user already exists in the list
            if (User.userList.Any(user => user.Username == username))
            {
                Console.WriteLine("Username already exists");
                return;
            }
            else
            {
                //adds user to the list 
                var passenger = new Passenger(username, email, password, initialBalance);
                User.userList.Add(passenger);
                //saves to the json
                UpdateUserData();
                Console.WriteLine("You have been registered!");
            }

        }
        public void registerDriver(string username, string email, string password, string car, string noPlate)
        {
            //validating if user already exists in the list
            if (User.userList.Any(user => user.Username == username))
            {
                Console.WriteLine("Username already exists");
                return;
            }
            else
            {
                //adds user as an object to the user list 
                var driver = new Driver(username, email, password, car, noPlate);
                User.userList.Add(driver);
                //updates the json
                UpdateUserData();
                Console.WriteLine("You have been registered!");
            }
        }
        public User Login(string username, string password)
        {
            //u.Login is used as a condition for a matching user 
            //You are iterating through a list of type User
            //And the function Login returns an object of type User 
            //Login in method called on each User object in the Users list and passing the username and password as arguments
            //The login method should return true if the username and password have a match for that user or false
            var user = User.userList.FirstOrDefault(u => u.Login(username, password));
            if (user != null)
            {
                return user;
            }
            else
            {
                Console.WriteLine("This user does not exist or this password is incorrect");
                return null;
            }
        }
        
        public static List<Driver> LoadAvaibleDrivers()
        {
            availableDriversList = User.userList.OfType<Driver>().Where(d => d.isAvailable).ToList();

            if (availableDriversList.Count > 0)
            {
                Console.WriteLine("There is drivers in the list");
            }
            else
            {
                Console.WriteLine("Drivers list is empty");
            }
            return availableDriversList;
        }
    }
}
