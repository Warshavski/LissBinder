using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Escyug.LissBinder.Data.Entities;

namespace Escyug.LissBinder.Data.QueryProcessors
{
    public interface IUserQueryProcessor : IQueryProcessor<User>
    {
        Task<User> SelectByNameAsync(string login);
        Task<User> SelectByIdAsync(int id);
    }
}
