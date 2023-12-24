using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagement.Data;
using LibraryManagement.Models;

namespace LibraryManagement.Controllers
{
    public class AccountController : Controller
    {
        private readonly LibraryManagementContext _context;

        public AccountController(LibraryManagementContext context)
        {
            _context = context;
        }

        // Diğer eylemler: Login, Register gibi metodlar buraya eklenebilir.

        // GET: Account/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]       
        public IActionResult Login(string email, string password, bool rememberMe)
        {
            // Kullanıcı doğrulama işlemleri burada gerçekleştirilecek
            var user = _context.CustomerModel.FirstOrDefault(u => u.Email == email && u.Password == password);

            if (user != null)
            {
                // Kullanıcı doğrulandıysa, oturum açılabilir veya başka bir işlem yapılabilir
                // Örneğin, oturum açma işlemleri için HttpContext.SignInAsync kullanılabilir
                return RedirectToAction("Index", "Home"); // Örneğin, Ana sayfaya yönlendirme
            }

            // Doğrulama başarısız ise, hata mesajıyla birlikte tekrar giriş sayfasını göster
            ModelState.AddModelError(string.Empty, "Invalid email or password");
            return View(); // veya login sayfasının adı
        }


        // GET: Account/Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("Id,Name,SurName,Email,Password")] CustomerModel customerModel)
        {
            if (ModelState.IsValid)
            {
                // Eğer aynı e-posta ile kayıtlı bir kullanıcı yoksa kaydı ekle
                if (!_context.CustomerModel.Any(u => u.Email == customerModel.Email))
                {
                    _context.Add(customerModel);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Privacy", "Home"); // Kayıt olduktan sonra giriş sayfasına yönlendirme
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Bu e-posta zaten kayıtlı.");
                }
            }
            return View(customerModel);
        }
    }
}
