using Microsoft.AspNetCore.Identity;
using MvcSocialWeb.Data.DBModel.Users;

namespace MvcSocialWeb.Middlewares.Services
{
    /// <summary>
    /// Класс для генерации тестовых пользователей
    /// </summary>
    public class UserGeneration
    {
        private readonly UserManager<User> _userManager;

        public UserGeneration(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        string[] names = {"Васиилий", "Даниил", "Валерий", "Петр", "Борис" };
        string[] lastnames = { "Иванов", "Петров", "Сидоров", "Морозов", "Тестов" };

        /// <summary>
        /// Метод для получения списка тестовых пользователей 
        /// </summary>
        public List<User> Populate(string value)
        {
            var list = new List<User>();

            if(int.TryParse(value, out int count))
            {
                var rnd = new Random();
                var totalCount = _userManager.Users.Count();

                for(int i = 0; i < count; i++)
                {
                    var testUser = new User()
                    {
                        FirstName = names[rnd.Next(names.Length)],
                        LastName = lastnames[rnd.Next(lastnames.Length)],
                        Email = $"test{i + totalCount}@gmail.com",
                        UserName = $"_testUser{i + totalCount}",
                        BirthDate = DateTime.Now,
                    };

                    list.Add(testUser);
                }
            }

            return list;
        }
    }
}
