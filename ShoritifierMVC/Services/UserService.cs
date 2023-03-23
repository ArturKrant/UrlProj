using Microsoft.AspNetCore.Mvc;
using ShoritifierMVC.Intrerfaces;
using ShoritifierMVC.Models;
using System.Net.Http.Json;

namespace ShoritifierMVC.Services
{
    public class UserService : DataService, IUserService
    {
        string api = "api/User";

        public UserService(string uri) : base(uri)
        {
        }

        public async Task<int> AddUrlsToUserById(ComplexUrl url, int id) =>
            await GetResponseResult<int>(await Client.PostAsJsonAsync($"{api}/add-url/{id}", url));

        public async Task<int> AddUser(User user) =>
            await GetResponseResult<int>(await Client.PostAsJsonAsync($"{api}/add", user));

        public async Task<IEnumerable<ComplexUrl>?> GetUrlsByUserId(int id) =>
            await Client.GetFromJsonAsync<IEnumerable<ComplexUrl>>($"{api}/urls/{id}");

        public async Task<int> GetUserIdByEmail(string email) => await Client.GetFromJsonAsync<int>($"{api}/get/{email}");

        public async Task<User?> LoginUser(User user) =>
            await GetResponseResult<User>(await Client.PostAsJsonAsync($"{api}/login", user));

        public async Task<int> UpdateUser(User user, int id)
        {
            HttpResponseMessage httpResponseMessage = await Client.PutAsJsonAsync($"{api}/update/{id}", user);
            return await GetResponseResult<int>(httpResponseMessage);
        }

    }
}
