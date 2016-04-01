using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using HR_Department.Models.Tables;
using OG_MFTG.DataLayer.Interfaces;
using OG_MFTG.DataLayer.Util;

namespace OG_MFTG.DataLayer.Repositories
{
    public class EmployeeScheduleRepository : IEmployeeScheduleRepository
    {
        private IDbConnection _connection;

        public async Task<IEnumerable<EmployeeSchedule>> SelectAll()
        {
            try
            {
                _connection = Connect.Open();
                return
                    await
                        _connection.QueryAsync<EmployeeSchedule>("EmployeeScheduleAll",
                            commandType: CommandType.StoredProcedure);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<EmployeeSchedule> SelectById(int id)
        {
            try
            {
                _connection = Connect.Open();
                var p = new DynamicParameters();

                p.Add("@EmployeeScheduleId", id);

                var result =
                    await
                        _connection.QueryAsync<EmployeeSchedule>("EmployeeScheduleSelectById", p,
                            commandType: CommandType.StoredProcedure);

                return result.FirstOrDefault();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<int> Insert(EmployeeSchedule model)
        {
            try
            {
                _connection = Connect.Open();
                var p = new DynamicParameters();

                p.Add("@EmployeeId", model.EmployeeId);
                p.Add("@ScheduleId", model.ScheduleId);
                p.Add("@Date", model.Date);
                p.Add("@EmployeeScheduleId", dbType: DbType.Int32, direction: ParameterDirection.Output);

                await _connection.ExecuteAsync("EmployeeScheduleInsert", p, commandType: CommandType.StoredProcedure);

                return p.Get<int>("@EmployeeScheduleId");
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task Delete(int id)
        {
            try
            {
                _connection = Connect.Open();
                var p = new DynamicParameters();

                p.Add("@EmployeeScheduleId", id);

                await _connection.ExecuteAsync("EmployeeScheduleDelete", p, commandType: CommandType.StoredProcedure);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task Update(EmployeeSchedule model)
        {
            try
            {
                _connection = Connect.Open();
                var p = new DynamicParameters();

                p.Add("@EmployeeScheduleId", model.EmployeeScheduleId);
                p.Add("@EmployeeId", model.EmployeeId);
                p.Add("@ScheduleId", model.ScheduleId);
                p.Add("@Date", model.Date);

                await _connection.ExecuteAsync("EmployeeScheduleUpdate", p, commandType: CommandType.StoredProcedure);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<EmployeeSchedule>> SelectByDate(DateTime startTime, DateTime endTime)
        {
            try
            {
                _connection = Connect.Open();
                var p = new DynamicParameters();

                p.Add("@StartDate", startTime);
                p.Add("@EndTime", endTime);

                return
                    await
                        _connection.QueryAsync<EmployeeSchedule>("EmployeeScheduleSelectByDate", p,
                            commandType: CommandType.StoredProcedure);
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public async Task<IEnumerable<EmployeeSchedule>> SelectByScheduleId(int scheduleId)
        {
            try
            {
                _connection = Connect.Open();
                var p = new DynamicParameters();

                p.Add("@ScheduleId", scheduleId);

                return
                    await
                        _connection.QueryAsync<EmployeeSchedule>("EmployeeScheduleSelectByScheduleId", p,
                            commandType: CommandType.StoredProcedure);
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public async Task<IEnumerable<EmployeeSchedule>> SelectByEmployee(int employeeId)
        {
            try
            {
                _connection = Connect.Open();
                var p = new DynamicParameters();

                p.Add("@EmployeeId");
                return
                    await
                        _connection.QueryAsync<EmployeeSchedule>("EmployeeScheduleSelectByEmployee", p,
                            commandType: CommandType.StoredProcedure);
            }
            catch (Exception)
            {
                
                throw;
            }    
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                _connection?.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
