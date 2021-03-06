﻿using DAN_LII_Kristina_Garcia_Francisco.Model;

namespace DAN_LII_Kristina_Garcia_Francisco
{
    /// <summary>
    /// Current logged in user data
    /// </summary>
    public static class LoggedUser
    {
        /// <summary>
        /// Current user
        /// </summary>
        private static tblUser currentUser;
        public static tblUser CurrentUser
        {
            get { return currentUser; }
            set { currentUser = value; }
        }
    }
}
