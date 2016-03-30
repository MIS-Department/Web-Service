﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HR_Department.Models.Tables;

namespace OG_MFTG.DataLayer.Interfaces
{
    public interface IEmployeeSchedule : IDisposable
    {
        Task<IEnumerable<EmployeeSchedule>> SelectAll();
        Task<EmployeeSchedule> SelectById(int id);
        Task<int> Insert(EmployeeSchedule model);
        Task Delete(int id);
        Task Update(EmployeeSchedule model);
        Task<IEnumerable<EmployeeSchedule>> SelectByDate(DateTime startTime, DateTime endTime);
        Task<IEnumerable<EmployeeSchedule>> SelectByScheduleId(int id);
        Task<IEnumerable<EmployeeSchedule>> SelectByEmployee(int id);
    }
}
