using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDigitalLibrary
{
    public class BookRepository
    {
        
        private AppContext db;
        public BookRepository (AppContext db)
        {
            this.db = db;
        }

        /// <summary>
        /// выбор объекта из БД по его идентификатору
        /// </summary>
        public Book GetByIdBook(int id)
        {
            return db.Books.Find(id);
        }

        /// <summary>
        /// выбор всех объектов
        /// </summary>
        public IQueryable<Book> GetAllBooks()
        {
            return db.Books;
        }

        /// <summary>
        /// добавление объекта в БД
        /// </summary>
        public int AddBook(Book book)
        {
            db.Books.Add(book);
            return db.SaveChanges();
        }
        
        /// <summary>
        /// удаление объекта из БД
        /// </summary>
        public int DeleteBook(Book book)
        {
            db.Books.Remove(book);
            return db.SaveChanges();
        }

        /// <summary>
        /// обновление года выпуска книги
        /// </summary>
        public int UpdateYear(int id, int year)
        {
            var book = GetByIdBook(id);
            if (book == null)
                return 0;
            book.Year = year;
            return db.SaveChanges();
        }

        /// <summary>
        /// Получать список книг определенного жанра и вышедших между определенными годами
        /// </summary>
        public IQueryable<Book> GetAllByGenreAndYear (string genre, int minYear, int maxYear)
        {
            return from book in db.Books
                   where book.Genres.Any(g => g.Name == genre)
                   && book.Year >= minYear && book.Year <= maxYear
                   select book;
        }

        /// <summary>
        /// Получать количество книг определенного автора в библиотеке
        /// </summary>
        public int CountByAuthor (string author)
        {
            return (from book in db.Books 
                    where book.Author.Any(a => a.Name == author) 
                    select book).Count();
        }

        /// <summary>
        /// Получать количество книг определенного жанра в библиотеке.
        /// </summary>
        public int CountByGenre (string genre)
        {
            return (from book in db.Books
                    where book.Genres.Any(g => g.Name == genre)
                    select book).Count();
        }
  

        /// <summary>
        /// Получать булевый флаг о том, есть ли книга определенного автора и с определенным названием в библиотеке
        /// </summary>
        public bool FindBookByNameAndAuthor (string name, string author)
        {
            return db.Books
                .Any(n => n.Name == name && n.Author
                .Any(a => a.Name == author));
        }

        /// <summary>
        /// Получение последней вышедшей книги.
        /// </summary>
        public Book? GetLatestBook()
        {
            var date = db.Books.Max(b => b.Year);
            return db.Books.First(b => b.Year == date);
        }
        /// <summary>
        /// Получение списка всех книг, отсортированного в алфавитном порядке по названию.
        /// </summary>
        public IQueryable<Book> GetAllSortedByName()
        {
            return db.Books.OrderBy(b => b.Name);
        }
        
        /// <summary>
        /// Получение списка всех книг, отсортированного в порядке убывания года их выхода.
        /// </summary>
        public IQueryable<Book> GetAllSortedByYears()
        {
            return db.Books.OrderByDescending(b => b.Year);
        }

    }
}
