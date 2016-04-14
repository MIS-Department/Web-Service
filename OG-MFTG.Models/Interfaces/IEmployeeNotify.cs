using System.Collections.Generic;
using HR_Department.Models.Tables;
using OG_MFTG.Models.DTO;

namespace OG_MFTG.Models.Interfaces
{
    public interface IEmployeeNotify : IReturnStatus
    {
        Employee Employee { get; set; }
        bool IsSuspended { get; set; }
        bool IsTimeCheck { get; set; }
        bool IsResign { get; set; }
        IEnumerable<DailyTimeDetails> DailyTimeRecord { get; set; }
    }
}
