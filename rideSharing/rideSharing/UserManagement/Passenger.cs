using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RideSharing
{
    public class Passenger:User
    {
        public Passenger(string username, string email, string password):base(username,password,email) 
        {
            Role = "Passanger";

        }
    }
}
