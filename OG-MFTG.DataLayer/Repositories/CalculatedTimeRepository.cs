using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using HR_Department.Models.Tables;
using OG_MFTG.DataLayer.Interfaces;
using OG_MFTG.DataLayer.Util;

namespace OG_MFTG.DataLayer.Repositories
{
    public class CalculatedTimeRepository : ICalculatedTime
    {
        private IDbConnection _connection;

        public async Task<IEnumerable<CalculatedTime>> SelectAll()
        {
            try
            {
                _connection = Connect.Open();

                return
                    await
                        _connection.QueryAsync<CalculatedTime>("CalculatedTimeAll",
                            commandType: CommandType.StoredProcedure);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<CalculatedTime> SelectById(int id)
        {
            try
            {
                _connection = Connect.Open();
                var p = new DynamicParameters();

                p.Add("@CalculatedTimeId", id);

                var result =
                    await
                        _connection.QueryAsync<CalculatedTime>("CalculatedTimeSelectById", p,
                            commandType: CommandType.StoredProcedure);

                return result.FirstOrDefault();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<int> Insert(CalculatedTime model)
        {
            try
            {
                _connection = Connect.Open();
                var p = new DynamicParameters();

                p.Add("@TimeTypeId", model.TimeTypeId);
                p.Add("@Value", model.Value);
                p.Add("@DailyTimeRecordId", model.DateTimeRecordId);
                p.Add("@CalculatedId", dbType: DbType.Int32, direction: ParameterDirection.Output);

                await _connection.ExecuteAsync("CalculatedTimeInsert", commandType: CommandType.StoredProcedure);
                return p.Get<int>("@CalculatedId");
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

                p.Add("@CalculatedTimeId", id);

                await _connection.ExecuteAsync("CalculatedTimeDelete", p, commandType: CommandType.StoredProcedure);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task Update(CalculatedTime model)
        {
            try
            {
                _connection = Connect.Open();
                var p = new DynamicParameters();

                p.Add("@CalculatedTimeId", model.CalculatedTimeId);
                p.Add("@TimeTypeId", model.TimeTypeId);
                p.Add("@Value", model.Value);
                p.Add("@DailyTimeRecordId", model.DateTimeRecordId);

                await _connection.ExecuteAsync("CalculatedTimeUpdate", p, commandType: CommandType.StoredProcedure);
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
