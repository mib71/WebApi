using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required]
        public string EntryBy { get; set; }
        [Required]
        public string Date { get; set; } // Ej DateTime eftersom användaren skickar ett ogiltigt tidsformat
        [Required]
        public string Comment { get; set; }
                
    }
}
