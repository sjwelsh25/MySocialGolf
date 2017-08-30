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
    public class UserDtoManager: BaseDtoManager
    {

        public UserDtoManager(SqlConnection sqlConn = null): base(sqlConn)
        {
        }

        #region User
        public UserDto GetUser(int id)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("@UserId", id);
            return BaseSqlConnection.Query<UserDto>("UserList", p, commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();
        }

        public UserDto GetUserByUserName(string userName)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("@UserName", userName);
            return BaseSqlConnection.Query<UserDto>("UserList", p, commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();
        }

        public IEnumerable<UserDto> ListUser(UserSearchModel us)
        {
            DynamicParameters p = new DynamicParameters();
            // p.Add("@Surname", us.Surname);
            IEnumerable<UserDto> result = BaseSqlConnection.Query<UserDto>("UserList", p, commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public bool AddUser(UserDto user)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("@UserName", user.UserName);
            p.Add("@PasswordHash", user.PasswordHash);
            p.Add("@SecurityStamp", user.SecurityStamp);
            p.Add("@Surname", user.Surname);
            p.Add("@FirstName", user.FirstName);
            p.Add("@Email", user.Email);
            p.Add("@Mobile", user.Mobile);
            p.Add("@CurrentHandicap", user.CurrentHandicap);
            // output params
            p.Add("@SubmitMessage", dbType: DbType.String, size: 1000, direction: ParameterDirection.Output);
            p.Add("@NewUserId", dbType: DbType.Int32, direction: ParameterDirection.Output);
            BaseSqlConnection.Execute("UserInsert", p, commandType: System.Data.CommandType.StoredProcedure);
            user.UserId = p.Get<int>("@NewUserId");
            user.SubmitMessage = p.Get<string>("@SubmitMessage");
            return true;
        }

        public string DeleteUser(int userId)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("@UserId", userId);
            // output params
            p.Add("@SubmitMessage", dbType: DbType.String, size: 1000, direction: ParameterDirection.Output);
            BaseSqlConnection.Execute("UserDelete", p, commandType: System.Data.CommandType.StoredProcedure);
            string submitMessage = p.Get<string>("@SubmitMessage");
            return submitMessage;
        }
        #endregion

        public bool UpdateUser(UserDto user)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("@Surname", user.Surname);
            p.Add("@FirstName", user.FirstName);
            p.Add("@Email", user.Email);
            p.Add("@Mobile", user.Mobile);
            p.Add("@CurrentHandicap", user.CurrentHandicap);
            p.Add("@UserId", user.UserId);
            // output params
            p.Add("@SubmitMessage", dbType: DbType.String, size: 1000, direction: ParameterDirection.Output);
            BaseSqlConnection.Execute("UserUpdate", p, commandType: System.Data.CommandType.StoredProcedure);
            user.SubmitMessage = p.Get<string>("@SubmitMessage");
            return true;
        }

        #region Golf Round
        public GolfRoundDto GetUserGolfRound(int userId)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("@UserID", userId);
            return BaseSqlConnection.Query<GolfRoundDto>("UserGolfRoundList", p, commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();
        }

        public IEnumerable<GolfRoundDto> ListUserGolfRound()
        {
            DynamicParameters p = new DynamicParameters();
            // p.Add("@Surname", us.Surname);
            return BaseSqlConnection.Query<GolfRoundDto>("UserGolfRoundList", p, commandType: System.Data.CommandType.StoredProcedure);
        }

        public bool SaveUserGolfRound(GolfRoundDto round)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("@UserID", round.UserId);
            p.Add("@GolfCourseName", round.GolfCourseName);
            p.Add("@GolfRoundDate", round.GolfRoundDate);
            p.Add("@HandicapThatDay", round.HandicapThatDay);
            p.Add("@StableFordScore", round.StableFordScore);
            p.Add("@StrokeScore", round.StrokeScore);
            p.Add("@CourseParThatDay", round.CourseParThatDay);

            p.Add("@NewUserGolfRoundID", dbType: DbType.Int32, direction: ParameterDirection.Output);
            p.Add("@SubmitMessage", dbType: DbType.String, size: 1000, direction: ParameterDirection.Output);
            BaseSqlConnection.Execute("UserGolfRoundInsert", p, commandType: System.Data.CommandType.StoredProcedure);
            round.SubmitMessage = p.Get<string>("@SubmitMessage");
            round.GolfRoundId = p.Get<int>("@NewGolfRoundId");
            return true;
        }

        public bool DeleteUserGolfRound(GolfRoundDto round)
        {
            throw new NotImplementedException();
        }
        
        #endregion
    }
}

