using DAN_LII_Kristina_Garcia_Francisco.Commands;
using DAN_LII_Kristina_Garcia_Francisco.Model;
using DAN_LII_Kristina_Garcia_Francisco.View;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;

namespace DAN_LII_Kristina_Garcia_Francisco.ViewModel
{
    class LoginViewModel : BaseViewModel
    {
        Login view;
        Service service = new Service();

        #region Constructor
        public LoginViewModel(Login loginView)
        {
            view = loginView;
            user = new tblUser();
            UserList = service.GetAllUsers().ToList();
        }
        #endregion

        #region Property
        /// <summary>
        /// Login info label
        /// </summary>
        private string infoLabel;
        public string InfoLabel
        {
            get
            {
                return infoLabel;
            }
            set
            {
                infoLabel = value;
                OnPropertyChanged("InfoLabel");
            }
        }

        /// <summary>
        /// Used for saving the current user
        /// </summary>
        private tblUser user;
        public tblUser User
        {
            get
            {
                return user;
            }
            set
            {
                user = value;
                OnPropertyChanged("User");
            }
        }

        /// <summary>
        /// List of all users in the application
        /// </summary>
        private List<tblUser> userList;
        public List<tblUser> UserList
        {
            get
            {
                return userList;
            }
            set
            {
                userList = value;
                OnPropertyChanged("UserList");
            }
        }
        #endregion

        #region Commands
        /// <summary>
        /// Command used to log te user into the application
        /// </summary>
        private ICommand login;
        public ICommand Login
        {
            get
            {
                if (login == null)
                {
                    login = new RelayCommand(LoginExecute);
                }
                return login;
            }
        }

        /// <summary>
        /// Checks if its possible to login depending on the given username and password and saves the logged in user to a list
        /// </summary>
        /// <param name="obj"></param>
        private void LoginExecute(object obj)
        {
            bool found = false;
            bool foundUsername = false;
            string password = (obj as PasswordBox).Password;

            for (int i = 0; i < UserList.Count; i++)
            {
                if (User.Username == UserList[i].Username && password == UserList[i].UserPassword)
                {
                    LoggedUser.CurrentUser = new tblUser
                    {
                        UserID = UserList[i].UserID,
                        Username = UserList[i].Username,
                        UserPassword = UserList[i].UserPassword,
                        FirstName = UserList[i].FirstName,
                        LastName = UserList[i].LastName,
                        UserAddress = UserList[i].UserAddress,
                        PhoneNumber = UserList[i].PhoneNumber
                    };

                    InfoLabel = "Logged in";
                    found = true;
                    UserWindow cake = new UserWindow();
                    view.Close();
                    cake.Show();
                    break;
                }
            }

            if (found == false)
            {
                for (int i = 0; i < UserList.Count; i++)
                {
                    if (User.Username == UserList[i].Username)
                    {
                        InfoLabel = "Username already exists";
                        foundUsername = true;
                        break;
                    }
                }

                if (foundUsername == false)
                {

                    InfoLabel = "Logged in";
                    User.UserPassword = password;
                    service.AddUser(User);
                    UserList.Add(User);

                    LoggedUser.CurrentUser = new tblUser
                    {
                        UserID = User.UserID,
                        Username = User.Username,
                        UserPassword = User.UserPassword
                    };

                    UserWindow cake = new UserWindow();
                    view.Close();
                    cake.Show();
                }
                else
                {
                    InfoLabel = "Wrong Username or Password";
                }

            }
        }
        #endregion
    }
}
