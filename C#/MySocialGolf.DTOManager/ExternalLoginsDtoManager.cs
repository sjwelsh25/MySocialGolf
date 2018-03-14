using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using Dapper;
using MySocialGolf.DtoModel;
using MySocialGolf.Model;

namespace MySocialGolf.DtoManager
{
    public class ExternalLoginsDtoManager: BaseDtoManager
    {

        public ExternalLoginsDtoManager(SqlConnection sqlConn = null): base(sqlConn)
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

