using System.Collections.Generic;
using HR_Department.Models.Tables;
using OG_MFTG.Models.Interfaces;

namespace OG_MFTG.Models.DTO
{
    public class EmployeeNotify : IEmployeeNotify
    {
        public Employee Employee { get; set; }
        public bool IsSuspended { get; set; }
        public bool IsTimeCheck { get; set; }
        public bool IsResign { get; set; }

        public IEnumerable<DailyTimeDetails> DailyTimeRecord { get; set; }
        public ErrorReturn Error { get; set; }
    }
}
