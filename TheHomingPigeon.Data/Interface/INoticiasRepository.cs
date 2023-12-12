using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheHomingPigeon.Model;

namespace TheHomingPigeon.Data.Interface
{
    public interface INoticiasRepository
    {
        Task<bool> InsertNews(Noticias noticias);
    }
}
