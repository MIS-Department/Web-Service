using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HR_Department.Models.Tables;

namespace OG_MFTG.DataLayer.Interfaces
{
    public interface ITemplateRepository : IDisposable
    {
        Task<IEnumerable<Template>> SelectAll();
        Task<Template> SelectById(int? id);
        Task<int> Insert(Template model);
        Task Delete(int? id);
        Task Update(Template model);
        Task<IEnumerable<Template>> SelectByDescription(string description);
        Task<IEnumerable<Template>> SelectByDate(DateTime startTime, DateTime endTime);
    }
}
