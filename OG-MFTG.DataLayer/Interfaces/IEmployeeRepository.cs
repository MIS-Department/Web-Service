using System.Collections.Generic;
using System.Threading.Tasks;
using HR_Department.Models.Tables;

namespace OG_MFTG.DataLayer.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> SelectAll();
        Task<Employee> SelectById(int? id);
        Task<Employee> SelectbyEmployeeNumber(string employeeNumber);
    }
}
