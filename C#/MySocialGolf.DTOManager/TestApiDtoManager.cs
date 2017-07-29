using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using MySocialGolf.DtoModel;
using MySocialGolf.Model;

namespace MySocialGolf.DtoManager
{
    public class TestApiDtoManager: BaseDtoManager
    {
        public TestApiDtoManager(SqlConnection sqlConn = null) : base(sqlConn)
        {
        }

        public IEnumerable<TestApiDto> ListTestApi(int testAPIID = 0)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("@TestApiId", testAPIID);
            IEnumerable<TestApiDto> result = BaseSqlConnection.Query<TestApiDto>("TestAPIList", p, commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public bool AddTestApi(TestApiDto taDTO)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("@TestName", taDTO.TestName);
            p.Add("@HttpVerb", taDTO.HttpVerb);
            p.Add("@TestInputJson", taDTO.TestInputJson);
            p.Add("@HttpVerb", taDTO.HttpVerb);
            p.Add("@TestUrl", taDTO.TestUrl);
            p.Add("@ReturnType", taDTO.ReturnType);
            // output params
            p.Add("@SubmitMessage", dbType: DbType.String, size: 1000, direction: ParameterDirection.Output);
            p.Add("@NewTestApiId", dbType: DbType.Int32, direction: ParameterDirection.Output);
            BaseSqlConnection.Execute("TestAPIInsert", p, commandType: System.Data.CommandType.StoredProcedure);
            taDTO.TestApiId = p.Get<int>("@NewTestApiId");
            taDTO.SubmitMessage = p.Get<string>("@SubmitMessage");
            return true;
        }

        public bool AddTestAPILog(TestApiDto taDTO)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("@TestApiId", taDTO.TestApiId);
            p.Add("@Response", taDTO.ResponseToLog);
            p.Add("@StatusCode", taDTO.LastStatusCode);
            BaseSqlConnection.Execute("TestAPILogInsert", p, commandType: System.Data.CommandType.StoredProcedure);
            return true;
        }

        public bool UpdateTestApi(TestApiDto taDTO)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("@TestApiId", taDTO.TestApiId);
            p.Add("@TestName", taDTO.TestName);
            p.Add("@HttpVerb", taDTO.HttpVerb);
            p.Add("@TestInputJson", taDTO.TestInputJson);
            p.Add("@HttpVerb", taDTO.HttpVerb);
            p.Add("@TestUrl", taDTO.TestUrl);
            p.Add("@ReturnType", taDTO.ReturnType);
            // output params
            p.Add("@SubmitMessage", dbType: DbType.String, size: 1000, direction: ParameterDirection.Output);
            BaseSqlConnection.Execute("TestAPIUpdate", p, commandType: System.Data.CommandType.StoredProcedure);
            taDTO.SubmitMessage = p.Get<string>("@SubmitMessage");
            return true;
        }
    }
}
