using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetrunnerUserApp.PresentationLayer.Entities
{
    public class ResetPasswordCommandMode : ViewModelCommandMode
    {
        public ResetPasswordCommandMode()
        {
            UsernameActive = ViewDisplayValues.UsernameEntryDisenabled;
            PasswordActive = ViewDisplayValues.PasswordEntryDisenabled;
            EmailActive = ViewDisplayValues.EmailEntryEnabled;
            ResetPasswordButtonActive = ViewDisplayValues.ResetPasswordButtonDisenabled;
            RecoverUsernameButtonActive = ViewDisplayValues.RecoverUsernameButtonEnabled;
            PrimaryButtonName = ViewDisplayValues.ResetPassword;
            PrimaryCommandName = CommandName.ResetPassword;
            SecondaryButtonName = ViewDisplayValues.SwitchToLoginMode;
            SecondaryCommandName = CommandName.SwitchToLoginMode;
        }
    }
}
