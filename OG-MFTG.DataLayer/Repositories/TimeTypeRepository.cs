using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Dapper;
using HR_Department.Models.Tables;
using OG_MFTG.DataLayer.Interfaces;
using OG_MFTG.DataLayer.Util;

namespace OG_MFTG.DataLayer.Repositories
{
    public class TimeTypeRepository : ITimeTypeRepository
    {
        private IDbConnection _connection;

        public async Task<IEnumerable<TimeType>> SelectAll()
        {
            try
            {
                _connection = Connect.Open();
                return
                    await _connection.QueryAsync<TimeType>("TimeTypeSelectAll", commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                CreateLogFile err = new CreateLogFile();
                await err.ErrorLog(ex.Message, "error");
                throw;
            }
        }

        public async Task<TimeType> SelectById(int id)
        {
            try
            {
                _connection = Connect.Open();
                var p = new DynamicParameters();

                p.Add("@TimetypeId", id);

                var result =
                    await
                        _connection.QueryAsync<TimeType>("TimeTypeSelectById", p,
                            commandType: CommandType.StoredProcedure);

                return result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                CreateLogFile err = new CreateLogFile();
                await err.ErrorLog(ex.Message, "error");
                throw;
            }
        }

        public async Task<int> Insert(TimeType model)
        {
            try
            {
                _connection = Connect.Open();
                var p = new DynamicParameters();

                p.Add("@Name", model.Name);
                p.Add("@TimeTypeId", dbType: DbType.Int32, direction: ParameterDirection.Output);

                await _connection.ExecuteAsync("TimeTypeInsert", p, commandType: CommandType.StoredProcedure);

                return p.Get<int>("@TimeTypeId");

            }
            catch (Exception ex)
            {
                CreateLogFile err = new CreateLogFile();
                await err.ErrorLog(ex.Message, "error");
                throw;
            }
        }

        public async Task Delete(int id)
        {
            try
            {
                _connection = Connect.Open();
                var p = new DynamicParameters();

                p.Add("@TimeTypeId", id);
                await _connection.ExecuteAsync("TimeTypeDelete", p, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                CreateLogFile err = new CreateLogFile();
                await err.ErrorLog(ex.Message, "error");
                throw;
            }
        }

        public async Task Update(TimeType model)
        {
            try
            {
                _connection = Connect.Open();
                var p = new DynamicParameters();

                p.Add("@Name", model.Name);
                p.Add("@TimeTypeId", model.TimeTypeId);
                await _connection.ExecuteAsync("TimeTypeUpdate", p, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                CreateLogFile err = new CreateLogFile();
                await err.ErrorLog(ex.Message, "error");
                throw;
            }
        }

        public async Task<IEnumerable<TimeType>> Search(string name)
        {
            try
            {
                _connection = Connect.Open();
                var p = new DynamicParameters();

                p.Add("@Name", name);
                return await _connection.QueryAsync<TimeType>("TimeTypeSelectByName", p, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                CreateLogFile err = new CreateLogFile();
                await err.ErrorLog(ex.Message, "error");
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
