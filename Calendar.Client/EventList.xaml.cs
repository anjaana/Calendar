using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using Calendar.Factory.Model;

namespace Calendar.Client
{
    public partial class EventList : Window
    {
        private AddEvent addEventForm;

        private EditEvent editEventForm;

        public EventList()
        {
            InitializeComponent();
            InitializeDataGrid();

            addEventForm = new AddEvent(this);
            editEventForm = new EditEvent(this);
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            addEventForm.Show();
        }

        private void EditEvent_Click(object sender, RoutedEventArgs e)
        {
            var eventToEdit = (Event) ((Button) e.Source).DataContext;
            editEventForm.CurrentEvent = eventToEdit;
            editEventForm.Show();
        }

        private void DeleteEvent_Click(object sender, RoutedEventArgs e)
        {
            var eventToDelete = (Event) ((Button) e.Source).DataContext;
            Factory.Factory.EventFactory.DeleteEvent(eventToDelete);
            InitializeDataGrid();
        }

        public void InitializeDataGrid()
        {
            Cevents.BlackoutDates.Clear(); 
            var eventList = Factory.Factory.EventFactory.GetList();
            foreach (var eventItem in eventList)
            {
                Cevents.BlackoutDates.Add(new CalendarDateRange(eventItem.StartDate, eventItem.EndDate));
            }

            DgEvents.ItemsSource = eventList;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;

            Application.Current.Shutdown();
        }
    }
}
