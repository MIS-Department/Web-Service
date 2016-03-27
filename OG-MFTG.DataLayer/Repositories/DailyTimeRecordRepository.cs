using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using HR_Department.Models.Tables;

namespace OG_MFTG.DataLayer.Repositories
{
    public class DailyTimeRecordRepository : IIO<DailyTimeRecord>
    {
        public async Task<IEnumerable<DailyTimeRecord>> SelectAll()
        {
            try
            {
                var connection = new SqlConnection(ConfigurationSettings.GetConnectionString());

                return
                    await
                        connection.QueryAsync<DailyTimeRecord>("DailyTimeRecordAll",
                            commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                
                throw;
            }
        }

        public async Task<IEnumerable<DailyTimeRecord>> SelectById(int id)
        {
            try
            {
                var connection = new SqlConnection(ConfigurationSettings.GetConnectionString());
                var p = new DynamicParameters();

                p.Add("@DailyTimeRecordId", id);
                return
                    await
                        connection.QueryAsync<DailyTimeRecord>("DailyTimeRecordSelectById", p,
                            commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                
                throw;
            }
        }

        public async Task Insert(DailyTimeRecord model)
        {
            try
            {
                var connection = new SqlConnection(ConfigurationSettings.GetConnectionString());
                var p = new DynamicParameters();

                p.Add("@EmployeeId", model.EmployeeId);
                p.Add("@TimeCategoryId", model.TimeCategoryId);
                p.Add("@DateCreated", model.DateCreated);
                p.Add("@Time", model.Time);
                await connection.ExecuteAsync("DailyTimeRecordInsert", p, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
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

                p.Add("@DailyTimeRecordId", id);
                await connection.ExecuteAsync("DailyTimeRecordDelete", p, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                
                throw;
            }
        }

        public async Task Update(DailyTimeRecord model)
        {
            try
            {
                var connection = new SqlConnection(ConfigurationSettings.GetConnectionString());
                var p = new DynamicParameters();

                p.Add("@DailyTimeRecordId", model.DailyTimeRecordId);
                p.Add("@EmployeeId", model.EmployeeId);
                p.Add("@TimeCategoryId", model.TimeCategoryId);
                p.Add("@DateCreated", model.DateCreated);
                p.Add("@Time", model.Time);
                await connection.ExecuteAsync("DailyTimeRecordUpdate", p, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                
                throw;
            }
        }

        public async Task<IEnumerable<DailyTimeRecord>> SelectByEmployeeId(int employeeId)
        {
            try
            {
                var connection = new SqlConnection(ConfigurationSettings.GetConnectionString());
                var p = new DynamicParameters();

                p.Add("@EmployeeId", employeeId);
                return
                    await
                        connection.QueryAsync<DailyTimeRecord>("DailyTimeRecordSelectByEmployeeId", p,
                            commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                
                throw;
            }
        } 
    }
}
