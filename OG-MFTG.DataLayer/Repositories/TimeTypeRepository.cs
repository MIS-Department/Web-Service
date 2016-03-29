using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using HR_Department.Models.Tables;

namespace OG_MFTG.DataLayer.Repositories
{
    public class TimeTypeRepository : IIO<TimeType>
    {
        public async Task<IEnumerable<TimeType>> SelectAll()
        {
            try
            {
                var connection = new SqlConnection(ConfigurationSettings.GetConnectionString());
                return
                    await connection.QueryAsync<TimeType>("TimeTypeSelectAll", commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                CreateLogFile err = new CreateLogFile();
                await err.ErrorLog(ex.Message,"error");
                throw;
            }
        }

        public async Task<IEnumerable<TimeType>> SelectById(int id)
        {
            try
            {
                var connection = new SqlConnection(ConfigurationSettings.GetConnectionString());
                var p = new DynamicParameters();

                p.Add("@TimetypeId", id);

                return await connection.QueryAsync<TimeType>("TimeTypeSelectById", p, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                CreateLogFile err = new CreateLogFile();
                await err.ErrorLog(ex.Message, "error");
                throw;
            }
        }

        public async Task<int> Insert(TimeType model)
        {
            try
            {
                var connection = new SqlConnection(ConfigurationSettings.GetConnectionString());
                var p = new DynamicParameters();

                p.Add("@Name", model.Name);
                p.Add("@TimeTypeId", dbType: DbType.Int32, direction: ParameterDirection.Output);

                await connection.ExecuteAsync("TimeTypeInsert", p, commandType: CommandType.StoredProcedure);

                return p.Get<int>("@TimeTypeId");

            }
            catch (Exception ex)
            {
                CreateLogFile err = new CreateLogFile();
                await err.ErrorLog(ex.Message, "error");
                throw;
            }             
        }

        public async Task Delete(int id)
        {
            try
            {
                var connection = new SqlConnection(ConfigurationSettings.GetConnectionString());
                var p = new DynamicParameters();

                p.Add("@TimeTypeId", id);
                await connection.ExecuteAsync("TimeTypeDelete", p, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                CreateLogFile err = new CreateLogFile();
                await err.ErrorLog(ex.Message, "error");
                throw;
            }
        }

        public async Task Update(TimeType model)
        {
            try
            {
                var connection = new SqlConnection(ConfigurationSettings.GetConnectionString());
                var p = new DynamicParameters();

                p.Add("@Name", model.Name);
                p.Add("@TimeTypeId", model.TimeTypeId);
                await connection.ExecuteAsync("TimeTypeUpdate", p, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                CreateLogFile err = new CreateLogFile();
                await err.ErrorLog(ex.Message, "error");
                throw;
            }
        }

        public async Task<IEnumerable<TimeType>> Search(string name)
        {
            try
            {
                var connection = new SqlConnection(ConfigurationSettings.GetConnectionString());
                var p = new DynamicParameters();

                p.Add("@Name", name);
                return await connection.QueryAsync<TimeType>("TimeTypeSelectByName", p, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                CreateLogFile err = new CreateLogFile();
                await err.ErrorLog(ex.Message, "error");
                throw;
            }
        } 
    }
}
