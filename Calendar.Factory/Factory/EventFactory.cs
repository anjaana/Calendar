using System;
using System.Collections.Generic;
using System.Linq;
using Calendar.Factory.Context;
using Calendar.Factory.Model;

namespace Calendar.Factory.Factory
{
    public  static class EventFactory
    {
        public static List<Event> GetList()
        {
            using (var db = new CalendarContext())
            {
                return db.Events.ToList();
            }
        }
        
        public static Event AddEvent(Event newEvent)
        {
            using (var db = new CalendarContext())
            {
                var book = db.Events.Add(newEvent);
                db.SaveChanges();

                return book;
            }
        }

        public static Event UpdateEvent(Event newEvent)
        {
            using (var db = new CalendarContext())
            {
                var currentEvent = db.Events.FirstOrDefault(e => e.Id == newEvent.Id);
                currentEvent.Name = newEvent.Name;
                currentEvent.StartDate = newEvent.StartDate;
                currentEvent.EndDate = newEvent.EndDate;

                db.SaveChanges();
                return currentEvent;
            }
        }

        public static void DeleteEvent(Event eventToDelete)
        {
            using (var db = new CalendarContext())
            {
                var currentEvent = db.Events.FirstOrDefault(e => e.Id == eventToDelete.Id);
                db.Events.Remove(currentEvent);
                db.SaveChanges();
            }
        }

        public static bool IsDatesExist(DateTime from, DateTime to, int? eventId)
        {
            using (var db = new CalendarContext())
            {
                return db.Events
                    .Where(e => !eventId.HasValue || e.Id != eventId.Value)
                    .Any(e => (from >= e.StartDate && from <= e.EndDate)
                                          || (to >= e.StartDate && to <= e.EndDate)
                                          || (from <= e.StartDate && to >= e.EndDate));
            }
        }
    }
}
