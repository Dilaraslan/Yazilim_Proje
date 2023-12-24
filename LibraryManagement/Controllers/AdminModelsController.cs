using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibraryManagement.Data;
using LibraryManagement.Models;

namespace LibraryManagement.Controllers
{
    public class AdminModelsController : Controller
    {
        private readonly LibraryManagementContext _context;

        public AdminModelsController(LibraryManagementContext context)
        {
            _context = context;
        }

        // GET: AdminModels
        public async Task<IActionResult> Index()
        {
              return _context.AdminModel != null ? 
                          View(await _context.AdminModel.ToListAsync()) :
                          Problem("Entity set 'LibraryManagementContext.AdminModel'  is null.");
        }

       
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string username, string password)
        {
            // Kullanıcı doğrulama işlemi veritabanından alınacak şekilde düzenlenmelidir
            var user = _context.AdminModel.SingleOrDefault(u => u.Username == username && u.Password == password);

            if (user != null)
            {
                // Başarılı giriş, kullanıcıya yönlendirme yapılabilir
                return RedirectToAction("Dashboard", "AdminModels"); // Dashboard'a yönlendirilebilir
            }
            else
            {
                // Başarısız giriş, hata mesajı gösterilebilir
                ModelState.AddModelError(string.Empty, "Invalid username or password.");
                return View();
            }
        }
        [HttpGet]
        public IActionResult Dashboard()
        {
            return View();
        }

    }
}
