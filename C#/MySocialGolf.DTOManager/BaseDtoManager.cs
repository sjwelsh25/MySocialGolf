using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySocialGolf.DtoManager
{
    public class BaseDtoManager
    {
        public SqlConnection BaseSqlConnection = null;

        public BaseDtoManager(SqlConnection sqlConn = null)
        {
            if (sqlConn == null)
            {
                sqlConn = new SqlConnection(DBConnectionManager.GetConnectionString());
            }
            BaseSqlConnection = sqlConn;
        }

    }
}
