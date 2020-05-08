using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Data.Entities
{
    public class Journal
    {
        public int Id { get; set; }
        public string EntryBy { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public string Comment { get; set; }
    }
}
