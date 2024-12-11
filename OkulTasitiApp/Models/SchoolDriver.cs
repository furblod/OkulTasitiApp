using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OkulTasitiApp.Models
{
    public class SchoolDriver
    {
        public int SchoolID { get; set; }
        public School School { get; set; }

        public string DriverID { get; set; }
        public ApplicationUser Driver { get; set; }
    }
}
