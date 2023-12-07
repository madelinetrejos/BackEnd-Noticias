using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheHomingPigeon.Model
{
 
        public class User
        {

            public string username { get; set; }
            public string email { get; set; }
            public string password { get; set; }

        }

        public class UserResponse
        {
            public int iduser { get; set; }
            public string username { get; set; }
            public string email { get; set; }
        }
    
}
