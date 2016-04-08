using HR_Department.Models.Tables;

namespace OG_MFTG.Models.Interfaces
{
    public interface IEmployeeNotify
    {
        Employee Employee { get; set; }
        bool IsSuspended { get; set; }
        bool IsTimeCheck { get; set; }
    }
}
