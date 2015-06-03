using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetrunnerUserApp.PresentationLayer.Entities
{
    public class SignUpCommandMode : ViewModelCommandMode
    {
        public SignUpCommandMode()
        {
            UsernameActive = ViewDisplayValues.UsernameEntryEnabled;
            PasswordActive = ViewDisplayValues.PasswordEntryEnabled;
            EmailActive = ViewDisplayValues.EmailEntryEnabled;
            ResetPasswordButtonActive = ViewDisplayValues.ResetPasswordButtonEnabled;
            RecoverUsernameButtonActive = ViewDisplayValues.RecoverUsernameButtonEnabled;
            PrimaryButtonName = ViewDisplayValues.SignUp;
            PrimaryCommandName = CommandName.SignUp;
            SecondaryButtonName = ViewDisplayValues.SwitchToLoginMode;
            SecondaryCommandName = CommandName.SwitchToLoginMode;
        }
    }
}
