using ResponcesOnline.Model;
using ResponcesOnline.View;
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
    class CategoriesViewModel
    {
        private CommandTemplate _openProfileViewCommand;

        private string _userName;
        private User _user;
        private Window _window;

        public CategoriesViewModel(User user, Window window)
        {
            User = user;
            _window = window;
            SetUserName();
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
            get => _userName;
            set
            {
                _userName = value;
                OnPropertyChanged(nameof(UserName));
            }
        }

        #region Команды
        public CommandTemplate OpenProfileViewCommand
        {
            get
            {
                if (_openProfileViewCommand == null)
                    _openProfileViewCommand = new CommandTemplate(obj =>
                    {
                        GoToProfileForm();
                    });
                return _openProfileViewCommand;
            }
        }
        #endregion

        public void GoToProfileForm()
        {
            ProfileWindow profileWindow = new ProfileWindow();
            ProfileViewModel profileViewModel = new ProfileViewModel(User);

            _window.Hide();

            profileWindow.DataContext = profileViewModel;
            profileWindow.ShowDialog();
            _window.Show();
        }

        public void GoToUserReportsForm(object obj)
        {
            MyReportsView myReportsWindow = new MyReportsView();
            MyReportsViewModel myReportsViewModel = new MyReportsViewModel();

            _window.Hide();

            myReportsWindow.DataContext = myReportsViewModel;
            myReportsWindow.ShowDialog();
            _window.Show();
        }

        public void SetUserName()
        {
            UserName = this.User.Nickname.ToString();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

