using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySocialGolf.DataManager
{
    public class BaseDataManager
    {
        public SqlConnection BaseSqlConnection = null;

        public BaseDataManager(SqlConnection sqlConn = null)
        {
            if (sqlConn == null)
            {
                sqlConn = new SqlConnection(DBConnectionManager.GetConnectionString());
            }
            BaseSqlConnection = sqlConn;
        }

    }
}
