using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryManagement.Data;
using LibraryManagement.Models;
using Microsoft.Extensions.Hosting;



namespace LibraryManagement.Controllers
{
    public class LibraryModelsController : Controller
    {
        private readonly LibraryManagementContext _context;

        public LibraryModelsController(LibraryManagementContext context)
        {
            _context = context;
        }

        // GET: LibraryModels
        public async Task<IActionResult> Index()
        {
            var libraryModels = await _context.LibraryModel.ToListAsync();
            return View(libraryModels);
        }
        public IActionResult ViewPdf(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var libraryModel = _context.LibraryModel.FirstOrDefault(m => m.Id == id);
            if (libraryModel == null || string.IsNullOrEmpty(libraryModel.Address))
            {
                return NotFound();
            }

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", libraryModel.Address);
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            // Dosyayı oku ve byte dizisine dönüştür
            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);

            // View'e model olarak byte dizisini gönder
            return View(fileBytes);
        }
        public IActionResult ViewPhoto(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var libraryModel = _context.LibraryModel.FirstOrDefault(m => m.Id == id);
            if (libraryModel == null || string.IsNullOrEmpty(libraryModel.Photo))
            {
                return NotFound();
            }

            var photoPath = libraryModel.Photo;
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", photoPath);

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            return PhysicalFile(filePath, "image/jpeg"); // ya da resmin türüne göre ayarlayın
        }


        private readonly IWebHostEnvironment _hostEnvironment;
       

        // GET: LibraryModels/Details/5

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var libraryModel = await _context.LibraryModel
                .FirstOrDefaultAsync(m => m.Id == id);

            if (libraryModel == null)
            {
                return NotFound();
            }

            return View(libraryModel);
        }

        // GET: LibraryModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LibraryModels/Create
        private string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                   + "_"
                   + Guid.NewGuid().ToString()
                   + Path.GetExtension(fileName);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,ReleaseDate,Genre")] LibraryModel libraryModel, IFormFile photo, IFormFile address)
        {
            if (ModelState.IsValid)
            {
                if (photo != null && photo.Length > 0)
                {
                    var photoFileName = GetUniqueFileName(photo.FileName);
                    var photoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", photoFileName);
                    using (var fileStream = new FileStream(photoPath, FileMode.Create))
                    {
                        await photo.CopyToAsync(fileStream);
                    }
                    libraryModel.Photo = "uploads/" + photoFileName; // Dosya yolunu kaydet
                }

                if (address != null && address.Length > 0)
                {
                    var addressFileName = GetUniqueFileName(address.FileName);
                    var addressPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", addressFileName);
                    using (var fileStream = new FileStream(addressPath, FileMode.Create))
                    {
                        await address.CopyToAsync(fileStream);
                    }
                    libraryModel.Address = "uploads/" + addressFileName; // Dosya yolunu kaydet
                }

                libraryModel.ReleaseDate = libraryModel.ReleaseDate.ToUniversalTime();
                _context.Add(libraryModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(libraryModel);
        }

        // GET: LibraryModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var libraryModel = await _context.LibraryModel.FindAsync(id);

            if (libraryModel == null)
            {
                return NotFound();
            }
            return View(libraryModel);
        }

        // POST: LibraryModels/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ReleaseDate,Genre,Photo,Address")] LibraryModel libraryModel, IFormFile photo, IFormFile address)
        {
            if (id != libraryModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingModel = await _context.LibraryModel.FindAsync(id);
                    if (existingModel == null)
                    {
                        return NotFound();
                    }

                    existingModel.Title = libraryModel.Title;
                    libraryModel.ReleaseDate = libraryModel.ReleaseDate.ToUniversalTime();
                    existingModel.Genre = libraryModel.Genre;

                    if (photo != null && photo.Length > 0)
                    {
                        var photoFileName = GetUniqueFileName(photo.FileName);
                        var photoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", photoFileName);
                        using (var fileStream = new FileStream(photoPath, FileMode.Create))
                        {
                            await photo.CopyToAsync(fileStream);
                        }
                        existingModel.Photo = "uploads/" + photoFileName; // Fotoğraf dosya yolunu güncelle
                    }

                    if (address != null && address.Length > 0)
                    {
                        var addressFileName = GetUniqueFileName(address.FileName);
                        var addressPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", addressFileName);
                        using (var fileStream = new FileStream(addressPath, FileMode.Create))
                        {
                            await address.CopyToAsync(fileStream);
                        }
                        existingModel.Address = "uploads/" + addressFileName; // Adres dosya yolunu güncelle
                    }

                    _context.Update(existingModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LibraryModelExists(libraryModel.Id))
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
            return View(libraryModel);
        }


        // GET: LibraryModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var libraryModel = await _context.LibraryModel
                .FirstOrDefaultAsync(m => m.Id == id);

            if (libraryModel == null)
            {
                return NotFound();
            }

            return View(libraryModel);
        }

        // POST: LibraryModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var libraryModel = await _context.LibraryModel.FindAsync(id);
            _context.LibraryModel.Remove(libraryModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LibraryModelExists(int id)
        {
            return _context.LibraryModel.Any(e => e.Id == id);
        }
    }
}