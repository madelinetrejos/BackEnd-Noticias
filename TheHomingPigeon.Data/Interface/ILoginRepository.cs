using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheHomingPigeon.Model;


namespace TheHomingPigeon.Data.Interface
{
    public interface ILoginRepository
    {
        Task<Login> ExistUser(string username);

        Task<bool> ValidatePassword(string password, string hash);
    }
}
