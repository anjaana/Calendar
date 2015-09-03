using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using Calendar.Factory.Model;

namespace Calendar.Client
{
    public partial class AddEvent : Window
    {
        private EventList parent;

        public AddEvent(EventList parent)
        {
            this.parent = parent;

            InitializeComponent();

            DatePickerStart.DisplayDateStart = DateTime.Now;
            DatePickerEnd.DisplayDateStart = DateTime.Now;
        }


        private void AddEventClick_Click(object sender, RoutedEventArgs e)
        {
            Event newEvent = new Event();

            if (string.IsNullOrEmpty(AddTextBOx.Text ))
            {
                Labelwarning1.Visibility = Visibility.Visible;
                return;
            }
            else
            {
                newEvent.Name = AddTextBOx.Text;
                Labelwarning1.Visibility = Visibility.Hidden;
            }

            if (DatePickerStart.SelectedDate == null)
            {
                Labelwarning2.Visibility = Visibility.Visible;
                return;
            }
            else
            {
                newEvent.StartDate = DatePickerStart.SelectedDate.Value;
                Labelwarning2.Visibility = Visibility.Hidden;
            }

            if (DatePickerEnd.SelectedDate == null)
            {
                Labelwarning3.Visibility =Visibility.Visible;
                return;
            }
            else
            {
                newEvent.EndDate = DatePickerEnd.SelectedDate.Value;
                Labelwarning3.Visibility = Visibility.Hidden;
            }

            if (!Factory.Factory.EventFactory.IsDatesExist(newEvent.StartDate, newEvent.EndDate, null))
            {
                Factory.Factory.EventFactory.AddEvent(newEvent);
                parent.InitializeDataGrid();
                Clear();
                this.Hide();
            }
            else
            {
                MessageBox.Show("These dates are already taken ! ", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;

            this.Hide();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DatePickerStart.SelectedDate > DatePickerEnd.SelectedDate)
            {
                if (e.RemovedItems.Count > 0)
                {
                    ((DatePicker) sender).SelectedDate = (DateTime) (e.RemovedItems)[0];
                }
                else
                {
                    ((DatePicker) sender).SelectedDate = null;
                }
                
                MessageBox.Show("Wrong Date", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Clear()
        {
            AddTextBOx.Text = string.Empty;
            DatePickerStart.SelectedDate = null;
            DatePickerEnd.SelectedDate = null;
        }
    }
}
