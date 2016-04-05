using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HR_Department.Models.Tables;

namespace OG_MFTG.DataLayer.Interfaces
{
    public interface ITemplateScheduleRepository : IDisposable
    {
        Task<IEnumerable<TemplateSchedule>> SelectAll();
        Task<TemplateSchedule> SelectById(int? id);
        Task<int> Insert(TemplateSchedule model);
        Task Delete(int? id);
        Task Update(TemplateSchedule model);
        Task<TemplateSchedule> SelectByScheduleId(int? id);
    }
}
