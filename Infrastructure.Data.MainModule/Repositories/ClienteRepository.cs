using Domain.MainModule.Entities;
using Infrastructure.Data.Contracts;
using Infrastructure.Data.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.MainModule.Repositories {
    public class ClienteRepository : Repository<Cliente, int>, IClienteRepository {
        static ClienteRepository() {
            store.Add(1, new Cliente() { Id = 1, RazonSocial = "Google", CIF = "N1234567D", eMail = "providers@gmail.com" });
            store.Add(2, new Cliente() { Id = 2, RazonSocial = "Microsoft", eMail = "providers@microsoft.com", Moroso = true });
            store.Add(3, new Cliente() { Id = 3, RazonSocial = "Apple", eMail = "providers@apple.com" });
        }
        public ClienteRepository() : base(nameof(Cliente.Id)) {

        }
    }
}
