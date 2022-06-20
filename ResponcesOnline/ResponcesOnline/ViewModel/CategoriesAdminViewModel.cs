using ResponcesOnline.Model;
using ResponcesOnline.Model.Tools;
using ResponcesOnline.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ResponcesOnline.ViewModel
{
    class CategoriesAdminViewModel
    {
        private CommandTemplate _openProfileViewCommand;
        private CommandTemplate _openMyReportsView;
        private CommandTemplate _openCategoryEditViewCommand;
        private CommandTemplate _openSubcategoryEditViewCommand;

        List<Button> buutons;

        private string _userName;
        private User _user;
        private Window _window;

        public CategoriesAdminViewModel(User user, Window window)
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

        public CommandTemplate OpenMyReportsViewCommand
        {
            get
            {
                if (_openMyReportsView == null)
                    _openMyReportsView = new CommandTemplate(obj =>
                     {
                         GoToUserReportsForm();
                     });
                return _openMyReportsView;
            }
        }

        public CommandTemplate OpenCategoryEditViewCommand
        {
            get
            {
                if (_openCategoryEditViewCommand == null)
                    _openCategoryEditViewCommand = new CommandTemplate(obj =>
                    {
                        GoToCategoryEditView();
                    });
                return _openCategoryEditViewCommand;
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

        public void GoToUserReportsForm()
        {
            MyReportsView myReportsWindow = new MyReportsView();
            MyReportsViewModel myReportsViewModel = new MyReportsViewModel();

            _window.Hide();

            myReportsWindow.DataContext = myReportsViewModel;
            myReportsWindow.ShowDialog();

            _window.Show();
        }

        public void GoToCategoryEditView()
        {
            CategoryEditWindow categoryEditWindow = new CategoryEditWindow();
            CategoryEditViewModel categoryEditViewModel = new CategoryEditViewModel();

            _window.Hide();

            categoryEditWindow.DataContext = categoryEditViewModel;
            categoryEditWindow.ShowDialog();

            _window.Show();
        }

        public void SetUserName()
        {
            UserName = User.Nickname;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
