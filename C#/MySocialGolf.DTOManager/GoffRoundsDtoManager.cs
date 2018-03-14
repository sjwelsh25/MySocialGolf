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
    public class GolfRoundsDtoManager: BaseDtoManager
    {

        public GolfRoundsDtoManager(SqlConnection sqlConn = null): base(sqlConn)
        {
        }

        public GolfRoundDataModel GetGolfRound(int golfRoundId)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("@GolfRoundID", golfRoundId);
            return BaseSqlConnection.Query<GolfRoundDataModel>("GolfRoundList", p, commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();
        }

        public IEnumerable<GolfRoundDataModel> ListGolfRoundForUser(int userId, int golfRoundId = 0)
        {
            DynamicParameters p = new DynamicParameters();
            // p.Add("@Surname", us.Surname);
            p.Add("@UserId", userId);
            p.Add("@GolfRoundID", golfRoundId);
            return BaseSqlConnection.Query<GolfRoundDataModel>("GolfRoundList", p, commandType: System.Data.CommandType.StoredProcedure);
        }

        public bool UpdateGolfRound(GolfRoundDataModel round)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("@UserId", round.UserId);
            p.Add("@GolfCourseName", round.GolfCourseName);
            p.Add("@GolfRoundDate", round.GolfRoundDate);
            p.Add("@HandicapThatDay", round.HandicapThatDay);
            p.Add("@StableFordScore", round.StableFordScore);
            p.Add("@StrokeScore", round.StrokeScore);
            p.Add("@CourseParThatDay", round.CourseParThatDay);

            p.Add("@NewGolfRoundId", dbType: DbType.Int32, direction: ParameterDirection.Output);
            p.Add("@SubmitMessage", dbType: DbType.String, size: 1000, direction: ParameterDirection.Output);
            BaseSqlConnection.Execute("GolfRoundInsert", p, commandType: System.Data.CommandType.StoredProcedure);
            round.SubmitMessage = p.Get<string>("@SubmitMessage");
            round.GolfRoundId = p.Get<int>("@NewGolfRoundId");
            return true;
        }

        public bool AddGolfRound(GolfRoundDataModel round)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("@UserId", round.UserId);
            p.Add("@GolfCourseName", round.GolfCourseName);
            p.Add("@GolfRoundDate", round.GolfRoundDate);
            p.Add("@HandicapThatDay", round.HandicapThatDay);
            p.Add("@StableFordScore", round.StableFordScore);
            p.Add("@StrokeScore", round.StrokeScore);
            p.Add("@CourseParThatDay", round.CourseParThatDay);

            p.Add("@NewGolfRoundId", dbType: DbType.Int32, direction: ParameterDirection.Output);
            p.Add("@SubmitMessage", dbType: DbType.String, size: 1000, direction: ParameterDirection.Output);
            BaseSqlConnection.Execute("GolfRoundInsert", p, commandType: System.Data.CommandType.StoredProcedure);
            round.SubmitMessage = p.Get<string>("@SubmitMessage");
            round.GolfRoundId = p.Get<int>("@NewGolfRoundId");
            return true;
        }

        public bool DeleteGolfRound(GolfRoundDataModel round)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("@GolfRoundId", round.UserId);
            p.Add("@SubmitMessage", dbType: DbType.String, size: 1000, direction: ParameterDirection.Output);
            BaseSqlConnection.Execute("GolfRoundDelete", p, commandType: System.Data.CommandType.StoredProcedure);
            string msg = p.Get<string>("@SubmitMessage");
            return true;
        }
        
    }
}

