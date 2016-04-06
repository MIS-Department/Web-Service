using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HR_Department.Models.Tables;

namespace OG_MFTG.DataLayer.Interfaces
{
    public interface IDailyTimeRecordRepository : IDisposable
    {
        Task<IEnumerable<DailyTimeRecord>> SelectAll();
        Task<DailyTimeRecord> SelectById(int? id);
        Task<int> Insert(DailyTimeRecord model);
        Task Delete(int? id);
        Task Update(DailyTimeRecord model);
        Task<DailyTimeRecord> SelectByEmployeeId(int? id);
        Task<IEnumerable<Employee>> SelectByEmployeeNumber(string number);
        Task<IEnumerable<DailyTimeRecord>> SelectEmplyeeIdDateCreated(int? id, DateTime startDate, DateTime endDate);
    }
}
