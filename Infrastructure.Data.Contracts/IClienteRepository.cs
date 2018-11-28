using Domain.MainModule.Entities;
using Infrastructure.Data.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Contracts {
    public interface IClienteRepository: IRepository<Cliente, int> {
    }
}
