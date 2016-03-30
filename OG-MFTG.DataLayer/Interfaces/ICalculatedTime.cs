using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HR_Department.Models.Tables;

namespace OG_MFTG.DataLayer.Interfaces
{
    public interface ICalculatedTime : IDisposable
    {
        Task<IEnumerable<CalculatedTime>> SelectAll();
        Task<CalculatedTime> SelectById(int id);
        Task<int> Insert(CalculatedTime model);
        Task Delete(int id);
        Task Update(CalculatedTime model);
    }
}
