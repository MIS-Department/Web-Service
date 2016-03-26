using System.Collections.Generic;
using System.Threading.Tasks;

namespace OG_MFTG.DataLayer
{
    public interface IIO<T>
    {
        Task<IEnumerable<T>> SelectAll();
        Task<IEnumerable<T>> SelectById(int id);
        Task Insert(T t);
        Task Delete(int id);
        Task Update(T t);
    }
}
