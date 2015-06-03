using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetrunnerUserApp.PresentationLayer.Entities
{
    public class RecoverUsernameCommandMode : ViewModelCommandMode
    {
        public RecoverUsernameCommandMode()
        {
            UsernameActive = ViewDisplayValues.UsernameEntryDisenabled;
            PasswordActive = ViewDisplayValues.PasswordEntryDisenabled;
            EmailActive = ViewDisplayValues.EmailEntryEnabled;
            ResetPasswordButtonActive = ViewDisplayValues.ResetPasswordButtonEnabled;
            RecoverUsernameButtonActive = ViewDisplayValues.RecoverUsernameButtonDisenabled;
            PrimaryButtonName = ViewDisplayValues.RecoverUsername;
            PrimaryCommandName = CommandName.RecoverUsername;
            SecondaryButtonName = ViewDisplayValues.SwitchToLoginMode;
            SecondaryCommandName = CommandName.SwitchToLoginMode;
        }
    }
}
