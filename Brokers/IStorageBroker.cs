using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Waka.Brokers
{
    public interface IStorageBroker<T> where T : class
    {
        IEnumerable<T> GetAll();
        
        void Update(T entity, T entityUpdate);
        T Post(T entity);
        void Delete(T entity);
    }
}
