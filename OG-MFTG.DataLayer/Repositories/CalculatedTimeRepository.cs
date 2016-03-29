using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using HR_Department.Models.Tables;

namespace OG_MFTG.DataLayer.Repositories
{
    public class CalculatedTimeRepository : IIO<CalculatedTime>
    {
        public async Task<IEnumerable<CalculatedTime>> SelectAll()
        {
            try
            {
                var connection = new SqlConnection(ConfigurationSettings.GetConnectionString());

                return
                    await
                        connection.QueryAsync<CalculatedTime>("CalculatedTimeAll",
                            commandType: CommandType.StoredProcedure);
            }
            catch (Exception)
            {
                
                throw;
            }    
        }

        public async Task<IEnumerable<CalculatedTime>> SelectById(int id)
        {
            try
            {
                var connection = new SqlConnection(ConfigurationSettings.GetConnectionString());
                var p = new DynamicParameters();

                p.Add("@CalculatedTimeId", id);

                return
                    await
                        connection.QueryAsync<CalculatedTime>("CalculatedTimeSelectById", p,
                            commandType: CommandType.StoredProcedure);
            }
            catch (Exception)
            {
                
                throw;
            }    
        }

        public async Task Insert(CalculatedTime model)
        {
            try
            {
                var connection = new SqlConnection(ConfigurationSettings.GetConnectionString());
                var p = new DynamicParameters();

                p.Add("@TimeTypeId", model.TimeTypeId);
                p.Add("@Value", model.Value);
                p.Add("@DailyTimeRecordId", model.DateTimeRecordId);

                await connection.ExecuteAsync("CalculatedTimeInsert", commandType: CommandType.StoredProcedure);
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

                p.Add("@CalculatedTimeId", id);

                await connection.ExecuteAsync("CalculatedTimeDelete", p, commandType: CommandType.StoredProcedure);
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public async Task Update(CalculatedTime model)
        {
            try
            {
                var connection = new SqlConnection(ConfigurationSettings.GetConnectionString());
                var p = new DynamicParameters();

                p.Add("@CalculatedTimeId", model.CalculatedTimeId);
                p.Add("@TimeTypeId", model.TimeTypeId);
                p.Add("@Value", model.Value);
                p.Add("@DailyTimeRecordId", model.DateTimeRecordId);

                await connection.ExecuteAsync("CalculatedTimeUpdate", p, commandType: CommandType.StoredProcedure);
            }
            catch (Exception)
            {
                
                throw;
            }   
        }
    }
}
