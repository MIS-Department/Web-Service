using System;
using OG_MFTG.Models.Interfaces;

namespace OG_MFTG.Models.DTO
{    
    public class DailyTimeDetails : IDailyTimeDetails
    {
        //public int DailyTimeRecordId { get; set; }
        public DateTime DateCreated { get; set; }
        //public int EmployeeId { get; set; }
        public DateTime Time { get; set; }
        public string TimeCategoryValue { get; set; }
    }
}
