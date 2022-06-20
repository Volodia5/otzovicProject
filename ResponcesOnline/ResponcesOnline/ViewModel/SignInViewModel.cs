using ResponcesOnline.Model;
using ResponcesOnline.Model.Tools;
using ResponcesOnline.View;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;

namespace ResponcesOnline.ViewModel
{
    class SignInViewModel
    {
        private CommandTemplate _signingCommand;
        private CommandTemplate _singuppingCommand;

        private string _login;
        private string _password;
        private Window _window;

        public SignInViewModel(Window window)
        {
            _window = window;
        }

        public string Login
        {
            get => _login;
            set
            {
                _login = value;
                OnPropertyChanged(nameof(Login));
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        #region Команды
        public CommandTemplate SigningCommand
        {
            get
            {
                if (_signingCommand == null)
                {
                    _signingCommand = new CommandTemplate(obj =>
                    {
                        User user;
                        SignIn(out user);

                        if (user == null)
                        {
                            MessageBox.Show("Неправильный логин или пароль !");
                        }
                        else
                        {
                            if (user.RoleId == 1)
                                OpenCategoriesView(user);
                            if (user.RoleId == 2)
                                OpenCategoriesAdminView(user);
                        }
                    });
                }

                return _signingCommand;
            }
        }

        public CommandTemplate SinguppingCommand
        {
            get
            {
                if (_singuppingCommand == null)
                {
                    _singuppingCommand = new CommandTemplate(obj =>
                    {
                        OpenSignUpView();
                    });
                }

                return _singuppingCommand;
            }
        }

        #endregion

        public void SignIn(out User user)
        {
            using (otzovic_dbContext db = new otzovic_dbContext())
            {
                user = null;

                List<User> users = db.Users.ToList();

                if (Login != null && Password != null)
                {
                    foreach (User item in users)
                    {
                        if (item.Login == Login && item.Password == Password)
                        {
                            user = item;
                        }
                    }
                }
            }
        }

        public void OpenCategoriesView(User user)
        {
            CategoriesWindow categoriesWindow = new CategoriesWindow();
            CategoriesViewModel categoriesViewModel = new CategoriesViewModel(user, categoriesWindow);

            _window.Hide();

            categoriesWindow.DataContext = categoriesViewModel;
            categoriesWindow.ShowDialog();
            _window.Show();
        }

        public void OpenCategoriesAdminView(User user)
        {
            CategoriesAdminView categoriesAdminWindow = new CategoriesAdminView();
            CategoriesAdminViewModel categoriesAdminViewModel = new CategoriesAdminViewModel(user, categoriesAdminWindow);

            _window.Hide();

            categoriesAdminWindow.DataContext = categoriesAdminViewModel;
            categoriesAdminWindow.ShowDialog();
            _window.Show();
        }

        public void OpenSignUpView()
        {
            SignUpWindow signUpWindow = new SignUpWindow();
            SignUpViewModel signUpViewModel = new SignUpViewModel();

            _window.Hide();

            signUpWindow.DataContext = signUpViewModel;
            signUpWindow.ShowDialog();
            _window.Show();
        }

        public void OnClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
