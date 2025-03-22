using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RideSharing
{
    public interface IAuth
    {
         bool Login(string username, string password); 
    }
}
