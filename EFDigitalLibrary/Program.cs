using System;
using System.Collections.Generic;
using System.Runtime.Intrinsics.X86;

namespace EFDigitalLibrary
{ 
    public class Program
    {
        static void Main(string[] args)
        {
            using (AppContext db = new AppContext())
            {
                BookRepository bookRepository = new(db);
                UserRepository userRepository = new(db);

                var user1 = new User() { Name = "user1", Email = "email@mail.ru" };
                var user2 = new User() { Name = "user2", Email = "email@mail.ru" };

                var book1 = new Book() { Name = "book1", Year = 2000 };
                var book2 = new Book() { Name = "book2", Year = 2000 };
                var book3 = new Book() { Name = "book3", Year = 2000 };

                var author1 = new Author() { Name = "author1" };
                var author2 = new Author() { Name = "author2" };

                var genre1 = new Genre() { Name = "genre1" };
                var genre2 = new Genre() { Name = "genre2" };

                book1.Genres.Add(genre1);
                book2.Genres.Add(genre2);
                book3.Genres.Add(genre1);

                book1.Author.Add(author1);
                book2.Author.Add(author2);
                book3.Author.AddRange(new Author[2] { author1, author2 });

                book1.Users.Add(user1);
                book2.Users.Add(user2);
                book3.Users.AddRange(new User[2] { user1, user2 });

                db.Users.AddRange(user1, user2);
                db.Books.AddRange(book1,book2, book3);
                db.Authors.AddRange(author1, author2);
                db.Genres.AddRange(genre1, genre2);
                db.SaveChanges();

                string input;
                Book book;
                User user;
                int min, max, count;

                while(true)
                {
                    input = Console.ReadLine();
                    switch (input)
                    {
                        case "GetByIdBook":
                            Console.WriteLine("Введите id: ");
                            input = Console.ReadLine();
                            book = bookRepository.GetByIdBook(Int32.Parse(input));
                            Console.WriteLine(book.Name);
                            break;
                        case "GetByIdUser":
                            Console.WriteLine("Введите id: ");
                            input = Console.ReadLine();
                            user = userRepository.GetByIdUser(Int32.Parse(input));
                            Console.WriteLine(user.Name);
                            break;
                        case "GetAllBooks":
                            var books = bookRepository.GetAllBooks();
                            foreach (var b in books)
                                Console.WriteLine($"{b.Id}, {b.Name}, {b.Year}");
                            break;
                        case "GetAllUsers":
                            var users = userRepository.GetAllUsers();
                            foreach (var u in users)
                                Console.WriteLine($"{u.Id}, {u.Name}");
                            break;

                                                            
                    }
                }
            }
        }
    }
}