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
    class UserViewModel : BaseViewModel
    {
        UserWindow userWindow;
        Service service = new Service();

        #region Constructor
        /// <summary>
        /// Constructor with UserWindow param
        /// </summary>
        /// <param name="UserWindow">opens the users window</param>
        public UserViewModel(UserWindow usersOpen)
        {
            userWindow = usersOpen;
            OrderList = service.GetAllUserOrders(LoggedUser.CurrentUser.UserID).ToList();
            ItemList = service.GetAllItems().ToList();
            ShoppingCartList = service.GetAllUserShoppingCarts(LoggedUser.CurrentUser.UserID).ToList();
            UserList = service.GetAllUsers().ToList();
            InfoText();
            CheckIfCartEmpty();
        }
        #endregion

        #region Property
        /// <summary>
        /// List of all Items
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
        /// List of all Items
        /// </summary>
        private List<tblItem> itemList;
        public List<tblItem> ItemList
        {
            get
            {
                return itemList;
            }
            set
            {
                itemList = value;
                OnPropertyChanged("ItemList");
            }
        }

        /// <summary>
        /// Specific Item
        /// </summary>
        private tblItem item;
        public tblItem Item
        {
            get
            {
                return item;
            }
            set
            {
                item = value;
                OnPropertyChanged("Item");
            }
        }

        /// <summary>
        /// List of all shopping carts
        /// </summary>
        private List<tblShoppingCart> shoppingCartList;
        public List<tblShoppingCart> ShoppingCartList
        {
            get
            {
                return shoppingCartList;
            }
            set
            {
                shoppingCartList = value;
                OnPropertyChanged("ShoppingCartList");
            }
        }
      
        /// <summary>
        /// Specific Shopping Cart
        /// </summary>
        private tblShoppingCart shoppingCart;
        public tblShoppingCart ShoppingCart
        {
            get
            {
                return shoppingCart;
            }
            set
            {
                shoppingCart = value;
                OnPropertyChanged("ShoppingCart");
            }
        }
       
        /// <summary>
        /// List of all user orders
        /// </summary>
        private List<tblOrder> orderList;
        public List<tblOrder> OrderList
        {
            get
            {
                return orderList;
            }
            set
            {
                orderList = value;
                OnPropertyChanged("OrderList");
            }
        }
       
        /// <summary>
        /// Specific Order
        /// </summary>
        private tblOrder itemOrder;
        public tblOrder ItemOrder
        {
            get
            {
                return itemOrder;
            }
            set
            {
                itemOrder = value;
                OnPropertyChanged("ItemOrder");
            }
        }

        /// <summary>
        /// Total amount of items label
        /// </summary>
        private string totalLabel;
        public string TotalLabel
        {
            get
            {
                return totalLabel;
            }
            set
            {
                totalLabel = value;
                OnPropertyChanged("TotalLabel");
            }
        }

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

        private Visibility cartVisibility;
        public Visibility CartVisibility
        {
            get
            {
                return cartVisibility;
            }
            set
            {
                cartVisibility = value;
                OnPropertyChanged("CartVisibility");
            }
        }
        #endregion

        public void InfoText()
        {
            if (LoggedUser.CurrentUser.UserAddress == null)
            {
                InfoLabel = "Please fill up the profile info before continuing.";
            }
            else
            {
                InfoLabel = "";
            }
        }

        public void CheckIfCartEmpty()
        {
            if (ShoppingCartList.Any())
            {
                CartVisibility = Visibility.Visible;
            }
            else
            {
                CartVisibility = Visibility.Collapsed;
            }
        }

        #region Commands       
        /// <summary>
        /// Command that tries to add or edit item
        /// </summary>
        private ICommand addItem;
        public ICommand AddItem
        {
            get
            {
                if (addItem == null)
                {
                    addItem = new RelayCommand(param => AddItemExecute(), param => CanAddItemExecute());
                }
                return addItem;
            }
        }

        /// <summary>
        /// Executes the add command
        /// </summary>
        public void AddItemExecute()
        {
            try
            {
                int itemID = Item.ItemID;
                service.AddItem(Item, LoggedUser.CurrentUser.UserID);
                ShoppingCartList = service.GetAllUserShoppingCarts(LoggedUser.CurrentUser.UserID).ToList();
                TotalLabel = service.TotalValue();
                CheckIfCartEmpty();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
            }
        }

        /// <summary>
        /// Checks if the item can be added
        /// </summary>
        /// <returns>true if possible</returns>
        public bool CanAddItemExecute()
        {
            if (LoggedUser.CurrentUser.UserAddress == null)
            {
                return false;
            }
            if (Item == null || Item.Amount == 0)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Command that tried to delete an item
        /// </summary>
        private ICommand removeItem;
        public ICommand RemoveItem
        {
            get
            {
                if (removeItem == null)
                {
                    removeItem = new RelayCommand(param => RemoveItemExecute(), param => CanRemoveItemExecute());
                }
                return removeItem;
            }
        }

        /// <summary>
        /// Executes the add command
        /// </summary>
        public void RemoveItemExecute()
        {
            try
            {
                if (Item != null)
                {
                    service.RemoveItem(Item, LoggedUser.CurrentUser.UserID);
                    ShoppingCartList.RemoveAll(i => i.UserID == LoggedUser.CurrentUser.UserID && i.ItemID == Item.ItemID);
                    ShoppingCartList = service.GetAllUserShoppingCarts(LoggedUser.CurrentUser.UserID).ToList();
                    TotalLabel = service.TotalValue();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Checks if the item can be added
        /// </summary>
        /// <returns>true if possible</returns>
        public bool CanRemoveItemExecute()
        {
            if (LoggedUser.CurrentUser.UserAddress == null)
            {
                return false;
            }
            if (Item == null || Item.Amount == 0)
            {
                return false;
            }

            return true;
        }
        
        /// <summary>
        /// Command that tries to order
        /// </summary>
        private ICommand order;
        public ICommand Order
        {
            get
            {
                if (order == null)
                {
                    order = new RelayCommand(param => OrderExecute(), param => CanOrderExecute());
                }
                return order;
            }
        }

        /// <summary>
        /// Executes the order command
        /// </summary>
        public void OrderExecute()
        {
            try
            {
                service.AddOrder(LoggedUser.CurrentUser.UserID);
                OrderList = service.GetAllUserOrders(LoggedUser.CurrentUser.UserID).ToList();

                ShoppingCartList.Clear();
                ShoppingCartList.RemoveAll(i => i.UserID == LoggedUser.CurrentUser.UserID);
                CheckIfCartEmpty();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
            }
        }
        
        /// <summary>
        /// Checks if the order can be added
        /// </summary>
        /// <returns>true if possible</returns>
        public bool CanOrderExecute()
        {
            if (LoggedUser.CurrentUser.UserAddress == null)
            {
                return false;
            }
            if (!ShoppingCartList.Any())
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Command that tries to open the edit employee window
        /// </summary>
        private ICommand editUser;
        public ICommand EditUser
        {
            get
            {
                if (editUser == null)
                {
                    editUser = new RelayCommand(param => EditUserExecute(), param => CanEditUserExecute());
                }
                return editUser;
            }
        }

        /// <summary>
        /// Executes the edit command
        /// </summary>
        public void EditUserExecute()
        {
            try
            {
                EditUser editUser = new EditUser(LoggedUser.CurrentUser);
                editUser.ShowDialog();
                InfoText();
                UserList = service.GetAllUsers().ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Checks if the report can be edited
        /// </summary>
        /// <returns>true if possible</returns>
        public bool CanEditUserExecute()
        {
            return true;
        }

        /// <summary>
        /// Command that logs off the user
        /// </summary>
        private ICommand logoff;
        public ICommand Logoff
        {
            get
            {
                if (logoff == null)
                {
                    logoff = new RelayCommand(param => LogoffExecute(), param => CanLogoffExecute());
                }
                return logoff;
            }
        }

        /// <summary>
        /// Executes the logoff command
        /// </summary>
        private void LogoffExecute()
        {
            try
            {
                Login login = new Login();
                userWindow.Close();

                login.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Checks if its possible to logoff
        /// </summary>
        /// <returns>true</returns>
        private bool CanLogoffExecute()
        {
            return true;
        }       
        #endregion
    }
}
