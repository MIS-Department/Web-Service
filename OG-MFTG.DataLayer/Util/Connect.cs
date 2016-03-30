using System.Data;
using System.Data.SqlClient;

namespace OG_MFTG.DataLayer.Util
{
    public static class Connect
    {
        public static IDbConnection Open()
        {
            var connection = new SqlConnection(ConfigurationSettings.GetConnectionString());
            connection.Open();
            return connection;
        }
    }
}
