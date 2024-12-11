using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OkulTasitiApp.Models
{
    public class Student
    {
        [Key]
        public int StudentID { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [StringLength(100)]
        public string GuardianName { get; set; }

        [Required]
        [StringLength(255)]
        public string Address { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public bool ShiftType { get; set; } // True: Sabahçı, False: Öğlenci

        // Foreign Key to the School table
        public int SchoolID { get; set; }

        // Navigation property to the School
        public School School { get; set; }

        public bool IsComingToday { get; set; }

        // Şoförle ilişkiyi kurmak için DriverID ve Driver
        public string DriverID { get; set; }


    }

}
