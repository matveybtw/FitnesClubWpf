using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FitnesClubWpf;
using WPFbase;
namespace FitnesClubWpf
{
    public class UserUpdateViewmodel:OnPropertyChangedHandler
    {
        public Visitor visitor { get; set; }
        UserUpdate window;
        public UserUpdateViewmodel(Visitor v, UserUpdate uu)
        {
            window = uu;
            visitor = v;
            LastName.Item = v.LastName;
            FirstName.Item = v.FirstName;
            Birthday = v.Birthday;
            Phone.Item = v.Phone;
            Balance.Item = v.Balance.ToString();
            Birthday = DateTime.Now;
        }
        public ChangingItem<string> LastName { get; set; } = new ChangingItem<string>();
        public ChangingItem<string> FirstName { get; set; } = new ChangingItem<string>();
        public DateTime Birthday
        {
            get => bd;
            set
            {
                bd = value;
                OnPropertyChanged(nameof(Birthday));
            }
        }
        DateTime bd;

        public ChangingItem<string> Phone { get; set; } = new ChangingItem<string>();
        public ChangingItem<string> Balance { get; set; } = new ChangingItem<string>();
        public ICommand Save => new RelayCommand(o =>
        {
            visitor.LastName = LastName.Item;
            visitor.FirstName = FirstName.Item;
            visitor.Birthday = Birthday;
            visitor.Phone = Phone.Item;
            visitor.Balance = double.Parse(Balance.Item.Replace('.',','));
            window.Close(visitor);
        });
        public ICommand Cancel => new RelayCommand(o =>
        {
            window.Close(visitor);
        });
    }

}
