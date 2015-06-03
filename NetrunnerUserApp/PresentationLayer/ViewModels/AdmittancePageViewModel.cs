using DomainObjects;
using NetrunnerUserApp.Interfaces;
using NetrunnerUserApp.PresentationLayer.Entities;
using NetrunnerUserApp.PresentationLayer.Interfaces;
using NetrunnerUserApp.PresentationLayer.Services;
using NetrunnerWebApp.PresetnationLayer.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Reflection;
using NetrunnerUserApp.Entities;

namespace NetrunnerUserApp.PresentationLayer.ViewModels
{
    public class AdmittancePageViewModel : NotificationObject, IInitializable
    {
        private ImageProvider _imageProvider;
        private GatewayService _gateway;
        private ViewManagerService _viewManager;
        private InputValidaterService _validater;
        private List<ViewModelCommandMode> _commandModes;
        private List<ICommand> _commands = new List<ICommand>();

        public UserAccount CurrentInput { get; set; }
        public ICommand PrimaryCommand { get; set; }
        public ICommand SecondaryCommand { get; set; }
        public string PrimaryButtonName { get; set; }
        public string SecondaryButtonName { get; set; }
        public string SystemMessage { get; set; }
        public int UsernameHeight { get; set; }
        public int PasswordHeight { get; set; }
        public int EmailHeight { get; set; }
        public int ResetPasswordVisibility { get; set; }
        public bool ResetPasswordEnabled { get; set; }
        public int RecoverUsernameVisibility { get; set; }
        public bool RecoverUsernameEnabled { get; set; }

        public AdmittancePageViewModel(ImageProvider provider, GatewayService gateway, ViewManagerService viewManager, 
                                       InputValidaterService validater, List<ViewModelCommandMode> commandModes)
        {
            _imageProvider = provider;
            _gateway = gateway;
            _viewManager = viewManager;
            _validater = validater;
            _commandModes = commandModes;
            CurrentInput = new UserAccount();
        }

        public async Task Initialize()
        {
            await SwitchToLoginMode(null);
        }

        public ICommand GoToSignUpCommandMode { get { return new AsyncDelegateCommand(SwitchToSignUpMode);  } }
        public ICommand GoToLoginCommandMode { get { return new AsyncDelegateCommand(SwitchToLoginMode); } }
        public ICommand GoToResetPasswordCommandMode { get { return new AsyncDelegateCommand(SwitchToResetPasswordMode); } }
        public ICommand GoToRecoverUsernameCommandMode { get { return new AsyncDelegateCommand(SwitchToRecoverUsernameMode); } }
        public ICommand SignUpCommand { get { return new AsyncDelegateCommand(SignUp); } }
        public ICommand LoginCommand { get { return new AsyncDelegateCommand(Login); } }
        public ICommand ResetPasswordCommand { get { return new AsyncDelegateCommand(ResetPassword); } }
        public ICommand RecoverUsernameCommand { get { return new AsyncDelegateCommand(RecoverUsername); } }

        public async Task SwitchToSignUpMode(object parameter)
        {
            SetMode(GetCommandModeByName("SignUpCommandMode"));
        }

        public async Task SwitchToLoginMode(object parameter)
        {
            SetMode(GetCommandModeByName("LoginCommandMode"));
        }

        public async Task SwitchToResetPasswordMode(object parameter)
        {
            SetMode(GetCommandModeByName("ResetPasswordCommandMode"));
        }

        public async Task SwitchToRecoverUsernameMode(object parameter)
        {
            SetMode(GetCommandModeByName("RecoverUsernameCommandMode"));
        }

        public void SetMode(ViewModelCommandMode mode)
        {
            SetDisplay("");
            UsernameHeight = mode.UsernameActive;
            PasswordHeight = mode.PasswordActive;
            EmailHeight = mode.EmailActive;
            ResetPasswordVisibility = mode.ResetPasswordButtonActive;
            ResetPasswordEnabled = mode.ResetPasswordButtonActive == ViewDisplayValues.ResetPasswordButtonEnabled;
            RecoverUsernameVisibility = mode.RecoverUsernameButtonActive;
            RecoverUsernameEnabled = mode.RecoverUsernameButtonActive == ViewDisplayValues.RecoverUsernameButtonEnabled;
            PrimaryButtonName = mode.PrimaryButtonName;
            PrimaryCommand = GetICommandByCommandName(mode.PrimaryCommandName);
            SecondaryButtonName = mode.SecondaryButtonName;
            SecondaryCommand = GetICommandByCommandName(mode.SecondaryCommandName);
            RaiseAllDynamicPropertiesChanged();
        }

        public async Task SignUp(object parameter)
        {
            SystemMessage = await _validater.ValidateSignUp(CurrentInput);
            if (SystemMessage == "")
                await SendRequest(CurrentInput, Routes.SignUp);
            SetDisplay(SystemMessage);
        }

        public async Task Login(object parameter)
        {
            await SendRequest(CurrentInput, Routes.Login);
            if (SystemMessage != SystemMessages.InvalidLogin)
            {
                SecurityToken.token = SystemMessage;
                await _viewManager.NavigateTo(ViewFactoryKeys.DeckBuilderPage);
            }
            SetDisplay(SystemMessage);
        }

        public async Task ResetPassword(object parameter)
        {
            await SendRequest(CurrentInput.Email, Routes.ResetPassword);
            SetDisplay(SystemMessage);
        }

        public async Task RecoverUsername(object parameter)
        {
            await SendRequest(CurrentInput.Email, Routes.RecoverUsername);
            SetDisplay(SystemMessage);
        }

        public void SetDisplay(string systemMessage)
        {
            CurrentInput.Username = "";
            CurrentInput.Password = "";
            CurrentInput.Email = "";
            SystemMessage = systemMessage;
            RaisePropertyChanged(new string[] { "CurrentInput", "SystemMessage" } );
        }

        public ViewModelCommandMode GetCommandModeByName(string name)
        {
            return _commandModes.Find(x => x.ToString().Contains(name));
        }

        public ICommand GetICommandByCommandName(CommandName name)
        {
            if (name == CommandName.Login) 
                return LoginCommand;
            if (name == CommandName.SignUp) 
                return SignUpCommand;
            if (name == CommandName.RecoverUsername)
                return RecoverUsernameCommand;
            if (name == CommandName.ResetPassword)
                return ResetPasswordCommand;
            if (name == CommandName.SwitchToLoginMode)
                return GoToLoginCommandMode;
            if (name == CommandName.SwitchToSignUpMode)
                return GoToSignUpCommandMode;
            throw new InvalidOperationException(string.Format("Command Not Found: {0}", name));
        }

        public void RaiseAllDynamicPropertiesChanged()
        {
            RaisePropertyChanged(new string[] {
                "PrimaryButtonName",
                "PrimaryCommand",
                "SecondaryButtonName",
                "SecondaryCommand",
                "UsernameHeight",
                "PasswordHeight",
                "EmailHeight",
                "SystemMessage",
                "RecoverUsernameVisibility",
                "RecoverUsernameEnabled",
                "ResetPasswordVisibility",
                "ResetPasswordEnabled",
                "CurrentInput"
            });
        }

        public Image TitleImage
        {
            get { return _imageProvider.GetImage("TitleImage"); }
        }

        private async Task SendRequest(object parameter, string route)
        {
            var SystemMessageObject = await _gateway.MakeRequest<StringObject>(parameter, route);
            SystemMessage = SystemMessageObject.Value;
        }
    }
}
