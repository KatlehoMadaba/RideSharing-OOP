using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RideSharing
{
   //Parent class of the users Driver and Passenger
    public abstract class User : IAuth
    {
        public string Username { get; set; }

        public string Password { get; set; }
        
        public string Email { get; set; }
        public User(string username, string password, string email)
        {
            Username = username;
            Password = password;
            Email = email;
        }

        public virtual bool Login(string username, string password) 
        { 
            return Username==username && Password==password;
        }
    }
}
