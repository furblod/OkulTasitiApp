using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OkulTasitiApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        // Ek özellikler eklenecekse buraya eklenebilir
        // Örneğin:
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ICollection<SchoolDriver> SchoolDrivers { get; set; }
    }
}
