using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using LibraryManagement.Data;
using System.Linq;

namespace LibraryManagement.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new LibraryManagementContext(
                serviceProvider.GetRequiredService<DbContextOptions<LibraryManagementContext>>()))
            {
                // Look for any books.
                if (context.LibraryModel.Any())
                {
                    return; // Veritabanı zaten başlatılmış
                }

                context.LibraryModel.AddRange(
                    new LibraryModel
                    {
                        Title = "When Harry Met Sally",
                        ReleaseDate = DateTime.Parse("1989-02-12").ToUniversalTime(),
                        Genre = "Romantic Comedy",
                        
                    },
                    new LibraryModel
                    {
                        Title = "Ghostbusters",
                        ReleaseDate = DateTime.Parse("1984-03-13").ToUniversalTime(),
                        Genre = "Comedy",
                        
                    },
                    new LibraryModel
                    {
                        Title = "Ghostbusters 2",
                        ReleaseDate = DateTime.Parse("1986-02-23").ToUniversalTime(),
                        Genre = "Comedy",
                        
                    },
                    new LibraryModel
                    {
                        Title = "Rio Bravo",
                        ReleaseDate = DateTime.Parse("1959-04-15").ToUniversalTime(),
                        Genre = "Western",
                        
                    }
                );
             
                context.SaveChanges();
            }
        }
    }
}
