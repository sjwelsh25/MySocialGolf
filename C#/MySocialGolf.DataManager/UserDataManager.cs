﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using Dapper;
using MySocialGolf.DataModel;
using MySocialGolf.Model;

namespace MySocialGolf.DataManager
{
    public class UserDataManager: BaseDataManager
    {

        public UserDataManager(SqlConnection sqlConn = null): base(sqlConn)
        {
        }

        #region User
        public UserDataModel GetUser(int id)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("@UserId", id);
            return BaseSqlConnection.Query<UserDataModel>("UserList", p, commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();
        }

        public UserDataModel GetUserByUserName(string userName)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("@UserName", userName);
            return BaseSqlConnection.Query<UserDataModel>("UserList", p, commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();
        }

        public IEnumerable<UserDataModel> ListUser(UserSearchModel us)
        {
            DynamicParameters p = new DynamicParameters();
            // p.Add("@Surname", us.Surname);
            IEnumerable<UserDataModel> result = BaseSqlConnection.Query<UserDataModel>("UserList", p, commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public bool AddUser(UserDataModel user)
        {
            if (user == null)
            {
                throw new Exception("AddUser failed > No UserDataModel provided");
            }

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

        public bool UpdateUser(UserDataModel user)
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

        #region User Registration
        public void RegisterNewUser(int id)
        {
            // write the email to the DB
        }
        #endregion User Registration 

        #region Golf Round
        public GolfRoundDataModel GetUserGolfRound(int userId)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("@UserID", userId);
            return BaseSqlConnection.Query<GolfRoundDataModel>("UserGolfRoundList", p, commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();
        }

        public IEnumerable<GolfRoundDataModel> ListUserGolfRound()
        {
            DynamicParameters p = new DynamicParameters();
            // p.Add("@Surname", us.Surname);
            return BaseSqlConnection.Query<GolfRoundDataModel>("UserGolfRoundList", p, commandType: System.Data.CommandType.StoredProcedure);
        }

        public bool SaveUserGolfRound(GolfRoundDataModel round)
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

        public bool DeleteUserGolfRound(GolfRoundDataModel round)
        {
            throw new NotImplementedException();
        }
        
        #endregion
    }
}

