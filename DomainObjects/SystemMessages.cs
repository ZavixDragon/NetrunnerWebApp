using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainObjects
{
    public class SystemMessages
    {
        public const string IncompleteSignUp = "You must fill all Fields";
        public const string InsecurePassword = "Password must contain at least 6 characters with a letter and a number";
        public const string InvalidEmail = "Email Is Invalid";
        public const string EmailAlreadyTaken = "That email is already in use";
        public const string UsernameAlreadyTaken = "That username is already taken";
        public const string SuccessfulRegister = "Your account has successfully been added";
        public const string UsernameRecovered = "Your username has been emailed to you";
        public const string NonExistentEmail = "That email is not in use";
        public const string PasswordWasReset = "Your new password has been emailed to you";
        public const string PasswordUpdated = "Your password has been updated";
        public const string InvalidPassword = "Invalid password";
        public const string InvalidLogin = "Incorrect username or password";
    }
}
