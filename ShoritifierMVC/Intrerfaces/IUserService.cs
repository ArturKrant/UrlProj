using ShoritifierMVC.Models;

namespace ShoritifierMVC.Intrerfaces
{
    public interface IUserService
    {
        Task<User?> LoginUser(User user);

        Task<int> AddUser(User user);

        Task<int> UpdateUser(User user, int id);

        Task<IEnumerable<ComplexUrl?>> GetUrlsByUserId(int id);

        Task<int> AddUrlsToUserById(ComplexUrl url, int id);

        Task<int> GetUserIdByEmail(string email);
    }
}
