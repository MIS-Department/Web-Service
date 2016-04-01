using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HR_Department.Models.Tables;

namespace OG_MFTG.DataLayer.Interfaces
{
    public interface IScheduleRepository : IDisposable
    {
        Task<IEnumerable<Schedule>> SelectAll();
        Task<Schedule> SelectById(int id);
        Task<int> Insert(Schedule model);
        Task Delete(int id);
        Task Update(Schedule model);
        Task<IEnumerable<Schedule>> SelectByName(string name);
    }
}
