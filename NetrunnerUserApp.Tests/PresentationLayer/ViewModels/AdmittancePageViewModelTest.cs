using DomainObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetrunnerUserApp.Interfaces;
using NetrunnerUserApp.PresentationLayer.Entities;
using NetrunnerUserApp.PresentationLayer.Interfaces;
using NetrunnerUserApp.PresentationLayer.ViewModels;
using NetrunnerUserApp.Tests.TestAssertions;
using NetrunnerWebApp.PresetnationLayer.Entities;
using Rhino.Mocks;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;

namespace NetrunnerUserApp.Tests.PresentationLayer.ViewModels
{
    [TestClass]
    public class AdmittancePageViewModelTest
    {
        private ImageProvider _imageProviderStub;
        private GatewayService _gatewayStub;
        private ViewManagerService _viewManagerStub;
        private InputValidaterService _validaterStub;
        private List<ViewModelCommandMode> _commandModes = new List<ViewModelCommandMode>();
        private AdmittancePageViewModel viewModel;

        private string NotEmpty = "stuff";
        private int NotZero = 1;

        [TestInitialize]
        public void init()
        {
            _imageProviderStub = MockRepository.GenerateStub<ImageProvider>();
            _gatewayStub = MockRepository.GenerateStub<GatewayService>();
            _viewManagerStub = MockRepository.GenerateStub<ViewManagerService>();
            _validaterStub = MockRepository.GenerateStub<InputValidaterService>();
            viewModel = new AdmittancePageViewModel(_imageProviderStub, _gatewayStub, _viewManagerStub, _validaterStub, _commandModes);
        }

        [TestMethod]
        public void AdmittancePageViewModel_SignUpProperly_AllMethodsCalled()
        {
            _validaterStub.Stub(s => s.ValidateSignUp(Arg<UserAccount>.Is.Anything)).Return(Task.FromResult(""));

            viewModel.SignUp(null);

            _validaterStub.AssertWasCalled(s => s.ValidateSignUp(Arg<UserAccount>.Is.Anything));
            _gatewayStub.AssertWasCalled(s => s.MakeRequest<StringObject>(Arg<UserAccount>.Is.Anything, Arg<string>.Is.Anything));
        }

        [TestMethod]
        public void AdmittancePageViewModel_SignUpWithInvalidInputs_GatewayNotCalled()
        {
            _validaterStub.Stub(s => s.ValidateSignUp(Arg<UserAccount>.Is.Anything)).Return(Task.FromResult("FailedToValidate"));

            viewModel.SignUp(null);

            _gatewayStub.AssertWasNotCalled(s => s.MakeRequest<StringObject>(Arg<UserAccount>.Is.Anything, Arg<string>.Is.Anything));
        }

        [TestMethod]
        public void AdmittancePageViewModel_Login_GatewayCalled()
        {
            viewModel.Login(null);

            _gatewayStub.AssertWasCalled(s => s.MakeRequest<StringObject>(Arg<UserAccount>.Is.Anything, Arg<string>.Is.Anything));
        }

        [TestMethod]
        public void AdmittancePageViewModel_RecoverUsername_GatewayCalled()
        {
            viewModel.RecoverUsername(null);

            _gatewayStub.AssertWasCalled(s => s.MakeRequest<StringObject>(Arg<string>.Is.Anything, Arg<string>.Is.Anything));
        }

        [TestMethod]
        public void AdmittancePageViewModel_resetPassword_GatewayCalled()
        {
            viewModel.ResetPassword(null);

            _gatewayStub.AssertWasCalled(s => s.MakeRequest<StringObject>(Arg<string>.Is.Anything, Arg<string>.Is.Anything));
        }

        [TestMethod]
        public void AdmittancePageViewModel_SetMode_AllThingsChangedProperly()
        {
            viewModel.CurrentInput.Username = NotEmpty;
            viewModel.CurrentInput.Password = NotEmpty;
            viewModel.CurrentInput.Email = NotEmpty;
            viewModel.SystemMessage = NotEmpty;
            _commandModes.Add(GetTestCommandMode());

            viewModel.SetMode(_commandModes[0]);

            Assert.IsTrue(viewModel.CurrentInput.Username == "" && viewModel.CurrentInput.Password == "" && viewModel.CurrentInput.Email == "", 
                "Current Inputs Not Reset");
            Assert.IsTrue(viewModel.UsernameHeight == NotZero && viewModel.PasswordHeight == NotZero && viewModel.EmailHeight == NotZero, "Input Boxes Not Set");
            Assert.IsTrue(viewModel.ResetPasswordVisibility == ViewDisplayValues.ResetPasswordButtonEnabled && viewModel.ResetPasswordEnabled, "Reset Password Not Set");
            Assert.IsTrue(viewModel.RecoverUsernameVisibility == ViewDisplayValues.RecoverUsernameButtonEnabled && viewModel.RecoverUsernameEnabled, "Recover Username Not Set");
            Assert.IsTrue(viewModel.PrimaryButtonName == NotEmpty && viewModel.PrimaryCommand != null, "Primary Button Not Set");
            Assert.IsTrue(viewModel.SecondaryButtonName == NotEmpty && viewModel.SecondaryCommand != null, "Secondary Button Not Set");
        }

        [TestMethod]
        public void AdmittancePageViewModel_SetDisplay_StringsResetAndMessageSet()
        {
            viewModel.CurrentInput.Username = NotEmpty;
            viewModel.CurrentInput.Password = NotEmpty;
            viewModel.CurrentInput.Email = NotEmpty;

            viewModel.SetDisplay(NotEmpty);

            Assert.AreEqual("", viewModel.CurrentInput.Username);
            Assert.AreEqual("", viewModel.CurrentInput.Password);
            Assert.AreEqual("", viewModel.CurrentInput.Email);
            Assert.AreEqual(NotEmpty, viewModel.SystemMessage);
        }

        [TestMethod]
        public void AdmittancePageViewModel_GetCommandModeByProperName_ReturnCommandMode()
        {
            _commandModes.Add(new ViewModelCommandMode());

            ViewModelCommandMode commandMode = viewModel.GetCommandModeByName("ViewModelCommandMode");

            Assert.AreEqual(commandMode, _commandModes[0]);
        }
        
        [TestMethod]
        public void AdmittancePageViewModel_AskForNonExistentCommandMode_ReturnNull()
        {
            _commandModes.Add(new ViewModelCommandMode());

            ViewModelCommandMode commandMode = viewModel.GetCommandModeByName("NonExistentCommandMode");

            Assert.AreEqual(commandMode, null);
        }

        [TestMethod]
        public void AdmittancePageViewModel_GetAllICommandsByCommandName_ReturnsICommandsWithAsyncDelegateCommand()
        {
            Assert.AreEqual(typeof(AsyncDelegateCommand), viewModel.GetICommandByCommandName(CommandName.Login).GetType());
            Assert.AreEqual(typeof(AsyncDelegateCommand), viewModel.GetICommandByCommandName(CommandName.SignUp).GetType());
            Assert.AreEqual(typeof(AsyncDelegateCommand), viewModel.GetICommandByCommandName(CommandName.RecoverUsername).GetType());
            Assert.AreEqual(typeof(AsyncDelegateCommand), viewModel.GetICommandByCommandName(CommandName.ResetPassword).GetType());
            Assert.AreEqual(typeof(AsyncDelegateCommand), viewModel.GetICommandByCommandName(CommandName.SwitchToLoginMode).GetType());
            Assert.AreEqual(typeof(AsyncDelegateCommand), viewModel.GetICommandByCommandName(CommandName.SwitchToSignUpMode).GetType());
        }

        [TestMethod]
        public void AdmittancePageViewModel_GetICommandByCommandNameWithNull_ThrowsInvalidOperationException()
        {
            ExceptionAssert.Throws<InvalidOperationException>(() => viewModel.GetICommandByCommandName(CommandName.Null));
        }

        [TestMethod]
        public void AdmittancePageViewModel_BindablesImages_ProviderCalled()
        {
            SetupImageProviderStub();

            Assert.IsNotNull(viewModel.TitleImage);
        }

        private ViewModelCommandMode GetTestCommandMode()
        {
            return new ViewModelCommandMode
            {
                UsernameActive = NotZero,
                PasswordActive = NotZero,
                EmailActive = NotZero,
                RecoverUsernameButtonActive = ViewDisplayValues.RecoverUsernameButtonEnabled,
                ResetPasswordButtonActive = ViewDisplayValues.ResetPasswordButtonEnabled,
                PrimaryButtonName = NotEmpty,
                PrimaryCommandName = CommandName.Login,
                SecondaryButtonName = NotEmpty,
                SecondaryCommandName = CommandName.SignUp
            };
        }

        private void SetupImageProviderStub()
        {
            _imageProviderStub.Stub(s => s.GetImage(Arg<string>.Is.Anything)).Return(Task.FromResult((Image)Resources.TestResources.TestImage));
        }
    }
}