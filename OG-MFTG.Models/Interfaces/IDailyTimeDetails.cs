using System;

namespace OG_MFTG.Models.Interfaces
{
    public interface IDailyTimeDetails
    {
        //int DailyTimeRecordId { get; set; }
        DateTime DateCreated { get; set; }
        //int EmployeeId { get; set; }
        DateTime Time { get; set; }
        string TimeCategoryValue { get; set; }
    }
}
