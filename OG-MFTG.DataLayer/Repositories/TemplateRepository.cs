using System.Collections.Generic;
using System.Threading.Tasks;
using HR_Department.Models.Tables;

namespace OG_MFTG.DataLayer.Repositories
{
    public class TemplateRepository : IIO<Template>
    {
        public Task<IEnumerable<Template>> SelectAll()
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Template>> SelectById(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task Insert(Template model)
        {
            throw new System.NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task Update(Template model)
        {
            throw new System.NotImplementedException();
        }
    }
}
