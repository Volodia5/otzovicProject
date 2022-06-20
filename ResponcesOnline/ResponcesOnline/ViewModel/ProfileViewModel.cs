using ResponcesOnline.Model;
using ResponcesOnline.Model.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ResponcesOnline.ViewModel
{
    public class ProfileViewModel : INotifyPropertyChanged
    {
        private CommandTemplate _updateUserCommand;
        private User _user;
        private string _nickName;
        private string _login;
        private string _password;

        public ProfileViewModel(User user)
        {
            User = user;
            SetUserData();
        }

        public User User
        {
            get => _user;
            set
            {
                _user = value;
                OnPropertyChanged(nameof(User));
            }
        }

        public string UserName
        {
            get
            {
                return _nickName;
            }
            set
            {
                if (value != _nickName)
                    _nickName = value;
                OnPropertyChanged(nameof(UserName));
            }
        }

        public string Login
        {
            get
            {
                return _login;
            }
            set
            {
                if (value != _login)
                    _login = value;
                OnPropertyChanged(nameof(Login));
            }
        }

        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                if (value != _password)
                    _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public CommandTemplate UpdateUserCommand
        {
            get
            {
                if(_updateUserCommand == null)
                {
                    _updateUserCommand = new CommandTemplate(obj =>
                    {
                        UpdateUser();
                    });
                }
                return _updateUserCommand;
            }
        }

        public void UpdateUser()
        {
            using (otzovic_dbContext db = new otzovic_dbContext())
            {
                User user = db.Users.Where(x => x.Id == User.Id).FirstOrDefault();

                if (user.Nickname != UserName)
                    user.Nickname = UserName;
                if (user.Login != Login)
                    user.Login = Login;
                if (user.Password != Password)
                    user.Password = Password;

                db.Update(user);
                db.SaveChanges();
            }
        }

        public void SetUserData()
        {
            UserName = User.Nickname;
            Login = User.Login;
            Password = User.Password;
        }

        public void OnClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
