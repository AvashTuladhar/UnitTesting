using Practise.Models;

namespace Practise.Repositories
{
    public interface IUserRepository
    {
        List<User> GetAllUsers();
        User? GetUserByID(int id);
        void DeleteUser(User user);
        User CreateUser(User user);
        User UpdateUser(User user);
    }
    public class UserRepository : IUserRepository
    {
        List<User> users;
        public UserRepository()
        {
            users = new List<User>()
            {
                new User(){ Id = 1, Name ="Avash"},
                new User(){ Id = 2, Name ="Avinash"},
                new User(){ Id = 3, Name ="Nitesh"},
            };
        }
        public User CreateUser(User user)
        {
            users.Add(user);
            return user;
        }

        public void DeleteUser(User user)
        {
            users.Remove(user);
        }

        public List<User> GetAllUsers()
        {
            return users;
        }

        public User? GetUserByID(int id)
        {
            return users.FirstOrDefault(x => x.Id == id);
        }

        public User UpdateUser(User user)
        {
            var result = users.Single(x=>x.Id==user.Id);
            result.Name = user.Name;                
            return user;
        }
    }
}
