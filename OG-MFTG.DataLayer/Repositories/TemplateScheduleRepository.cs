using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using HR_Department.Models.Tables;

namespace OG_MFTG.DataLayer.Repositories
{
    public class TemplateScheduleRepository : IIO<TemplateSchedule>
    {
        public async Task<IEnumerable<TemplateSchedule>> SelectAll()
        {
            try
            {
                var connecstion = new SqlConnection(ConfigurationSettings.GetConnectionString());
                return
                    await
                        connecstion.QueryAsync<TemplateSchedule>("TemplateScheduleAll",
                            commandType: CommandType.StoredProcedure);
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public async Task<IEnumerable<TemplateSchedule>> SelectById(int id)
        {
            try
            {
                var connection = new SqlConnection(ConfigurationSettings.GetConnectionString());
                var p = new DynamicParameters();

                p.Add("@TemplateScheduleId", id);

                return
                    await
                        connection.QueryAsync<TemplateSchedule>("TemplateScheduleSelectByTemplateScheduleId", p,
                            commandType: CommandType.StoredProcedure);
            }
            catch (Exception)
            {
                
                throw;
            }  
        }

        public async Task Insert(TemplateSchedule model)
        {
            try
            {
                var connection = new SqlConnection(ConfigurationSettings.GetConnectionString());
                var p = new DynamicParameters();

                p.Add("@ScheduleId", model.ScheduleId);
                p.Add("@TemplateId", model.TemplateId);

                await connection.ExecuteAsync("TemplateScheduleInsert", p, commandType: CommandType.StoredProcedure);
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

                p.Add("@TemplateScheduleId", id);

                await connection.ExecuteAsync("TemplateScheduleDelete", p, commandType: CommandType.StoredProcedure);
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public async Task Update(TemplateSchedule model)
        {
            try
            {
                var connection = new SqlConnection(ConfigurationSettings.GetConnectionString());
                var p = new DynamicParameters();

                p.Add("@TemplateScheduleId", model.TemplateScheduleId);
                p.Add("@ScheduleId", model.ScheduleId);
                p.Add("@TemplateId", model.TemplateId);

                await connection.ExecuteAsync("TemplateScheduleUpdate", p, commandType: CommandType.StoredProcedure);
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public async Task<IEnumerable<TemplateSchedule>> SelectByScheduleId(int scheduleId)
        {
            try
            {
                var connection = new SqlConnection(ConfigurationSettings.GetConnectionString());
                var p = new DynamicParameters();

                p.Add("@ScheduleId", scheduleId);

                return
                    await
                        connection.QueryAsync<TemplateSchedule>("TemplateScheduleSelectByScheduleId", p,
                            commandType: CommandType.StoredProcedure);
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}
