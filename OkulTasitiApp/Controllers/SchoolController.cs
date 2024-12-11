using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OkulTasitiApp.Data;
using OkulTasitiApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OkulTasitiApp.Controllers
{
    public class SchoolController : Controller
    {
        private readonly SchoolContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public SchoolController(SchoolContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        [Authorize]
        public async Task<IActionResult> Settings()
        {
            var driverId = _userManager.GetUserId(User); // Giriş yapan şoförün ID'sini alın
            var schools = await _context.SchoolDrivers
                                        .Where(sd => sd.DriverID == driverId)
                                        .Select(sd => sd.School)
                                        .ToListAsync();

            return View(schools); // Okulları view'a gönder
        }


    }
}
