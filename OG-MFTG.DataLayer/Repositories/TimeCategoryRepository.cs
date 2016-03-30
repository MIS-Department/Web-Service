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
    public class TimeCategoryRepository : ITimeCategory
    {
        private IDbConnection _connection;

        public async Task<IEnumerable<TimeCategory>> SelectAll()
        {
            try
            {
                _connection = Connect.Open();

                return
                    await
                        _connection.QueryAsync<TimeCategory>("TimeCategorySelectAll",
                            commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {

                throw;
            }
        }    

        public async Task<TimeCategory> SelectById(int id)
        {
            try
            {
                _connection = Connect.Open();
                var p = new DynamicParameters();

                p.Add("@TimeCategoryId", id);
                var result =
                    await
                        _connection.QueryAsync<TimeCategory>("TimeCategorySelectById", p,
                            commandType: CommandType.StoredProcedure);

                return result.FirstOrDefault();
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
                _connection = Connect.Open();
                var p = new DynamicParameters();

                p.Add("@TimeCategoryValue", model.TimeCategoryValue);
                p.Add("@TimeCategoryId", dbType: DbType.Int32, direction: ParameterDirection.Output);

                await _connection.ExecuteAsync("TimeCategoryInsert", p, commandType: CommandType.StoredProcedure);

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
                _connection = Connect.Open();
                var p = new DynamicParameters();

                p.Add("@TimeCategoryId", id);
                await _connection.ExecuteAsync("TimeCategoryDelete", p, commandType: CommandType.StoredProcedure);
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
                _connection = Connect.Open();
                var p = new DynamicParameters();

                p.Add("@TimeCategoryValue", model.TimeCategoryValue);
                p.Add("@TimeCategoryId", model.TimeCategoryId);
                await _connection.ExecuteAsync("TimeCategoryUpdate", p, commandType: CommandType.StoredProcedure);
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
                _connection = Connect.Open();
                var p = new DynamicParameters();

                p.Add("@Value", value);
                return await
                    _connection.QueryAsync<TimeCategory>("TimeCategorySelectByValue", p,
                        commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
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
