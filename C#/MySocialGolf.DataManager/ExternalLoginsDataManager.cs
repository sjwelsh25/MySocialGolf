using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using Dapper;
using MySocialGolf.DataModel;
using MySocialGolf.Model;

namespace MySocialGolf.DataManager
{
    public class ExternalLoginsDataManager: BaseDataManager
    {

        public ExternalLoginsDataManager(SqlConnection sqlConn = null): base(sqlConn)
        {
        }

        public IEnumerable<ExternalLoginsDataModel> ListExternalLogins(int userId)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("@UserId", userId);
            IEnumerable<ExternalLoginsDataModel> result = BaseSqlConnection.Query<ExternalLoginsDataModel>("ExternalLoginsList", p, commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }
    }
}

