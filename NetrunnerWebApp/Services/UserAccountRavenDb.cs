﻿using DomainObjects;
using NetrunnerWebApp.Interfaces;
using Raven.Client;
using Raven.Client.Document;
using System;
using System.Linq;
using System.Threading.Tasks;

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
                DefaultDatabase = "UserAccounts"
            };

            docStore.Initialize();
            return docStore;
        });

        public Task AddNewAccount(UserAccount userApplicant)
        {
            userApplicant.Id = userApplicant.Username;
            using (Session = Store.OpenSession())
            {
                Session.Store(userApplicant);
                Session.SaveChanges();
            }
            return Task.FromResult(true);
        }

        public Task ChangePassword(UserAccount currentUser, string newPassword)
        {
            currentUser.Password = newPassword;
            using (Session = Store.OpenSession())
            {
                Session.Store(currentUser);
                Session.SaveChanges();
            }
            return Task.FromResult(true);
        }

        public Task<UserAccount> GetAccountInfo(string username)
        {
            using (Session = Store.OpenSession())
            {
                UserAccount user = ((from userAccount in Session.Query<UserAccount>()
                                     where userAccount.Username == username
                                     select userAccount).SingleOrDefault()) ?? new UserAccount();
                return Task.FromResult(user);
            }
        }

        public Task<UserAccount> GetAccountInfoFromEmail(string email)
        {
            using (Session = Store.OpenSession())
            {
                UserAccount user = ((from userAccount in Session.Query<UserAccount>()
                                     where userAccount.Email == email
                                     select userAccount).SingleOrDefault()) ?? new UserAccount();
                return Task.FromResult(user);
            }
        }

        public async Task<bool> IsUsernameTaken(string username)
        {
            UserAccount QueryResult = await GetAccountInfo(username);
            return QueryResult.Username != null;
        }

        public async Task<bool> IsEmailTaken(string email)
        {
            UserAccount QueryResult = await GetAccountInfoFromEmail(email);
            return QueryResult.Username != null;
        }

        public Task DeleteUserAccount(string username)
        {
            using (Session = Store.OpenSession())
            {
                UserAccount targetAccount = Session.Load<UserAccount>(username);
                Session.Delete(targetAccount);
                Session.SaveChanges();
            }
            return Task.FromResult(true);
        }
    }
}