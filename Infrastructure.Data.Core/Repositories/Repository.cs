using Infrastructure.Data.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Core.Repositories {
    public abstract class Repository<T, K> : IRepository<T, K> {
        protected static Dictionary<K, T> store = new Dictionary<K, T>();
        private PropertyInfo primaryKey;

        public Repository(string primaryKey) {
            this.primaryKey = typeof(T).GetProperty(primaryKey);
        }

        public virtual List<T> GetAll() {
            return store.Values.ToList();
        }

        public virtual List<T> Find(Func<T, bool> where) {
            if (where == null)
                throw new ArgumentNullException();
            return store.Values.Where(where).ToList();
        }

        public virtual T GetById(K id) {
            if (!store.ContainsKey(id))
                throw new Exception("Not found.");
            return store[id];
        }

        public virtual void Add(T item) {
            if (item == null)
                throw new ArgumentNullException();
            K key = (K)primaryKey.GetValue(item);
            if (store.ContainsKey(key))
                throw new Exception("Duplicate key.");
            store.Add(key, item);
        }

        public virtual void Modify(T item) {
            if (item == null)
                throw new ArgumentNullException();
            K key = (K)primaryKey.GetValue(item);
            if (!store.ContainsKey(key))
                throw new Exception("Not found.");
            store[key] = item;
        }

        public virtual void Delete(K id) {
            if (!store.ContainsKey(id))
                throw new Exception("Not found.");
            store.Remove(id);
        }

        public virtual void Delete(T item) {
            if (item == null)
                throw new ArgumentNullException();
            Delete((K)primaryKey.GetValue(item));
        }
    }
}
