using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using HR_Department.Models.Tables;
using OG_MFTG.DataLayer.Interfaces;

namespace OG_MFTG.DataLayer.Repositories
{
    public class DailyTimeRecordRepository : IDailyTimeRecord
    {
        private IDbConnection _connection;

        public async Task<IEnumerable<DailyTimeRecord>> SelectAll()
        {
            try
            {
                _connection = Connect.Open();

                return
                    await
                        _connection.QueryAsync<DailyTimeRecord>("DailyTimeRecordAll",
                            commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<DailyTimeRecord> SelectById(int id)
        {
            try
            {
                _connection = Connect.Open();
                var p = new DynamicParameters();

                p.Add("@DailyTimeRecordId", id);

                var result =
                    await
                        _connection.QueryAsync<DailyTimeRecord>("DailyTimeRecordSelectById", p,
                            commandType: CommandType.StoredProcedure);
                return result.FirstOrDefault();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<int> Insert(DailyTimeRecord model)
        {
            try
            {
                _connection = Connect.Open();
                var p = new DynamicParameters();

                p.Add("@EmployeeId", model.EmployeeId);
                p.Add("@TimeCategoryId", model.TimeCategoryId);
                p.Add("@DateCreated", model.DateCreated);
                p.Add("@Time", model.Time);
                p.Add("@DailyTimeRecordId", dbType: DbType.Int32, direction: ParameterDirection.Output);

                await _connection.ExecuteAsync("DailyTimeRecordInsert", p, commandType: CommandType.StoredProcedure);

                return p.Get<int>("@DailyTimeRecordId");
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

                p.Add("@DailyTimeRecordId", id);
                await _connection.ExecuteAsync("DailyTimeRecordDelete", p, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task Update(DailyTimeRecord model)
        {
            try
            {
                _connection = Connect.Open();
                var p = new DynamicParameters();

                p.Add("@DailyTimeRecordId", model.DailyTimeRecordId);
                p.Add("@EmployeeId", model.EmployeeId);
                p.Add("@TimeCategoryId", model.TimeCategoryId);
                p.Add("@DateCreated", model.DateCreated);
                p.Add("@Time", model.Time);
                await _connection.ExecuteAsync("DailyTimeRecordUpdate", p, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<IEnumerable<DailyTimeRecord>> SelectByEmployeeId(int employeeId)
        {
            try
            {
                _connection = Connect.Open();
                var p = new DynamicParameters();

                p.Add("@EmployeeId", employeeId);
                return
                    await
                        _connection.QueryAsync<DailyTimeRecord>("DailyTimeRecordSelectByEmployeeId", p,
                            commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Employee>> SelectByEmployeeNumber(string number)
        {
            try
            {
                _connection = Connect.Open();
                var p = new DynamicParameters();

                p.Add("@EmployeeNumber", number);

                return
                    await
                        _connection.QueryAsync<Employee>("DailyTimeRecordEmployeeNumber", p,
                            commandType: CommandType.StoredProcedure);
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
