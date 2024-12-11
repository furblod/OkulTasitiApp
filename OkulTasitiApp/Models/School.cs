using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OkulTasitiApp.Models
{
    public class School
    {
        public int SchoolID { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(255)]
        public string Address { get; set; }

        // Şoförlerle ilişki
        public ICollection<SchoolDriver> SchoolDrivers { get; set; }

        public ICollection<Student> Students { get; set; }
    }

}
