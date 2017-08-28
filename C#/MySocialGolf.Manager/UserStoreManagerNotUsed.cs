using MySocialGolf.DtoModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySocialGolf.Manager
{
    public class UserStoreManagerNotUsed : IUserStore<User>, IUserLoginStore<User>, IUserPasswordStore<User>, IUserSecurityStampStore<User>
    {
        #region IUserStore
        public virtual Task CreateAsync(MsgUser user)
        {
            if (MsgUser == null)
                throw new ArgumentNullException("user");

            return Task.Factory.StartNew(() => {
                user.UserId = Guid.NewGuid();
                using (SqlConnection connection = new SqlConnection(connectionString))
                    connection.Execute("insert into Users(UserId, UserName, PasswordHash, SecurityStamp) values(@userId, @userName, @passwordHash, @securityStamp)", user);
            });
        }

        public virtual Task DeleteAsync(MsgUser user)
        {
            if (MsgUser == null)
                throw new ArgumentNullException("user");

            return Task.Factory.StartNew(() =>
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                    connection.Execute("delete from Users where UserId = @userId", new { user.UserId });
            });
        }

        public virtual Task<User> FindByIdAsync(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                throw new ArgumentNullException("userId");

            Guid parsedUserId;
            if (!Guid.TryParse(userId, out parsedUserId))
                throw new ArgumentOutOfRangeException("userId", string.Format("'{0}' is not a valid GUID.", new { userId }));

            return Task.Factory.StartNew(() =>
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                    return connection.Query<User>("select * from Users where UserId = @userId", new { userId = parsedUserId }).SingleOrDefault();
            });
        }

        public virtual Task<User> FindByNameAsync(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
                throw new ArgumentNullException("userName");

            return Task.Factory.StartNew(() =>
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                    return connection.Query<User>("select * from Users where lower(UserName) = lower(@userName)", new { userName }).SingleOrDefault();
            });
        }

        public virtual Task UpdateAsync(MsgUser user)
        {
            if (MsgUser == null)
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
            if (MsgUser == null)
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

        public virtual Task<User> FindAsync(UserLoginInfo login)
        {
            if (login == null)
                throw new ArgumentNullException("login");

            return Task.Factory.StartNew(() =>
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                    return connection.Query<User>("select u.* from Users u inner join ExternalLogins l on l.UserId = u.UserId where l.LoginProvider = @loginProvider and l.ProviderKey = @providerKey",
                        login).SingleOrDefault();
            });
        }

        public virtual Task<IList<UserLoginInfo>> GetLoginsAsync(MsgUser user)
        {
            if (MsgUser == null)
                throw new ArgumentNullException("user");

            return Task.Factory.StartNew(() =>
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                    return (IList<UserLoginInfo>)connection.Query<UserLoginInfo>("select LoginProvider, ProviderKey from ExternalLogins where UserId = @userId", new { user.UserId }).ToList();
            });
        }

        public virtual Task RemoveLoginAsync(MsgUser user, UserLoginInfo login)
        {
            if (MsgUser == null)
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
            if (MsgUser == null)
                throw new ArgumentNullException("user");

            return Task.FromResult(user.PasswordHash);
        }

        public virtual Task<bool> HasPasswordAsync(MsgUser user)
        {
            return Task.FromResult(!string.IsNullOrEmpty(user.PasswordHash));
        }

        public virtual Task SetPasswordHashAsync(MsgUser user, string passwordHash)
        {
            if (MsgUser == null)
                throw new ArgumentNullException("user");

            user.PasswordHash = passwordHash;

            return Task.FromResult(0);
        }

        #endregion

        #region IUserSecurityStampStore
        public virtual Task<string> GetSecurityStampAsync(MsgUser user)
        {
            if (MsgUser == null)
                throw new ArgumentNullException("user");

            return Task.FromResult(user.SecurityStamp);
        }

        public virtual Task SetSecurityStampAsync(MsgUser user, string stamp)
        {
            if (MsgUser == null)
                throw new ArgumentNullException("user");

            user.SecurityStamp = stamp;

            return Task.FromResult(0);
        }
        #endregion
    }
