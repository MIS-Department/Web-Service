using System;
using System.Collections;
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
                IEnumerable<TimeType> error = new List<TimeType>();

                await err.ErrorLog(ex.Message,"error");
                return error;
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

        public async Task Insert(TimeType t)
        {
            try
            {
                var connection = new SqlConnection(ConfigurationSettings.GetConnectionString());
                var p = new DynamicParameters();

                p.Add("@Name", t.Name);
                await connection.ExecuteAsync("TimeTypeInsert", p, commandType: CommandType.StoredProcedure);

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

        public async Task Update(TimeType t)
        {
            try
            {
                var connection = new SqlConnection(ConfigurationSettings.GetConnectionString());
                var p = new DynamicParameters();

                p.Add("@Name", t.Name);
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
