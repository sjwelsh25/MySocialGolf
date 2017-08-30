using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Dapper;
using MySocialGolf.Model.Identity;
using System.Configuration;
using System.Data.SqlClient;
using MySocialGolf.DtoManager;
using MySocialGolf.DtoModel;
using MySocialGolf.Extensions;

namespace MySocialGolf.Manager.Identity
{
    public class UserStoreManager: IUserStore<MsgUser>, IUserLoginStore<MsgUser>, IUserPasswordStore<MsgUser>, IUserSecurityStampStore<MsgUser>
    {
        private readonly string connectionString;

        public void Dispose()
        {

        }

        #region IUserStore
        public virtual Task CreateAsync(MsgUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            return Task.Factory.StartNew(() => {
                var uDtomngr = new UserDtoManager();
                var uDto = new UserDto()
                {
                    UserName = user.UserName,
                    PasswordHash = user.PasswordHash,
                    SecurityStamp = user.SecurityStamp
                };
                uDtomngr.AddUser(uDto);
                user.UserId = uDto.UserId.ToString();
                //user.UserId = Guid.NewGuid();
                //using (SqlConnection connection = new SqlConnection(connectionString))
                //    connection.Execute("insert into Users(userId, UserName, PasswordHash, SecurityStamp) values(@userId, @userName, @passwordHash, @securityStamp)", user);
            });
        }

        public virtual Task DeleteAsync(MsgUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            return Task.Factory.StartNew(() =>
            {
                var uDtomngr = new UserDtoManager();
                uDtomngr.DeleteUser(user.UserId.IToInt());
                //using (SqlConnection connection = new SqlConnection(connectionString))
                //    connection.Execute("deete from Users where UserId = @userId", new { user.UserId });
            });
        }

        public virtual Task<MsgUser> FindByIdAsync(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                throw new ArgumentNullException("userId");

            //Guid parsedUserId;
            //if (!Guid.TryParse(userId, out parsedUserId))
            //    throw new ArgumentOutOfRangeException("userId", string.Format("'{0}' is not a valid GUID.", new { userId }));

            return Task.Factory.StartNew(() =>
            {
                var uDtomngr = new UserDtoManager();
                var uDto = uDtomngr.GetUser(userId.IToInt());
                var myUser = new MsgUser();

                myUser.UserId = uDto.UserId.ToString();
                myUser.UserName = uDto.UserName;
                myUser.PasswordHash = uDto.PasswordHash;
                myUser.SecurityStamp = uDto.SecurityStamp;
                return myUser;
                //using (SqlConnection connection = new SqlConnection(connectionString))
                //    return connection.Query<MsgUser>("select * from Users where UserId = @userId", new { userId = parsedUserId }).SingleOrDefault();
            });
        }

        public virtual Task<MsgUser> FindByNameAsync(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
                throw new ArgumentNullException("userName");

            return Task.Factory.StartNew(() =>
            {
                var uDtomngr = new UserDtoManager();
                var uDto = uDtomngr.GetUserByUserName(userName);

                if (uDto == null)
                    return null;

                return new MsgUser()
                {
                    UserId = uDto.UserId.ToString(),
                    UserName = uDto.UserName,
                    PasswordHash = uDto.PasswordHash,
                    SecurityStamp = uDto.SecurityStamp
                };

                //using (SqlConnection connection = new SqlConnection(connectionString))
                //    return connection.Query<MsgUser>("select * from Users where lower(userName) = lower(@userName)", new { userName }).SingleOrDefault();
            });
        }

        public virtual Task UpdateAsync(MsgUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            return Task.Factory.StartNew(() =>
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                    connection.Execute("update Users set UserName = @userName, PasswordHash = @passwordHash, SecurityStamp = @securityStamp where UserId = @userId", user);
            });
        }
        #endregion

        #region IUserLoginStore
        public virtual Task AddLoginAsync(MsgUser user, UserLoginInfo login)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            if (login == null)
                throw new ArgumentNullException("login");

            return Task.Factory.StartNew(() =>
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                    connection.Execute("insert into ExternalLogins(ExternalLoginId, UserId, LoginProvider, ProviderKey) values(@externalLoginId, @userId, @loginProvider, @providerKey)",
                        new { externalLoginId = Guid.NewGuid(), userId = user.UserId, loginProvider = login.LoginProvider, providerKey = login.ProviderKey });
            });
        }

        public virtual Task<MsgUser> FindAsync(UserLoginInfo login)
        {
            if (login == null)
                throw new ArgumentNullException("login");

            return Task.Factory.StartNew(() =>
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                    return connection.Query<MsgUser>("select u.* from Users u inner join ExternalLogins l on l.UserId = u.UserId where l.LoginProvider = @loginProvider and l.ProviderKey = @providerKey",
                        login).SingleOrDefault();
            });
        }

        public virtual Task<IList<UserLoginInfo>> GetLoginsAsync(MsgUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            return Task.Factory.StartNew(() =>
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                    return (IList<UserLoginInfo>)connection.Query<UserLoginInfo>("select LoginProvider, ProviderKey from ExternalLogins where UserId = @userId", new { user.UserId }).ToList();
            });
        }

        public virtual Task RemoveLoginAsync(MsgUser user, UserLoginInfo login)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            if (login == null)
                throw new ArgumentNullException("login");

            return Task.Factory.StartNew(() =>
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                    connection.Execute("delete from ExternalLogins where UserId = @userId and LoginProvider = @loginProvider and ProviderKey = @providerKey",
                        new { user.UserId, login.LoginProvider, login.ProviderKey });
            });
        }
        #endregion

        #region IUserPasswordStore
        public virtual Task<string> GetPasswordHashAsync(MsgUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            return Task.FromResult(user.PasswordHash);
        }

        public virtual Task<bool> HasPasswordAsync(MsgUser user)
        {
            return Task.FromResult(!string.IsNullOrEmpty(user.PasswordHash));
        }

        public virtual Task SetPasswordHashAsync(MsgUser user, string passwordHash)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            user.PasswordHash = passwordHash;

            return Task.FromResult(0);
        }

        #endregion

        #region IUserSecurityStampStore
        public virtual Task<string> GetSecurityStampAsync(MsgUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            return Task.FromResult(user.SecurityStamp);
        }

        public virtual Task SetSecurityStampAsync(MsgUser user, string stamp)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            user.SecurityStamp = stamp;

            return Task.FromResult(0);
        }

        #endregion
    }
}
