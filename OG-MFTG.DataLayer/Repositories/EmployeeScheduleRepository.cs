using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using HR_Department.Models.Tables;

namespace OG_MFTG.DataLayer.Repositories
{
    public class EmployeeScheduleRepository : IIO<EmployeeSchedule>
    {
        public async Task<IEnumerable<EmployeeSchedule>> SelectAll()
        {
            try
            {
                var connection = new SqlConnection(ConfigurationSettings.GetConnectionString());
                return
                    await
                        connection.QueryAsync<EmployeeSchedule>("EmployeeScheduleAll",
                            commandType: CommandType.StoredProcedure);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<EmployeeSchedule>> SelectById(int id)
        {
            try
            {
                var connection = new SqlConnection(ConfigurationSettings.GetConnectionString());
                var p = new DynamicParameters();

                p.Add("@EmployeeScheduleId", id);

                return
                    await
                        connection.QueryAsync<EmployeeSchedule>("EmployeeScheduleSelectById", p,
                            commandType: CommandType.StoredProcedure);
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
                var connection = new SqlConnection(ConfigurationSettings.GetConnectionString());
                var p = new DynamicParameters();

                p.Add("@EmployeeId", model.EmployeeId);
                p.Add("@ScheduleId", model.ScheduleId);
                p.Add("@Date", model.Date);
                p.Add("@EmployeeScheduleId", dbType: DbType.Int32, direction: ParameterDirection.Output);

                await connection.ExecuteAsync("EmployeeScheduleInsert", p, commandType: CommandType.StoredProcedure);

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
                var connection = new SqlConnection(ConfigurationSettings.GetConnectionString());
                var p = new DynamicParameters();

                p.Add("@EmployeeScheduleId", id);

                await connection.ExecuteAsync("EmployeeScheduleDelete", p, commandType: CommandType.StoredProcedure);
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
                var connection = new SqlConnection(ConfigurationSettings.GetConnectionString());
                var p = new DynamicParameters();

                p.Add("@EmployeeScheduleId", model.EmployeeScheduleId);
                p.Add("@EmployeeId", model.EmployeeId);
                p.Add("@ScheduleId", model.ScheduleId);
                p.Add("@Date", model.Date);

                await connection.ExecuteAsync("EmployeeScheduleUpdate", p, commandType: CommandType.StoredProcedure);
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
                var connection = new SqlConnection(ConfigurationSettings.GetConnectionString());
                var p = new DynamicParameters();

                p.Add("@StartDate", startTime);
                p.Add("@EndTime", endTime);

                return
                    await
                        connection.QueryAsync<EmployeeSchedule>("EmployeeScheduleSelectByDate", p,
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
                var connection = new SqlConnection(ConfigurationSettings.GetConnectionString());
                var p = new DynamicParameters();

                p.Add("@ScheduleId", scheduleId);

                return
                    await
                        connection.QueryAsync<EmployeeSchedule>("EmployeeScheduleSelectByScheduleId", p,
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
                var connection = new SqlConnection(ConfigurationSettings.GetConnectionString());
                var p = new DynamicParameters();

                p.Add("@EmployeeId");
                return
                    await
                        connection.QueryAsync<EmployeeSchedule>("EmployeeScheduleSelectByEmployee", p,
                            commandType: CommandType.StoredProcedure);
            }
            catch (Exception)
            {
                
                throw;
            }    
        } 
    }
}
