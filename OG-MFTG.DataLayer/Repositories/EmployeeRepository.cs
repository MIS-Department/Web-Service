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
    public class EmployeeRepository : IEmployeeRepository
    {
        private IDbConnection _connection;

        public async Task<IEnumerable<Employee>> SelectAll()
        {
            try
            {
                _connection = Connect.Open();
                return
                    await
                        _connection.QueryAsync<Employee>("EmployeeSelectAll", commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                
                throw;
            }
            finally
            {
                _connection.Close();
            }
        }  
        public async Task<Employee> SelectById(int? id)
        {
            try
            {
                _connection = Connect.Open();
                var p = new DynamicParameters();

                p.Add("@EmployeeId", id);

                var result = await _connection.QueryAsync<Employee>("EmployeeSelectById", p,
                    commandType: CommandType.StoredProcedure);

                return result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                
                throw;
            }
            finally
            {
                _connection.Close();
            }
        }

        public async Task<Employee> SelectbyEmployeeNumber(string employeeNumber)
        {
            try
            {
                _connection = Connect.Open();
                var p = new DynamicParameters();

                p.Add("@EmployeeNumber", employeeNumber);

                var result =
                    await
                        _connection.QueryAsync<Employee>("EmployeeSelectByEmployeeNumber", p,
                            commandType: CommandType.StoredProcedure);

                return result.FirstOrDefault();

            }
            catch (Exception ex)
            {
                
                throw;
            }
            finally
            {
                _connection.Close();
            }
        }

        //protected void Dispose(bool diposing)
        //{
        //    if (diposing)
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
