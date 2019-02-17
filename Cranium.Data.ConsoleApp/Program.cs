using System;
using Cranium.Data.Models;
using Cranium.Data.Services;
using Microsoft.EntityFrameworkCore;

namespace Cranium.Data.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
            optionsBuilder.UseSqlite("Data Source=Cranium.db");

            using (var db = new DataContext(optionsBuilder.Options))
            {
                db.QuestionTypes.Add(new QuestionType
                {
                    Name = "Multi-Mimator",
                    Explanation = "Iedereen speelt mee met deze multi-Mimator! Kies een artiest uit elk team om stille hints uit te boolden. Het eerste team dat het antwoord raadt, mag meteen nog eens gooien. Als deze kaart werd getrokken tijdens jouw beurt, krijg je een andere kaart nadat de winnaar nog eens heeft gegooid.",
                });
                var count = db.SaveChanges();
                Console.WriteLine("{0} records saved to database", count);

                Console.WriteLine();
                Console.WriteLine("All blogs in database:");
                foreach (var blog in db.QuestionTypes)
                {
                    Console.WriteLine(" - {0}", blog.Name);
                }
            }
        }
    }
}
