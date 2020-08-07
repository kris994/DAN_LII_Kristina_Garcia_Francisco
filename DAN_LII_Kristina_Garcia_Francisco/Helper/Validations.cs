using DAN_LII_Kristina_Garcia_Francisco.Model;
using System.Collections.Generic;

namespace DAN_LII_Kristina_Garcia_Francisco.Helper
{
    /// <summary>
    /// Validates the user inputs
    /// </summary>
    class Validations
    {
        /// <summary>
        /// Checks if the Username is exists
        /// </summary>
        /// <param name="username">the username we are checking</param>
        /// <param name="id">for the specific user</param>
        /// <returns>null if the input is correct or string error message if its wrong</returns>
        public string UsernameChecker(string username, int id)
        {
            Service service = new Service();

            List<tblUser> AllUsers = service.GetAllUsers();
            string currectUsername = "";

            if (username == null)
            {
                return "Username cannot be empty.";
            }
            // Get the current users username
            for (int i = 0; i < AllUsers.Count; i++)
            {
                if (AllUsers[i].UserID == id)
                {
                    currectUsername = AllUsers[i].Username;
                    break;
                }
            }

            // Check if the username already exists, but it is not the current user username
            for (int i = 0; i < AllUsers.Count; i++)
            {
                if ((AllUsers[i].Username == username && currectUsername != username))
                {
                    return "This Username already exists!";
                }
            }

            return null;
        }

        /// <summary>
        /// Checks if the phone number is correct
        /// </summary>
        /// <param name="phone">the phone we are checking</param>
        /// <param name="id">for the specific user id</param>
        /// <returns>phone if the input is correct or null if its wrong</returns>
        public string PhoneNumber(string phone, int id)
        {
            Service service = new Service();
            string correctPhone = "";

            if (phone == null || phone.Length < 4)
            {
                return "Value has to be at least 4 characters long.";
            }

            // Get the current users phone
            for (int i = 0; i < service.GetAllUsers().Count; i++)
            {
                if (service.GetAllUsers()[i].UserID == id)
                {
                    correctPhone = service.GetAllUsers()[i].PhoneNumber;
                    break;
                }
            }

            // Check if the username already exists, but it is not the current user username
            for (int i = 0; i < service.GetAllUsers().Count; i++)
            {
                if ((service.GetAllUsers()[i].PhoneNumber == phone && correctPhone != phone))
                {
                    return "This Phone Number already exists!";
                }
            }

            return null;
        }
    }
}
