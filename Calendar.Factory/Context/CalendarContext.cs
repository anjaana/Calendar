using System.Data.Entity;
using Calendar.Factory.Model;

namespace Calendar.Factory.Context
{
    internal class CalendarContext : DbContext
    {
        public DbSet<Event> Events { get; set; } 
    }
}
