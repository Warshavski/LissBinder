using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Escyug.LissBinder.Data.Entities;

namespace Escyug.LissBinder.Data.QueryProcessors
{
    public interface IAddBindingQueryProcessor
    {
        Task<bool> AddBindingAsync(Binding binding);
    }
}
