using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheHomingPigeon.Data.Interface
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserResponse>> GetUsers();

        Task<bool> InsertUser(User user);
    }
}
