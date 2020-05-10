using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Data.Entities
{
    public class Journal
    {
        public Journal()
        {

        }

        

        public int Id { get; set; }
        public string EntryBy { get; set; }
        public string Date { get; set; } // Ej DateTime eftersom användaren skickar ett ogiltigt tidsformat
        public string Comment { get; set; }
                
    }
}
