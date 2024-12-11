using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OkulTasitiApp.Data;
using OkulTasitiApp.Models;

namespace OkulTasitiApp.Controllers
{
    public class StudentsController : Controller
    {
        private readonly SchoolContext _context;    
        private readonly UserManager<ApplicationUser> _userManager;

        [HttpPost]
        public IActionResult SaveAttendance(List<int> studentIds)
        {
            if (studentIds == null || !studentIds.Any())
            {
                return Json(new { success = false, message = "Öğrenci seçimi yapılmadı." });
            }

            foreach (var student in _context.Students)
            {
                student.IsComingToday = studentIds.Contains(student.StudentID);
            }

            _context.SaveChanges();

            return Json(new { success = true });
        }



        public IActionResult GetStudentsComingToday(string schoolName, string shiftType)
        {
            bool isMorningShift = shiftType == "morning";

            var studentsComingToday = _context.Students
                .Where(s => s.IsComingToday && s.School.Name == schoolName && s.ShiftType == isMorningShift)
                .Select(s => s.Address)
                .ToList();

            return Json(studentsComingToday);
        }

        public IActionResult GetStudentsForModal(int schoolId, bool shiftType)
        {
            var driverId = _userManager.GetUserId(User);

            var studentsForModal = _context.Students
                .Where(s => s.SchoolID == schoolId && s.ShiftType == shiftType && s.DriverID == driverId)
                .Select(s => new
                {
                    s.StudentID,
                    s.FirstName,
                    s.LastName,
                    s.IsComingToday
                })
                .ToList();

            return Json(studentsForModal);
        }



        [HttpPost]
        public IActionResult ResetIsComingToday()
        {
            var students = _context.Students.ToList();

            foreach (var student in students)
            {
                student.IsComingToday = true; // Reset all to true
            }

            _context.SaveChanges();

            return Json(new { success = true });
        }

        public JsonResult GetStudentsBySchool(int schoolId)
        {
            var students = _context.Students
                .Where(s => s.SchoolID == schoolId && s.IsComingToday == true)
                .ToList();

            return Json(students);
        }


        public async Task<JsonResult> GetSchoolAddress(int schoolId)
        {
            Console.WriteLine($"Requested schoolId: {schoolId}"); // schoolId'yi konsola yazdır
            var school = await _context.School.FindAsync(schoolId);
            if (school != null)
            {
                Console.WriteLine($"Found school: {school.Name}, Address: {school.Address}"); // Adres bulunursa yazdır
                return Json(school.Address);
            }
            Console.WriteLine($"No school found for schoolId: {schoolId}");
            return Json(null);
        }




        public IActionResult GetStudentsForMap(int schoolId, string shiftType)
        {
            var driverId = _userManager.GetUserId(User); // Şoför ID'sini al
            bool isMorningShift = shiftType == "morning"; // Sabah veya öğleden sonra servisini kontrol et

            var studentsForMap = _context.Students
                .Where(s => s.School.SchoolID == schoolId && s.ShiftType == isMorningShift && s.DriverID == driverId && s.IsComingToday == true)
                .Select(s => new
                {
                    s.StudentID,
                    s.FirstName,
                    s.LastName,
                    s.Address // Öğrenci adresi rota için gerekli
        })
                .ToList();

            if (studentsForMap.Any())
            {
                return Json(studentsForMap); // Öğrenci varsa listeyi döndür
            }
            else
            {
                Console.WriteLine("No students found for the selected school and shift.");
                return Json(new List<object>()); // Boş liste döndür
            }
        }
        [HttpPost]
        public JsonResult GetStudentAddresses(List<int> studentIds)
        {
            var studentAddresses = _context.Students
                .Where(s => studentIds.Contains(s.StudentID))
                .Select(s => s.Address)
                .ToList();

            return Json(studentAddresses);
        }


       




        public StudentsController(SchoolContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        //GET: Students
        public async Task<IActionResult> Index()
        {

            // Giriş yapan şoförün ID'sini al
            var driverId = _userManager.GetUserId(User);

            var students = await _context.Students
                                 .Include(s => s.School) // Okul ile ilişkiyi yükle
                                 .Where(s => s.DriverID == driverId)
                                 .ToListAsync();

            // Öğrencilerin gittiği okulları listele ve ViewBag'e ata
            ViewBag.Schools = students.Select(s => s.School).Distinct().ToList();



            return View(students);
        }



        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.StudentID == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create    
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            // ViewBag ile okulları dropdown için çekiyoruz
            ViewBag.Schools = new SelectList(await _context.School.ToListAsync(), "SchoolID", "Name");
            return View();
        }


        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentID,FirstName,LastName,GuardianName,Address,Price,ShiftType,SchoolID")] Student student)
        {
            if (ModelState.IsValid)
            {
                // Giriş yapan şoförün ID'sini öğrenciye ekle
                student.DriverID = _userManager.GetUserId(User);

                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            // Model geçerli değilse, okulları tekrar doldur
            ViewBag.Schools = new SelectList(await _context.School.ToListAsync(), "SchoolID", "Name");
            return View(student);
        }



        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();  
            }
            // Tüm okulları alıyoruz
            var schools = await _context.School.ToListAsync();
            ViewBag.Schools = new SelectList(schools, "SchoolID", "Name", student.SchoolID);
            return View(student);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StudentID,FirstName,LastName,GuardianName,Address,Price,ShiftType,SchoolID")] Student student)
        {
            if (id != student.StudentID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    student.DriverID = _userManager.GetUserId(User);

                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.StudentID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            // Eğer bir hata varsa okulları yeniden doldur
            var schools = await _context.School.ToListAsync();
            ViewBag.Schools = new SelectList(schools, "SchoolID", "Name", student.SchoolID);

            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.StudentID == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Students.FindAsync(id);
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.StudentID == id);
        }
    }
}
