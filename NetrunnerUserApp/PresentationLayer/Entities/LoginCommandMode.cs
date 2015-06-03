using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetrunnerUserApp.PresentationLayer.Entities
{
    public class LoginCommandMode : ViewModelCommandMode
    {
        public LoginCommandMode()
        {
            UsernameActive = ViewDisplayValues.UsernameEntryEnabled;
            PasswordActive = ViewDisplayValues.PasswordEntryEnabled;
            EmailActive = ViewDisplayValues.EmailEntryDisenabled;
            ResetPasswordButtonActive = ViewDisplayValues.ResetPasswordButtonEnabled;
            RecoverUsernameButtonActive = ViewDisplayValues.RecoverUsernameButtonEnabled;
            PrimaryButtonName = ViewDisplayValues.Login;
            PrimaryCommandName = CommandName.Login;
            SecondaryButtonName = ViewDisplayValues.SwitchToSignUpMode;
            SecondaryCommandName = CommandName.SwitchToSignUpMode;
        }
    }
}
