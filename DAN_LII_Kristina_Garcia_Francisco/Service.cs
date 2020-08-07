using DAN_LII_Kristina_Garcia_Francisco.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace DAN_LII_Kristina_Garcia_Francisco
{
    /// <summary>
    /// Class that includes all CRUD functions of the application
    /// </summary>
    class Service
    {
        /// <summary>
        /// Gets all information about users
        /// </summary>
        /// <returns>a list of found users</returns>
        public List<tblUser> GetAllUsers()
        {
            try
            {
                using (BirthdayDBEntities context = new BirthdayDBEntities())
                {
                    List<tblUser> list = new List<tblUser>();
                    list = (from x in context.tblUsers select x).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        /// <summary>
        /// Gets all information about items
        /// </summary>
        /// <returns>a list of found items</returns>
        public List<tblItem> GetAllItems()
        {
            try
            {
                using (BirthdayDBEntities context = new BirthdayDBEntities())
                {
                    List<tblItem> list = new List<tblItem>();
                    list = (from x in context.tblItems select x).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        /// <summary>
        /// Gets all information about shopping carts from the user
        /// </summary>
        /// <returns>a list of found shopping carts</returns>
        public List<tblShoppingCart> GetAllUserShoppingCarts(int UserID)
        {
            try
            {
                List<tblShoppingCart> list = new List<tblShoppingCart>();
                using (BirthdayDBEntities context = new BirthdayDBEntities())
                {
                    for (int i = 0; i < GetAllShoppingCarts().Count; i++)
                    {
                        if (GetAllShoppingCarts()[i].UserID == UserID)
                        {
                            list.Add(GetAllShoppingCarts()[i]);

                        }
                    }
                    return list;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }


        /// <summary>
        /// Gets all information about all shopping carts
        /// </summary>
        /// <returns>a list of found shopping cart</returns>
        public List<tblShoppingCart> GetAllShoppingCarts()
        {
            try
            {
                using (BirthdayDBEntities context = new BirthdayDBEntities())
                {
                    List<tblShoppingCart> list = new List<tblShoppingCart>();
                    list = (from x in context.tblShoppingCarts select x).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        /// <summary>
        /// Gets all information about orders from the user
        /// </summary>
        /// <returns>a list of found orders</returns>
        public List<tblOrder> GetAllUserOrders(int UserID)
        {
            try
            {
                List<tblOrder> list = new List<tblOrder>();
                using (BirthdayDBEntities context = new BirthdayDBEntities())
                {
                    for (int i = 0; i < GetAllOrders().Count; i++)
                    {
                        if (GetAllOrders()[i].UserID == UserID)
                        {
                            list.Add(GetAllOrders()[i]);

                        }
                    }
                    return list;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }


        /// <summary>
        /// Gets all information about all orders
        /// </summary>
        /// <returns>a list of found orders</returns>
        public List<tblOrder> GetAllOrders()
        {
            try
            {
                using (BirthdayDBEntities context = new BirthdayDBEntities())
                {
                    List<tblOrder> list = new List<tblOrder>();
                    list = (from x in context.tblOrders select x).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        /// <summary>
        /// Searches for a user with the given username, returns 0 if it does not exist
        /// </summary>
        /// <param name="username">Checks if the user with that Username exists</param>
        /// <returns>The found user id</returns>
        public int FindUserByUsername(string username)
        {
            for (int i = 0; i < GetAllUsers().Count; i++)
            {
                if (GetAllUsers()[i].Username == username)
                {
                    return GetAllUsers()[i].UserID;
                }
            }
            return 0;
        }

        /// <summary>
        /// Checks if the shopping cart exists
        /// </summary>
        /// <param name="itemID">Id of the item in the cart</param>
        /// <param name="userID">Id of the user that owns the cart</param>
        /// <returns>the shoppingcart id</returns>
        public int CartExists(int itemID, int userID)
        {
            int cartId = 0;

            if (GetAllUserShoppingCarts(userID).Any())
            {
                for (int i = 0; i < GetAllUserShoppingCarts(userID).Count; i++)
                {
                    if (GetAllUserShoppingCarts(userID)[i].ItemID == itemID)
                    {
                        cartId = GetAllUserShoppingCarts(userID)[i].ShoppingCartID;
                    }
                }
            }

            return cartId;
        }

        /// <summary>
        /// Checks the amount of items in the cart
        /// </summary>
        /// <param name="cartID">The cart id</param>
        /// <returns>the total amount of items in the cart</returns>
        public int CurrentCartItemAmount(int cartID)
        {
            int currentAmount = 0;

            for (int i = 0; i < GetAllShoppingCarts().Count; i++)
            {
                if (GetAllShoppingCarts()[i].ShoppingCartID == cartID)
                {
                    currentAmount = (int)GetAllShoppingCarts()[i].Amount;
                }
            }

            return currentAmount;
        }

        /// <summary>
        /// Adds the item if the itemID exists
        /// </summary>
        /// <param name="userID">the item that is being added for the user</param>
        /// <param name="item">the item that is being added</param> 
        /// <returns>a new shopping cart element</returns>
        public tblShoppingCart AddItem(tblItem item, int userID)
        {
            int cartId = CartExists(item.ItemID, userID);

            try
            {
                using (BirthdayDBEntities context = new BirthdayDBEntities())
                {
                    if (cartId == 0)
                    {
                        tblShoppingCart newShoppingCart = new tblShoppingCart
                        {
                            Amount = item.Amount,
                            UserID = userID,
                            ItemID = item.ItemID
                        };

                        context.tblShoppingCarts.Add(newShoppingCart);
                        context.SaveChanges();

                        return newShoppingCart;
                    }
                    else
                    {
                        tblShoppingCart shoppingCartToEdit = (from ss in context.tblShoppingCarts where ss.ShoppingCartID == cartId select ss).First();

                        shoppingCartToEdit.Amount = item.Amount;
                        shoppingCartToEdit.UserID = shoppingCartToEdit.UserID;
                        shoppingCartToEdit.ItemID = shoppingCartToEdit.ItemID;

                        shoppingCartToEdit.ShoppingCartID = cartId;
                        context.SaveChanges();

                        return shoppingCartToEdit;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        /// <summary>
        /// Adds the user
        /// </summary>
        /// <param name="user">the user that is being added</param> 
        /// <returns>a new user</returns>
        public tblUser AddUser(tblUser user)
        {
            try
            {
                using (BirthdayDBEntities context = new BirthdayDBEntities())
                {
                    if (FindUserByUsername(user.Username) == 0)
                    {
                        tblUser newUser = new tblUser
                        {
                            Username = user.Username,
                            UserPassword = user.UserPassword
                        };

                        context.tblUsers.Add(newUser);
                        context.SaveChanges();
                        user.UserID = newUser.UserID;

                        return newUser;
                    }
                    else
                    {
                        //TODO user edit
                        tblUser userToEdit = (from ss in context.tblUsers where ss.UserID == user.UserID select ss).First();

                        userToEdit.FirstName = user.FirstName;
                        userToEdit.LastName = user.LastName;
                        userToEdit.UserAddress = user.UserAddress;
                        userToEdit.PhoneNumber = user.PhoneNumber;
                        userToEdit.Username = LoggedUser.CurrentUser.Username;
                        userToEdit.UserPassword = LoggedUser.CurrentUser.UserPassword;

                        userToEdit.UserID = user.UserID;
                        context.SaveChanges();

                        return user;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        /// <summary>
        /// Empries the shopping cart of the user
        /// </summary>
        /// <param name="userID">users whoes shopping cart we are empting</param>
        public void EmptyShoppingCart(int userID)
        {
            try
            {
                using (BirthdayDBEntities context = new BirthdayDBEntities())
                {
                    int shoppingChartCount = GetAllUserShoppingCarts(userID).Count;
                    for (int i = 0; i < shoppingChartCount; i++)
                    {
                        tblShoppingCart shoppingCartToRemove = (from r in context.tblShoppingCarts
                                                                where r.UserID == userID
                                                                select r).First();

                        context.tblShoppingCarts.Remove(shoppingCartToRemove);
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
            }
        }

        /// <summary>
        /// Adds the order if the userID exists
        /// </summary>
        /// <param name="userID">oreder for the specific user</param>
        /// <returns>a new order</returns>
        public tblOrder AddOrder(int userID)
        {
            try
            {
                using (BirthdayDBEntities context = new BirthdayDBEntities())
                {
                    tblOrder newOrder = new tblOrder
                    {
                        TotalPrice = TotalValue(),
                        TotalCakesOrdered = TotalAmount(),
                        AllCakes = AllCakesInCart(),
                        OrderCreated = DateTime.Now.Date,
                        UserID = userID
                    };

                    context.tblOrders.Add(newOrder);
                    EmptyShoppingCart(userID);

                    context.SaveChanges();

                    return newOrder;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }
       
        /// <summary>
        /// Deletes item from shopping chart
        /// </summary>
        /// <param name="item">the item that is being deleted</param>
        /// <param name="userID">the user that has the item</param>
        public void RemoveItem(tblItem item, int userID)
        {
            int cartId = CartExists(item.ItemID, userID);

            try
            {
                using (BirthdayDBEntities context = new BirthdayDBEntities())
                {
                    tblShoppingCart shoppingCartToRemove = (from r in context.tblShoppingCarts where r.ShoppingCartID == cartId select r).First();

                    context.tblShoppingCarts.Remove(shoppingCartToRemove);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
            }
        }
       
        /// <summary>
        /// Calculates the total value of items in the cart
        /// </summary>
        /// <returns>The total value</returns>
        public string TotalValue()
        {
            Service service = new Service();

            double orderPrice = 0;

            for (int i = 0; i < service.GetAllUserShoppingCarts(LoggedUser.CurrentUser.UserID).Count; i++)
            {
                int index = service.GetAllItems().FindIndex(f => f.ItemID == service.GetAllUserShoppingCarts(LoggedUser.CurrentUser.UserID)[i].ItemID);
                double price = double.Parse(service.GetAllItems()[index].Price);
                orderPrice = orderPrice + (double)service.GetAllUserShoppingCarts(LoggedUser.CurrentUser.UserID)[i].Amount * price;
            }
            return orderPrice.ToString();
        }

        /// <summary>
        /// Calculates the total amount of items in the cart
        /// </summary>
        /// <returns>The total value</returns>
        public int TotalAmount()
        {
            Service service = new Service();

            int amount = 0;

            for (int i = 0; i < service.GetAllUserShoppingCarts(LoggedUser.CurrentUser.UserID).Count; i++)
            {
                amount++;
            }
            return amount;
        }

        /// <summary>
        /// Gets all types of cakes
        /// </summary>
        /// <returns>The total value</returns>
        public string AllCakesInCart()
        {
            Service service = new Service();
            string cakeNames = "";
            for (int i = 0; i < service.GetAllUserShoppingCarts(LoggedUser.CurrentUser.UserID).Count; i++)
            {
                int index = service.GetAllItems().FindIndex(f => f.ItemID == service.GetAllUserShoppingCarts(LoggedUser.CurrentUser.UserID)[i].ItemID);
                cakeNames = service.GetAllItems()[index].ItemName + " ";
            }

            return cakeNames;
        }
    }
}
