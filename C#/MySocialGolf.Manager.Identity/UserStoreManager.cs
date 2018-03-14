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
    public class UserStoreManager: IUserStore<IdentityUser>, IUserLoginStore<IdentityUser>, IUserPasswordStore<IdentityUser>, IUserSecurityStampStore<IdentityUser>
    {
        private readonly string connectionString;

        public void Dispose()
        {

        }

        #region IUserStore
        public virtual Task CreateAsync(IdentityUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            return Task.Factory.StartNew(() => {
                var uDtomngr = new UserDtoManager();
                var uDto = new UserDataModel()
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

        public virtual Task DeleteAsync(IdentityUser user)
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

        public virtual Task<IdentityUser> FindByIdAsync(string userId)
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
                var myUser = new IdentityUser();

                myUser.UserId = uDto.UserId.ToString();
                myUser.UserName = uDto.UserName;
                myUser.PasswordHash = uDto.PasswordHash;
                myUser.SecurityStamp = uDto.SecurityStamp;
                return myUser;
                //using (SqlConnection connection = new SqlConnection(connectionString))
                //    return connection.Query<MsgUser>("select * from Users where UserId = @userId", new { userId = parsedUserId }).SingleOrDefault();
            });
        }

        public virtual Task<IdentityUser> FindByNameAsync(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
                throw new ArgumentNullException("userName");

            return Task.Factory.StartNew(() =>
            {
                var uDtomngr = new UserDtoManager();
                var uDto = uDtomngr.GetUserByUserName(userName);

                if (uDto == null)
                    return null;

                return new IdentityUser()
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

        public virtual Task UpdateAsync(IdentityUser user)
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
        public virtual Task AddLoginAsync(IdentityUser user, UserLoginInfo login)
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

        public virtual Task<IdentityUser> FindAsync(UserLoginInfo login)
        {
            if (login == null)
                throw new ArgumentNullException("login");

            return Task.Factory.StartNew(() =>
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                    return connection.Query<IdentityUser>("select u.* from Users u inner join ExternalLogins l on l.UserId = u.UserId where l.LoginProvider = @loginProvider and l.ProviderKey = @providerKey",
                        login).SingleOrDefault();
            });
        }

        public virtual Task<IList<UserLoginInfo>> GetLoginsAsync(IdentityUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            var elDtomngr = new ExternalLoginsDtoManager();
            var elDto = elDtomngr.ListExternalLogins(user.UserId.IToInt());

            return Task.Factory.StartNew(() =>
            {
                return (IList<UserLoginInfo>)elDto.OfType<UserLoginInfo>().ToList();
                //using (SqlConnection connection = new SqlConnection(connectionString))
                //    return (IList<UserLoginInfo>)connection.Query<UserLoginInfo>("select LoginProvider, ProviderKey from ExternalLogins where UserId = @userId", new { user.UserId }).ToList();
            });
        }

        public virtual Task RemoveLoginAsync(IdentityUser user, UserLoginInfo login)
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
        public virtual Task<string> GetPasswordHashAsync(IdentityUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            return Task.FromResult(user.PasswordHash);
        }

        public virtual Task<bool> HasPasswordAsync(IdentityUser user)
        {
            return Task.FromResult(!string.IsNullOrEmpty(user.PasswordHash));
        }

        public virtual Task SetPasswordHashAsync(IdentityUser user, string passwordHash)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            user.PasswordHash = passwordHash;

            return Task.FromResult(0);
        }

        #endregion

        #region IUserSecurityStampStore
        public virtual Task<string> GetSecurityStampAsync(IdentityUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            return Task.FromResult(user.SecurityStamp);
        }

        public virtual Task SetSecurityStampAsync(IdentityUser user, string stamp)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            user.SecurityStamp = stamp;

            return Task.FromResult(0);
        }

        #endregion
    }
}
