using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dapper;
using HR_Department.Models.Tables;
using OG_MFTG.DataLayer.Interfaces;
using OG_MFTG.DataLayer.Util;

namespace OG_MFTG.DataLayer.Repositories
{    
    public class TemplateRepository : ITemplateRepository
    {
        private IDbConnection _connection;

        public async Task<IEnumerable<Template>> SelectAll()
        {
            try
            {
                _connection = Connect.Open();
                return
                    await _connection.QueryAsync<Template>("TemplateSelectAll", commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;

            }
        }

        public async Task<Template> SelectById(int? id)
        {
            try
            {
                _connection = Connect.Open();
                var p = new DynamicParameters();

                p.Add("@TemplateId", id);
                var result =
                    await
                        _connection.QueryAsync<Template>("TemplateSelectById", p,
                            commandType: CommandType.StoredProcedure);

                return result.FirstOrDefault();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public async Task<int> Insert(Template model)
        {
            try
            {
                _connection = Connect.Open();
                var p = new DynamicParameters();

                p.Add("@TemplateCode", model.TemplateCode);
                p.Add("@Description", model.Description);
                p.Add("@StartTime", model.StartTime);
                p.Add("@EndTime", model.EndTime);
                p.Add("@TemplateId", dbType: DbType.Int32, direction: ParameterDirection.Output);

                await _connection.ExecuteAsync("TemplateInsert", p, commandType: CommandType.StoredProcedure);
                return p.Get<int>("@TemplateId");
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

                p.Add("@TemplateId", id);
                await _connection.ExecuteAsync("TemplateDelete", p, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                
            }
        }

        public async Task Update(Template model)
        {
            try
            {
                _connection = Connect.Open();
                var p = new DynamicParameters();

                p.Add("@TemplateId", model.TemplateId);
                p.Add("@TemplateCode", model.TemplateCode);
                p.Add("@Description", model.Description);
                p.Add("@StartTime", model.StartTime);
                p.Add("@EndTime", model.EndTime);

                await _connection.ExecuteAsync("TemplateUpdate", p, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
               
            }
        }

        public async Task<IEnumerable<Template>> SelectByDescription(string description)
        {
            try
            {
                _connection = Connect.Open();
                var p = new DynamicParameters();

                p.Add("@Description", description);

                return await
                    _connection.QueryAsync<Template>("TemplateSelectByDescription", p,
                        commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public async Task<IEnumerable<Template>> SelectByDate(DateTime startTime, DateTime endTime)
        {
            try
            {
                _connection = Connect.Open();
                var p = new DynamicParameters();

                p.Add("@StartTime", startTime);
                p.Add("@EndTime", endTime);

                return
                    await
                        _connection.QueryAsync<Template>("SelectByStartEnd", p, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                return null;
            }
        }

        //protected void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        _connection?.Dispose();
        //    }
        //}

        //public void Dispose()
        //{
        //    Dispose(true);
        //    GC.SuppressFinalize(this);
        //}
    }
}
