//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Security;
//using MySql.Web.Security;

//namespace Fintrak.Presentation.WebClient.Core
//{
//    public class MySqlSimpleMembership : MySqlSimpleMembershipProvider
//    {
//        public MySqlSimpleMembershipProvider();
//        public MySqlSimpleMembershipProvider(MembershipProvider previousProvider);

//        public string ConnectionString { get; set; }
//        public string ConnectionStringName { get; set; }
//        public override bool EnablePasswordReset { get; }
//        public override bool EnablePasswordRetrieval { get; }
//        public override int MaxInvalidPasswordAttempts { get; }
//        public override int MinRequiredPasswordLength { get; }
//        public override int MinRequiredNonAlphanumericCharacters { get; }
//        public override int PasswordAttemptWindow { get; }
//        public override MembershipPasswordFormat PasswordFormat { get; }
//        public override string PasswordStrengthRegularExpression { get; }
//        public string ProviderName { get; set; }
//        public override bool RequiresQuestionAndAnswer { get; }
//        public override bool RequiresUniqueEmail { get; }
//        public string UserTableName { get; }
//        public override string ApplicationName { get; set; }
//        public string UserIdColumn { get; }
//        public string UserNameColumn { get; }

//        public override string CreateUserAndAccount(string userName, string password, bool requireConfirmation, IDictionary<string, object> values);

//        public override MembershipUser GetUser(string username, bool userIsOnline) 
//        { 
//            return base.GetUser(username, userIsOnline);
//        }
//        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline);
//        public int GetUserId(string userName);
//        public override int GetUserIdFromOAuth(string provider, string providerUserId);
//        public override int GetUserIdFromPasswordResetToken(string token);
//        public override string GetUserNameByEmail(string email);
//        public override string GetUserNameFromId(int userId);
//        public override bool HasLocalAccount(int userId);
//        public override void Initialize(string name, NameValueCollection config);
//        public override bool IsConfirmed(string userName);
//        public override void ReplaceOAuthRequestTokenWithAccessToken(string requestToken, string accessToken, string accessTokenSecret);
//        public override string ResetPassword(string username, string answer);
//        public override bool ResetPasswordWithToken(string token, string newPassword);
//        public override void StoreOAuthRequestToken(string requestToken, string requestTokenSecret);
//        public override bool UnlockUser(string userName);
//        public override void UpdateUser(MembershipUser user);
//        public override bool ValidateUser(string username, string password);
//    }
//}