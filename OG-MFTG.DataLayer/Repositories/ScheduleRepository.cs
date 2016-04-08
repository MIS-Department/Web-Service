using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dapper;
using HR_Department.Models.Tables;
using OG_MFTG.DataLayer.Interfaces;
using OG_MFTG.DataLayer.Util;

namespace OG_MFTG.DataLayer.Repositories
{
    public class ScheduleRepository : IScheduleRepository
    {
        private IDbConnection _connection;

        public async Task<IEnumerable<Schedule>> SelectAll()
        {
            try
            {
                _connection = Connect.Open();
                return await _connection.QueryAsync<Schedule>("ScheduleAll", commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public async Task<Schedule> SelectById(int? id)
        {
            try
            {
                _connection = Connect.Open();
                var p = new DynamicParameters();

                p.Add("@ScheduleId", id);
                var result =
                    await
                        _connection.QueryAsync<Schedule>("ScheduleSelectById", p,
                            commandType: CommandType.StoredProcedure);

                return result.FirstOrDefault();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public async Task<int> Insert(Schedule model)
        {
            try
            {
                _connection = Connect.Open();
                var p = new DynamicParameters();

                p.Add("@Name", model.Name);
                p.Add("@ScheduleId", dbType: DbType.Int32, direction: ParameterDirection.Output);

                await _connection.ExecuteAsync("ScheduleInsert", p, commandType: CommandType.StoredProcedure);

                return p.Get<int>("@ScheduleId");
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                return ex.HResult;
            }    
        }

        public async Task Delete(int? id)
        {
            try
            {
                _connection = Connect.Open();
                var p = new DynamicParameters();

                p.Add("@ScheduleId", id);
                await _connection.ExecuteAsync("ScheduleDelete", p, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
               
            }
        }

        public async Task Update(Schedule model)
        {
            try
            {
                _connection = Connect.Open();
                var p = new DynamicParameters();

                p.Add("@ScheduleId", model.ScheduleId);
                p.Add("@Name", model.Name);
                await _connection.ExecuteAsync("ScheduleUpdate", p, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
               
            }
        }

        public async Task<IEnumerable<Schedule>> SelectByName(string name)
        {
            try
            {
                _connection = Connect.Open();
                var p = new DynamicParameters();

                p.Add("@Name", name);
                return
                    await
                        _connection.QueryAsync<Schedule>("ScheduleSelectByName", p,
                            commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                return null;
            }
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                _connection.Close();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
