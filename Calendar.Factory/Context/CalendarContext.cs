using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calendar.Factory.Model;

namespace Calendar.Factory.Context
{
    internal class CalendarContext : DbContext
    {
        public DbSet<Event> Events { get; set; } 
    }
}
