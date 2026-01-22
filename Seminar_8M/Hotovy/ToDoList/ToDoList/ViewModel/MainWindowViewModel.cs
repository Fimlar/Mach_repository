using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Converters;
using System.Windows.Documents;
using ToDoList.Model;
using ToDoList.MVVM;

namespace ToDoList.ViewModel
{
    internal class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            Items = new ObservableCollection<Item>();
        }


        // data grid logic
        public ObservableCollection<Item> Items { get; set; }


        private Item selectedItem;
        public Item SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                OnPropertyChanged();
            }
        }

        private string taskName {  get; set; }
        public string TaskName
        {
            get { return taskName; }
            set
            {
                taskName = value;
                OnPropertyChanged();
            }
        }

        public string DeadlineText { get; set; } = "";

        // command logic

        public RelayCommand AddCommand => new RelayCommand(execute => AddItem());

        public RelayCommand DeleteCommand => new RelayCommand(execute => DeleteItem(), canExecute => SelectedItem != null);


        private void AddItem()
        {
            if (!DateOnly.TryParse(DeadlineText, out var deadline))
            {
                MessageBox.Show("Zadej platné datum");
                return;
            }

            Items.Add(new Item()
            {
                Name = TaskName,
                Deadline = deadline
            });
        }

        private void DeleteItem()
        {
            Items.Remove(selectedItem);
        }
    }
}
