using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FitnesClubWpf
{
    /// <summary>
    /// Логика взаимодействия для UserUpdate.xaml
    /// </summary>
    public partial class UserUpdate : Window
    {
        public Visitor Visitor { get; set; }
        public UserUpdate(Visitor v)
        {
            InitializeComponent();
            DataContext = new UserUpdateViewmodel(v, this);
        }
        public void Close(Visitor v)
        {
            Visitor = v;
            base.Close();
        }
    }
}
