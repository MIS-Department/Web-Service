﻿using System;
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
    public class DailyTimeRecordRepository : IDailyTimeRecordRepository
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

                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public async Task<DailyTimeRecord> SelectById(int? id)
        {
            try
            {
                _connection = Connect.Open();
                var p = new DynamicParameters();

                p.Add("@EmployeeId", id);

                var result =
                    await
                        _connection.QueryAsync<DailyTimeRecord>("DailyTimeRecordTimeCategory", p,
                            commandType: CommandType.StoredProcedure);
                return result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
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

                p.Add("@DailyTimeRecordId", id);
                await _connection.ExecuteAsync("DailyTimeRecordDelete", p, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                
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

                MessageBox.Show(ex.Message);
               
            }
        }

        public async Task<DailyTimeRecord> SelectByEmployeeId(int? employeeId)
        {
            try
            {
                _connection = Connect.Open();
                var p = new DynamicParameters();

                p.Add("@EmployeeId", employeeId);
                var result =
                    await
                        _connection.QueryAsync<DailyTimeRecord>("DailyTimeRecordSelectByEmployeeId", p,
                            commandType: CommandType.StoredProcedure);
                return result.FirstOrDefault();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                return null;
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
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public async Task<IEnumerable<DailyTimeRecord>> SelectEmplyeeIdDateCreated(int? id, DateTime startDate, DateTime endDate)
        {
            try
            {
                _connection = Connect.Open();
                var p = new DynamicParameters();

                p.Add("@EmployeeId", id);
                p.Add("@StartDate", startDate);
                p.Add("@EndDate", endDate);

                return
                    await
                        _connection.QueryAsync<DailyTimeRecord>("DailyTimeRecordSelectByEmployeeIdDateCreated", p,
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
                _connection.Dispose();
            }    
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
