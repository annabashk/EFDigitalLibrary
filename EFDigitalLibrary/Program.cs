using System;

namespace EFDigitalLibrary
{ 
    public class Program
    {
        static void Main(string[] args)
        {
            using (var db = new AppContext())
            {
                var user1 = new User { Name = "Анна", Email = "anna@mail.ru" };

                db.Users.Add(user1);

                var book1 = new Book { Name = "Идиот", Year = "1821" };

                db.Books.Add(book1);

                db.SaveChanges();
            }
        }
    }
}