using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Core.Contracts {
    public interface IRepository<T, K> {
        List<T> GetAll();
        List<T> Find(Func<T, bool> where);
        T GetById(K id);
        void Add(T item);
        void Modify(T item);
        void Delete(T item);
        void Delete(K id);
    }
}
