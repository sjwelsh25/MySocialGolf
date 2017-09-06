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

        public IEnumerable<ExternalLoginsDto> ListExternalLogins(int userId)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("@UserId", userId);
            IEnumerable<ExternalLoginsDto> result = BaseSqlConnection.Query<ExternalLoginsDto>("ExternalLoginsList", p, commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }
    }
}

