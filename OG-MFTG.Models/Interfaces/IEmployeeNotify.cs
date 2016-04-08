using HR_Department.Models.Tables;

namespace OG_MFTG.Models.Interfaces
{
    public interface IEmployeeNotify 
    {
        Employee Employee { get; set; }
        bool IsNotify { get; set; }
    }
}
