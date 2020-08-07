using DAN_LII_Kristina_Garcia_Francisco.Model;
using DAN_LII_Kristina_Garcia_Francisco.ViewModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace DAN_LII_Kristina_Garcia_Francisco.View
{
    /// <summary>
    /// Interaction logic for EditUser.xaml
    /// </summary>
    public partial class EditUser : Window
    {
        /// <summary>
        /// Window constructor for editing user
        /// </summary>
        /// <param name="userEdit">user that is bing edited</param>
        public EditUser(tblUser userEdit)
        {
            InitializeComponent();
            this.DataContext = new EditUserViewModel(this, userEdit);
        }

        /// <summary>
        /// User can only imput numbers
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
