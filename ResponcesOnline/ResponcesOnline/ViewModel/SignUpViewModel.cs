using ResponcesOnline.Model.Tools;
using System.ComponentModel;
using System.Windows.Input;
using ResponcesOnline.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ResponcesOnline.ViewModel
{
    class SignUpViewModel : Window
    {
        private CommandTemplate _addUserCommand;


        private string _userName;
        private string _login;
        private string _password;
        private string _confrimPassword;

        public string UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                OnPropertyChanged(nameof(UserName));
            }
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

        public string ConfrimPassword
        {
            get => _confrimPassword;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(ConfrimPassword));
            }
        }

        #region Команды
        public CommandTemplate AddUserCommand
        {
            get
            {
                if (_addUserCommand == null && string.IsNullOrWhiteSpace(_login) && string.IsNullOrWhiteSpace(_userName)
                    && string.IsNullOrWhiteSpace(_password))
                {
                    if (_password == _confrimPassword)
                    {
                        _addUserCommand = new CommandTemplate(obj =>
                        {
                            AddUser();
                        });
                    }
                }
                else
                {
                    MessageBox.Show("Поля не должны быть пустыми !");
                    return null;
                }

                return _addUserCommand;
            }
        }
        #endregion

        public void AddUser()
        {
            using (otzovic_dbContext db = new otzovic_dbContext())
            {
                User user = new User();
                Role role = new Role();

                user = new User { Login = _login, Password = _password, Nickname = _userName, RoleId = 1 };
                db.Users.Add(user);
                db.SaveChanges();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
