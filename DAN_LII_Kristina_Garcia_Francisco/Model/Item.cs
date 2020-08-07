using DAN_LII_Kristina_Garcia_Francisco.ViewModel;

namespace DAN_LII_Kristina_Garcia_Francisco.Model
{
    public partial class tblItem : BaseViewModel
    {
        Service service = new Service();

        /// <summary>
        /// Item amount
        /// </summary>
        private int amount;
        public int Amount
        {
            get
            {
                if (amount <= 0)
                {
                    return 0;
                }
                return amount;
            }
            set
            {
                amount = value;
                OnPropertyChanged("Amount");
            }
        }
    }
}
