using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Data.Sql;
using FitnesClubWpf;
using WPFbase;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace FitnesClubWpf
{
    public class MainWindowViewmodel:OnPropertyChangedHandler
    {
        public MainWindowViewmodel()
        {
            //Refresh.Execute(123);
        }
        public Visitor selectedv => Visitors[selected.Item];
        public ChangingItem<int> selected { get; set; } = new ChangingItem<int>();
        public ICommand Refresh => new RelayCommand(o =>
        {
            var list = FitnesClubDB.SelectVisitors();
            Visitors = new ObservableCollection<Visitor>(list);
            OnPropertyChanged(nameof(Visitors));
        });
        public ICommand Add => new RelayCommand(o =>
        {
            UserUpdate win = new UserUpdate(new Visitor());
            if (win.ShowDialog()!=null)
            {
                if (win.Visitor != new Visitor())
                {
                    FitnesClubDB.Insert(win.Visitor); 
                    Refresh.Execute(1);
                }
            }
        });
        public ICommand Update => new RelayCommand(o =>
        {
            UserUpdate win = new UserUpdate(selectedv);
            if (win.ShowDialog() != null)
            {
                if (win.Visitor != new Visitor())
                {
                    FitnesClubDB.Update(win.Visitor);
                    Refresh.Execute(1);
                }
            }
        });
        public ICommand Delete => new RelayCommand(o =>
        {
            FitnesClubDB.Delete(selectedv);
            Refresh.Execute(1);
        });
        public ObservableCollection<Visitor> Visitors { get;set; }
    }
}   