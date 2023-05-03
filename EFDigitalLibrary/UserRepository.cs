using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDigitalLibrary
{
    public class UserRepository
    {
        private AppContext db;

        public UserRepository(AppContext db)
        {
            this.db = db;
        }

        /// <summary>
        /// выбор объекта из БД по его идентификатору
        /// </summary>
        public User GetByIdUser(int id)
        {
            return db.Users.Find(id);
        }

        /// <summary>
        /// выбор всех объектов
        /// </summary>
        public IQueryable<User> GetAllUsers()
        {
            return db.Users;
        }

        /// <summary>
        /// добавление объекта в БД
        /// </summary>
        public int AddUser(User user)
        {
            db.Users.Add(user);
            return db.SaveChanges();
        }

        /// <summary>
        /// удаление объекта из БД
        /// </summary>
        public int DeleteUser(User user)
        {
            db.Users.Remove(user);
            return db.SaveChanges();
        }

        /// <summary>
        /// обновление года выпуска книги
        /// </summary>
        public int UpdateName(int id, string name)
        {
            var user = GetByIdUser(id);
            if (user == null)
                return 0;
            user.Name = name;
            return db.SaveChanges();
        }

        /// <summary>
        /// Получать булевый флаг о том, есть ли определенная книга на руках у пользователя.
        /// </summary>
        public bool HasBook(User user, Book book)
        {
            return db.Users.Any(u => u.Equals(user) && u.Books.Contains(book));
        }

        /// <summary>
        /// Получать количество книг на руках у пользователя.
        /// </summary>
        public int BookCount(User user)
        {
            return db.Books.Where(b => b.Users.Any(u => u.Equals(user))).Count();
        }
    }
}
