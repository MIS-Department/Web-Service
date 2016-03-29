using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using HR_Department.Models.Tables;

namespace OG_MFTG.DataLayer.Repositories
{
    public class TimeCategoryRepository : IIO<TimeCategory>
    {
        public async Task<IEnumerable<TimeCategory>> SelectAll()
        {
            try
            {
                var connection = new SqlConnection(ConfigurationSettings.GetConnectionString());

                return
                    await
                        connection.QueryAsync<TimeCategory>("TimeCategorySelectAll",
                            commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<IEnumerable<TimeCategory>> SelectById(int id)
        {
            try
            {
                var connection = new SqlConnection(ConfigurationSettings.GetConnectionString());
                var p = new DynamicParameters();

                p.Add("@TimeCategoryId", id);
                return
                    await
                        connection.QueryAsync<TimeCategory>("TimeCategorySelectById", p,
                            commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<int> Insert(TimeCategory model)
        {
            try
            {
                var connection = new SqlConnection(ConfigurationSettings.GetConnectionString());
                var p = new DynamicParameters();

                p.Add("@TimeCategoryValue", model.TimeCategoryValue);
                p.Add("@TimeCategoryId", dbType: DbType.Int32, direction: ParameterDirection.Output);

                await connection.ExecuteAsync("TimeCategoryInsert", p, commandType: CommandType.StoredProcedure);

                return p.Get<int>("@TimeCategoryId");
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

                p.Add("@TimeCategoryId", id);
                await connection.ExecuteAsync("TimeCategoryDelete", p, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task Update(TimeCategory model)
        {
            try
            {
                var connection = new SqlConnection(ConfigurationSettings.GetConnectionString());
                var p = new DynamicParameters();

                p.Add("@TimeCategoryValue", model.TimeCategoryValue);
                p.Add("@TimeCategoryId", model.TimeCategoryId);
                await connection.ExecuteAsync("TimeCategoryUpdate", p, commandType: CommandType.StoredProcedure);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<TimeCategory>> SelectByValue(string value)
        {
            try
            {
                var connection = new SqlConnection(ConfigurationSettings.GetConnectionString());
                var p = new DynamicParameters();

                p.Add("@Value", value);
                return await
                    connection.QueryAsync<TimeCategory>("TimeCategorySelectByValue", p,
                        commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
