using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using HR_Department.Models.Tables;

namespace OG_MFTG.DataLayer.Repositories
{
    public class ScheduleRepository : IIO<Schedule>
    {
        public async Task<IEnumerable<Schedule>> SelectAll()
        {
            try
            {
                var connection = new SqlConnection(ConfigurationSettings.GetConnectionString());
                return await connection.QueryAsync<Schedule>("ScheduleAll", commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                
                throw;
            }
        }

        public async Task<IEnumerable<Schedule>> SelectById(int id)
        {
            try
            {
                var connection = new SqlConnection(ConfigurationSettings.GetConnectionString());
                var p = new DynamicParameters();

                p.Add("@ScheduleId", id);
                return
                    await
                        connection.QueryAsync<Schedule>("ScheduleSelectById", p,
                            commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                
                throw;
            }
        }

        public async Task Insert(Schedule model)
        {
            try
            {
                var connection = new SqlConnection(ConfigurationSettings.GetConnectionString());
                var p = new DynamicParameters();

                p.Add("@Name", model.Name);
                await connection.ExecuteAsync("ScheduleInsert", p, commandType: CommandType.StoredProcedure);
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

                p.Add("@ScheduleId", id);
                await connection.ExecuteAsync("ScheduleDelete", p, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                
                throw;
            }
        }

        public async Task Update(Schedule model)
        {
            try
            {
                var connection = new SqlConnection(ConfigurationSettings.GetConnectionString());
                var p = new DynamicParameters();

                p.Add("@ScheduleId", model.ScheduleId);
                p.Add("@Name", model.Name);
                await connection.ExecuteAsync("ScheduleUpdate", p, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                
                throw;
            }
        }

        public async Task<IEnumerable<Schedule>> SelectByName(string name)
        {
            try
            {
                var connection = new SqlConnection(ConfigurationSettings.GetConnectionString());
                var p = new DynamicParameters();

                p.Add("@Name", name);
                return
                    await
                        connection.QueryAsync<Schedule>("ScheduleSelectByName", p,
                            commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                
                throw;
            }
        } 
    }
}
