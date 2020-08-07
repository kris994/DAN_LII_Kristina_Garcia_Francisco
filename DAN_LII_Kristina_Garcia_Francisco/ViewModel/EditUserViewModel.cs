using DAN_LII_Kristina_Garcia_Francisco.Commands;
using DAN_LII_Kristina_Garcia_Francisco.Model;
using DAN_LII_Kristina_Garcia_Francisco.View;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace DAN_LII_Kristina_Garcia_Francisco.ViewModel
{
    class EditUserViewModel : BaseViewModel
    {
        EditUser editUser;
        Service service = new Service();

        #region Constructor
        /// <summary>
        /// Constructor with edit user window opening
        /// </summary>
        /// <param name="addUseOpen">opens the edit user window</param>
        /// <param name="userEdit">gets the user info that is being edited</param>
        public EditUserViewModel(EditUser addUseOpen, tblUser userEdit)
        {
            user = userEdit;
            editUser = addUseOpen;
            UserList = service.GetAllUsers().ToList();
        }      
        #endregion

        #region Property
        /// <summary>
        /// List of users
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
      
        /// <summary>
        /// Specific User
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
        /// Cheks if its possible to execute the add and edit user commands
        /// </summary>
        private bool isUpdateUser;
        public bool IsUpdateUser
        {
            get
            {
                return isUpdateUser;
            }
            set
            {
                isUpdateUser = value;
            }
        }       
        #endregion

        #region Commands
        /// <summary>
        /// Command that tries to save the new user
        /// </summary>
        private ICommand save;
        public ICommand Save
        {
            get
            {
                if (save == null)
                {
                    save = new RelayCommand(param => SaveExecute(), param => this.CanSaveExecute);
                }
                return save;
            }
        }

        /// <summary>
        /// Tries the execute the save command
        /// </summary>
        private void SaveExecute()
        {
            try
            {
                service.AddUser(User);
                IsUpdateUser = true;

                editUser.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
            }
        }

        /// <summary>
        /// Checks if its possible to save the user
        /// </summary>
        protected bool CanSaveExecute
        {
            get
            {
                return User.IsValid;
            }
        }
      
        /// <summary>
        /// Command that closes the add worker or edit worker window
        /// </summary>
        private ICommand cancel;
        public ICommand Cancel
        {
            get
            {
                if (cancel == null)
                {
                    cancel = new RelayCommand(param => CancelExecute(), param => CanCancelExecute());
                }
                return cancel;
            }
        }

        /// <summary>
        /// Executes the close command
        /// </summary>
        private void CancelExecute()
        {
            try
            {
                editUser.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Checks if its possible to execute the close command
        /// </summary>
        /// <returns>true</returns>
        private bool CanCancelExecute()
        {
            return true;
        }
        #endregion
    }
}
