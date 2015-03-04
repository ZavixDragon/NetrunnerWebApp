using NetrunnerWebApp.Interfaces;
using NetrunnerWebApp.Models;
using Raven.Client;
using Raven.Client.Document;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace NetrunnerWebApp.Services
{
    public class UserAccountRavenDb : UserAccountDatabaseService
    {
        private IDocumentSession Session { get; set; }
        private IDocumentStore Store { get { return LazyDocStore.Value; }}

        private static readonly Lazy<IDocumentStore> LazyDocStore = new Lazy<IDocumentStore>(() =>
        {
            var docStore = new DocumentStore
            {
                Url = "http://localhost:8080",
                DefaultDatabase = "UserAccount"
            };

            docStore.Initialize();
            return docStore;
        });

        public Task AddNewAccount(UserAccount userApplicant)
        {
            Session.Store(userApplicant);
            Session.SaveChanges();
            return Task.FromResult(true);
        }

        public Task ChangePassword(UserAccount currentUser, string newPassword)
        {
            currentUser.Password = newPassword;
            Session.Store(currentUser);
            Session.SaveChanges();
            return Task.FromResult(true);
        }

        public Task<UserAccount> GetAccountInfo(string username)
        {
            return Task.FromResult((from userAccount in Session.Query<UserAccount>()
                                    where userAccount.Username == username
                                    select userAccount).Single());
        }

        public Task<UserAccount> GetAccountInfoFromEmail(string email)
        {
            return Task.FromResult((from userAccount in Session.Query<UserAccount>()
                                    where userAccount.Email == email
                                    select userAccount).Single());
        }

        public Task<bool> IsUsernameNotTaken(string username)
        {
            return Task.FromResult(GetAccountInfo(username) == null);
        }

        public Task<bool> IsEmailNotInUse(string email)
        {
            return Task.FromResult(GetAccountInfoFromEmail(email) == null);
        }
    }
}