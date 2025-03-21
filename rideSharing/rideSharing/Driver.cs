using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RideSharing
{
    class Driver: User
    {
        public string Car { get; set; }

        public string NoPlate { get; set; }
        public Driver(string username,string email ,string password,string car,string noPlate) : base(username, password,email)
        {

            Car = car;
            NoPlate = noPlate;
        }


    }
}
