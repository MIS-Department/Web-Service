using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using HR_Department.Models.Tables;

namespace OG_MFTG.DataLayer.Repositories
{
    public class TemplateRepository : IIO<Template>
    {
        public async Task<IEnumerable<Template>> SelectAll()
        {
            try
            {
                var connection = new SqlConnection(ConfigurationSettings.GetConnectionString());
                return
                    await connection.QueryAsync<Template>("TemplateSelectAll", commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Template>> SelectById(int id)
        {
            try
            {
                var connection = new SqlConnection(ConfigurationSettings.GetConnectionString());
                var p = new DynamicParameters();

                p.Add("@TemplateId", id);
                return
                    await
                        connection.QueryAsync<Template>("TemplateSelectById", p,
                            commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<int> Insert(Template model)
        {
            try
            {
                var connection = new SqlConnection(ConfigurationSettings.GetConnectionString());
                var p = new DynamicParameters();

                p.Add("@TemplateCode", model.TemplateCode);
                p.Add("@Description", model.Description);
                p.Add("@StartTime", model.StartTime);
                p.Add("@EndTime", model.EndTime);
                p.Add("@TemplateId", dbType: DbType.Int32, direction: ParameterDirection.Output);

                await connection.ExecuteAsync("TemplateInsert", p, commandType: CommandType.StoredProcedure);
                return p.Get<int>("@TemplateId");
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

                p.Add("@TemplateId", id);
                await connection.ExecuteAsync("TemplateDelete", p, commandType: CommandType.StoredProcedure);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task Update(Template model)
        {
            try
            {
                var connection = new SqlConnection(ConfigurationSettings.GetConnectionString());
                var p = new DynamicParameters();

                p.Add("@TemplateId", model.TemplateId);
                p.Add("@TemplateCode", model.TemplateCode);
                p.Add("@Description", model.Description);
                p.Add("@StartTime", model.StartTime);
                p.Add("@EndTime", model.EndTime);

                await connection.ExecuteAsync("TemplateUpdate", p, commandType: CommandType.StoredProcedure);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Template>> SelectByDescription(string description)
        {
            try
            {
                var connection = new SqlConnection(ConfigurationSettings.GetConnectionString());
                var p = new DynamicParameters();

                p.Add("@Description", description);

                return await
                    connection.QueryAsync<Template>("TemplateSelectByDescription", p,
                        commandType: CommandType.StoredProcedure);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Template>> SelectByDate(DateTime startTime, DateTime endTime)
        {
            try
            {
                var connection = new SqlConnection(ConfigurationSettings.GetConnectionString());
                var p = new DynamicParameters();

                p.Add("@StartTime", startTime);
                p.Add("@EndTime", endTime);

                return
                    await
                        connection.QueryAsync<Template>("SelectByStartEnd", p, commandType: CommandType.StoredProcedure);
            }
            catch (Exception)
            {
                
                throw;
            }
        } 
    }
}
