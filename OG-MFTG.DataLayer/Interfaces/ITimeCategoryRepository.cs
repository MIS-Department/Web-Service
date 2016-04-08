using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HR_Department.Models.Tables;

namespace OG_MFTG.DataLayer.Interfaces
{
    public interface ITimeCategoryRepository 
    {
        Task<IEnumerable<TimeCategory>> SelectAll();
        Task<TimeCategory> SelectById(int? id);
        Task<int> Insert(TimeCategory model);
        Task Delete(int? id);
        Task Update(TimeCategory model);
        Task<IEnumerable<TimeCategory>> SelectByValue(string value);
    }
}
