using API.DAL;
using API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace API.Services
{
    public class UserService
    {
        private readonly DataContext data;

        public UserService(DataContext data) => this.data = data;

        public async Task<User?> LoginUser(User user) =>
            await data.Users.SingleOrDefaultAsync(u => u.Email!.Equals(user.Email) && u.Password!.Equals(user.Password));

        public async Task<int> AddUser(User user) {
            await data.Users.AddAsync(user);
            return await data.SaveChangesAsync();
        }

        public async Task<int> UpdateUser(User user, int id)
        {
            await ChangeUserBasedOnChangedProperties(user, id);
            data.Users.Add(user);
            return await data.SaveChangesAsync();
        }

        private async Task ChangeUserBasedOnChangedProperties(User user, int id)
        {
            var existingUser = await GetUserById(id);
            user.Id = id;
            var poperties = existingUser.GetType().GetProperties();
            foreach (var property in poperties)
            {
                var propertyValue = property.GetValue(user);
                if (propertyValue == null)
                    property.SetValue(user, property.GetValue(existingUser));
            }
            data.Users.Remove(existingUser);
        }

        public async Task<ICollection<ComplexUrl?>> GetUrlsByUserId(int id) {
            var user = await GetUserById(id);
            await data.Entry(user).Collection(u => u.ComplexUrls!).LoadAsync();
            return user.ComplexUrls;
                }
        
        public async Task<int> AddUrlToUserId(ComplexUrl complexUrl, int id)
        {
            complexUrl.UserId = id;
            await data.ComplexUrls.AddAsync(complexUrl);
            return await data.SaveChangesAsync();
        }
        
        public async Task<User?> GetUserById(int id) => await data.Users.SingleOrDefaultAsync(u => u.Id!.Equals(id));

        public async Task<int?> GetUserIdByEmail(string email)
        {
            var user = await data.Users.SingleOrDefaultAsync(u => u.Email!.Equals(email));
            return user.Id;
        }

    }
}
