﻿using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using MySocialGolf.DataModel;
using MySocialGolf.Model;
using System.Linq;

namespace MySocialGolf.DataManager
{
    public class TestApiDataManager: BaseDataManager
    {
        public TestApiDataManager(SqlConnection sqlConn = null) : base(sqlConn)
        {
        }

        public IEnumerable<TestApiDataModel> ListTestApi(int testAPIID = 0)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("@TestApiId", testAPIID);
            IEnumerable<TestApiDataModel> result = BaseSqlConnection.Query<TestApiDataModel>("TestAPIList", p, commandType: CommandType.StoredProcedure);
            return result;
        }

        public TestApiDataModel GetTestApi(int testAPIID)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("@TestApiId", testAPIID);
            return BaseSqlConnection.Query<TestApiDataModel>("TestAPIList", p, commandType: CommandType.StoredProcedure).FirstOrDefault();
        }

        public bool AddTestApi(TestApiDataModel taDTO)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("@TestName", taDTO.TestName);
            p.Add("@HttpVerb", taDTO.HttpVerb);
            p.Add("@TestInputJson", taDTO.TestInputJson);
            p.Add("@HttpVerb", taDTO.HttpVerb);
            p.Add("@TestUrl", taDTO.TestUrl);
            p.Add("@ReturnType", taDTO.ReturnType);
            p.Add("@SortOrder", taDTO.SortOrder);
            // output params
            p.Add("@SubmitMessage", dbType: DbType.String, size: 1000, direction: ParameterDirection.Output);
            p.Add("@NewTestApiId", dbType: DbType.Int32, direction: ParameterDirection.Output);
            BaseSqlConnection.Execute("TestAPIInsert", p, commandType: System.Data.CommandType.StoredProcedure);
            taDTO.TestApiId = p.Get<int>("@NewTestApiId");
            taDTO.SubmitMessage = p.Get<string>("@SubmitMessage");
            return true;
        }

        public bool AddTestAPILog(TestApiDataModel taDTO)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("@TestApiId", taDTO.TestApiId);
            p.Add("@Response", taDTO.ResponseToLog);
            p.Add("@StatusCode", taDTO.LastStatusCode);
            BaseSqlConnection.Execute("TestAPILogInsert", p, commandType: System.Data.CommandType.StoredProcedure);
            return true;
        }

        public bool UpdateTestApi(TestApiDataModel taDTO)
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

        public string DeleteTestApi(int testApiId)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("@TestApiId", testApiId);
            // output params
            p.Add("@SubmitMessage", dbType: DbType.String, size: 1000, direction: ParameterDirection.Output);
            BaseSqlConnection.Execute("TestApiDelete", p, commandType: System.Data.CommandType.StoredProcedure);
            string submitMessage = p.Get<string>("@SubmitMessage");
            return submitMessage;
        }

        public bool CloneTestApi(int testApiId)
        {
            TestApiDataModel ta = GetTestApi(testApiId);
            ta.TestName += "Clone";
            ta.SortOrder++;
            AddTestApi(ta);
            return true;
        }
    }
}
