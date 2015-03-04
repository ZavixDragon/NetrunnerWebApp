using Microsoft.Practices.Unity;
using NetrunnerWebApp.Controllers;
using NetrunnerWebApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NetrunnerWebApp.Services
{
    public class Bootstrap
    {
        private EmailService emailer = new Emailer();
        private UserAccountAuthenticatorService authenticator = new UserAccountAuthenticator();
        private UserAccountDatabaseService database = new UserAccountRavenDb();
        private UserAccountEmailService userAccountEmailer;
        private RandomPasswordGeneratorService passwordGenerator = new RandomPasswordGenerator();
        private UserAccountController controller;
        private IUnityContainer myContainer = new UnityContainer();

        public IUnityContainer GetContainer()
        {
            userAccountEmailer = new UserAccountEmailer(emailer);
            controller = new UserAccountController(database, userAccountEmailer, authenticator, passwordGenerator);
            return myContainer.RegisterInstance<UserAccountController>(controller);
        }
    }
}