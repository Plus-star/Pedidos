using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using _4._2_Pedidos.Infraestructura.Data.Contracts.Repositories.Abstracciones;
using _4._2_Pedidos.Infraestructura.Data.Contracts.Repositorios.Abstracciones;

namespace _4._1_Pedidos.Infraestructura.Data.Repositorios
{
    // Generic in-memory repository used for development/testing when no DB connection is desired.
    public class InMemoryRepositorio<T> : IClientesRepositorio<T>, IPedidosRepositorio<T>, IDetallesRepositorio<T> where T : class, new()
    {
        private readonly ConcurrentDictionary<int, T> _store = new ConcurrentDictionary<int, T>();
        private readonly PropertyInfo _idProp;
        private int _currentId = 0;

        public InMemoryRepositorio()
        {
            // Try to find an id property like "id_xxx" of type int or int?
            _idProp = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .FirstOrDefault(p => (p.Name.StartsWith("id_", StringComparison.OrdinalIgnoreCase) || p.Name.Equals("Id", StringComparison.OrdinalIgnoreCase))
                                      && (p.PropertyType == typeof(int) || p.PropertyType == typeof(int?)));

            if (_idProp == null)
            {
                throw new InvalidOperationException($"Cannot find integer id property on type {typeof(T).FullName}. Expected property starting with 'id_' or 'Id'.");
            }
        }

        public Task<T> createAsync(T entity)
        {
            var currentVal = _idProp.GetValue(entity);
            int id = 0;
            if (currentVal is int i && i > 0)
            {
                id = i;
            }
            else if (currentVal is int?)
            {
                var val = (int?)currentVal;
                id = val.GetValueOrDefault();
            }

            if (id == 0)
            {
                id = Interlocked.Increment(ref _currentId);
                _idProp.SetValue(entity, id);
            }
            else
            {
                // ensure current id counter is at least id
                Interlocked.Exchange(ref _currentId, Math.Max(_currentId, id));
            }

            _store[id] = entity;
            return Task.FromResult(entity);
        }

        public Task<T> deleteAsync(int id)
        {
            if (_store.TryRemove(id, out var removed))
            {
                return Task.FromResult(removed);
            }
            return Task.FromResult<T>(null);
        }

        public Task<T> existAsync(int id)
        {
            return Task.FromResult(_store.TryGetValue(id, out var val) ? val : null);
        }

        public Task<List<T>> getAll()
        {
            return Task.FromResult(_store.Values.ToList());
        }

        public Task<T> getByIdAsync(params object[] keyvalues)
        {
            if (keyvalues == null || keyvalues.Length == 0) return Task.FromResult<T>(null);
            if (!(keyvalues[0] is int id))
            {
                try
                {
                    id = Convert.ToInt32(keyvalues[0]);
                }
                catch
                {
                    return Task.FromResult<T>(null);
                }
            }

            return Task.FromResult(_store.TryGetValue(id, out var val) ? val : null);
        }

        public Task<T> updateAsync(T entity)
        {
            var currentVal = _idProp.GetValue(entity);
            int id = 0;
            if (currentVal is int i && i > 0) id = i;
            else if (currentVal is int?) id = ((int?)currentVal).GetValueOrDefault();

            if (id == 0) return Task.FromResult<T>(null);

            _store[id] = entity;
            return Task.FromResult(entity);
        }
    }
}
