using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using Calendar.Factory.Model;

namespace Calendar.Client
{
    public partial class EditEvent : Window
    {
        private Event currentEvent;
        private EventList parent;

        public Event CurrentEvent
        {
            get
            {
                return currentEvent;
            }
            set
            {
                currentEvent = value;
                this.UpdateForm();
            }
        }

        public EditEvent(EventList parent)
        {
            this.parent = parent;

            InitializeComponent();

            DatePickerStart.DisplayDateStart = DateTime.Now;
            DatePickerEnd.DisplayDateStart = DateTime.Now;
        }

        public void UpdateForm()
        {
            txtEditEventName.Text = currentEvent.Name;
            DatePickerStart.SelectedDate = currentEvent.StartDate;
            DatePickerEnd.SelectedDate = currentEvent.EndDate;
        }

        private void btnEditClose_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void btnEditEvent_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtEditEventName.Text))
            {
                EditLabel.Visibility = Visibility.Visible;
                return;
            }
            else
            {
                EditLabel.Visibility = Visibility.Hidden;
            }

            UpdateEvent();
            if (!Factory.Factory.EventFactory.IsDatesExist(currentEvent.StartDate, currentEvent.EndDate, currentEvent.Id))
            {
                Factory.Factory.EventFactory.UpdateEvent(currentEvent);
                parent.InitializeDataGrid();
                this.Hide();
            }
            else
            {
                MessageBox.Show("These dates are already taken ! ", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateEvent()
        {
            if (DatePickerStart.SelectedDate.HasValue && DatePickerEnd.SelectedDate.HasValue &&
                !string.IsNullOrEmpty(txtEditEventName.Text))
            {
                currentEvent.Name = txtEditEventName.Text;
                currentEvent.StartDate = DatePickerStart.SelectedDate.Value;
                currentEvent.EndDate = DatePickerEnd.SelectedDate.Value;
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;

            this.Hide();
       }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DatePickerStart.SelectedDate > DatePickerEnd.SelectedDate)
            {
                if (e.RemovedItems.Count > 0)
                {
                    ((DatePicker)sender).SelectedDate = (DateTime)(e.RemovedItems)[0];
                }
                else
                {
                    ((DatePicker)sender).SelectedDate = null;
                }

                MessageBox.Show("Wrong Date", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
