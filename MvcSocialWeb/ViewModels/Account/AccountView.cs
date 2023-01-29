namespace MvcSocialWeb.ViewModels.Account
{
    /// <summary>
    /// Модель для представления главной страницы
    /// </summary>
    public class AccountView
    {
        public RegisterViewModel RegisterView { get; set; }
        public LoginViewModel LoginView { get; set; }

        public AccountView()
        {
            RegisterView = new RegisterViewModel();
            LoginView = new LoginViewModel();
        }

        public AccountView(LoginViewModel loginView)
        {
            RegisterView = new RegisterViewModel();
            LoginView = loginView;
        }
    }
}
