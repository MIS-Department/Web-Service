﻿using System.Collections.Generic;
using System.Threading.Tasks;
using HR_Department.Models.Tables;

namespace OG_MFTG.DataLayer.Interfaces
{
    public interface ITimeTypeRepository 
    {
        Task<IEnumerable<TimeType>> SelectAll();
        Task<TimeType> SelectById(int? id);
        Task<int> Insert(TimeType model);
        Task Delete(int? id);
        Task Update(TimeType model);
        Task<IEnumerable<TimeType>> Search(string name);
    }
}

