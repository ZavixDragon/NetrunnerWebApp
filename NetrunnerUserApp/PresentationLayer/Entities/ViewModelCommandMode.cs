using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetrunnerUserApp.PresentationLayer.Entities
{
    public class ViewModelCommandMode
    {
        public int UsernameActive { get; set; }
        public int PasswordActive { get; set; }
        public int EmailActive { get; set; }
        public int ResetPasswordButtonActive { get; set; }
        public int RecoverUsernameButtonActive { get; set; }
        public string PrimaryButtonName { get; set; }
        public CommandName PrimaryCommandName { get; set; }
        public string SecondaryButtonName { get; set; }
        public CommandName SecondaryCommandName { get; set; }
    }
}
