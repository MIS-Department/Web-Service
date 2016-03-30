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
    public class TemplateScheduleRepository : ITemplateSchedule
    {
        private IDbConnection _connection;

        public async Task<IEnumerable<TemplateSchedule>> SelectAll()
        {
            try
            {
                _connection = Connect.Open();
                return
                    await
                        _connection.QueryAsync<TemplateSchedule>("TemplateScheduleAll",
                            commandType: CommandType.StoredProcedure);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<TemplateSchedule> SelectById(int id)
        {
            try
            {
                _connection = Connect.Open();
                var p = new DynamicParameters();

                p.Add("@TemplateScheduleId", id);

                var result =
                    await
                        _connection.QueryAsync<TemplateSchedule>("TemplateScheduleSelectByTemplateScheduleId", p,
                            commandType: CommandType.StoredProcedure);

                return result.FirstOrDefault();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<int> Insert(TemplateSchedule model)
        {
            try
            {
                _connection = Connect.Open();
                var p = new DynamicParameters();

                p.Add("@ScheduleId", model.ScheduleId);
                p.Add("@TemplateId", model.TemplateId);
                p.Add("@TemplateScheduleId", dbType: DbType.Int32, direction: ParameterDirection.Output);

                await _connection.ExecuteAsync("TemplateScheduleInsert", p, commandType: CommandType.StoredProcedure);
                return p.Get<int>("@TemplateScheduleId");
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

                p.Add("@TemplateScheduleId", id);

                await _connection.ExecuteAsync("TemplateScheduleDelete", p, commandType: CommandType.StoredProcedure);
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
                _connection = Connect.Open();
                var p = new DynamicParameters();

                p.Add("@TemplateScheduleId", model.TemplateScheduleId);
                p.Add("@ScheduleId", model.ScheduleId);
                p.Add("@TemplateId", model.TemplateId);

                await _connection.ExecuteAsync("TemplateScheduleUpdate", p, commandType: CommandType.StoredProcedure);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<TemplateSchedule> SelectByScheduleId(int scheduleId)
        {
            try
            {
                _connection = Connect.Open();
                var p = new DynamicParameters();

                p.Add("@ScheduleId", scheduleId);

                var result =
                    await
                        _connection.QueryAsync<TemplateSchedule>("TemplateScheduleSelectByScheduleId", p,
                            commandType: CommandType.StoredProcedure);
                return result.FirstOrDefault();
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
