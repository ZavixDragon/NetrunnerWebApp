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
        private IUnityContainer myContainer = new UnityContainer();

        public IUnityContainer GetContainer()
        {
            myContainer.RegisterType<EmailService, Emailer>();
            myContainer.RegisterType<UserAccountDatabaseService, UserAccountRavenDb>();
            myContainer.RegisterType<UserAccountAuthenticatorService, UserAccountAuthenticator>();
            myContainer.RegisterType<RandomPasswordGeneratorService, RandomPasswordGenerator>();
            myContainer.RegisterType<UserAccountEmailService, UserAccountEmailer>();
            return myContainer;
        }
    }
}