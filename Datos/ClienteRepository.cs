using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos {
    public class ClienteRepository {
        public IEnumerable<Cliente> GetAll() {
            return new Cliente[] {
                new Cliente() { Id = 1, RazonSocial = "Uno" },
                new Cliente() { Id = 2, RazonSocial = "Dos" },
            };
        }
        public Cliente GetById(int id) {
            return new Cliente() { Id = 1, RazonSocial = "Uno" };
        }
    }
}
