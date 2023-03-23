using API.Models;
using API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using UrlProject.Services;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService userService;
        private readonly ComplexUrlService complexUrlService;

        public UserController(UserService userService, ComplexUrlService complexUrlService)
        {
            this.userService = userService;
            this.complexUrlService = complexUrlService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<User?> Login(User user) => await userService.LoginUser(user);

        [HttpPost]
        [Route("add")]
        public async Task<int> AddUser(User user) => await userService.AddUser(user);

        [HttpGet]
        [Route("get/{email}")]
        public async Task<int?> GetUserIdByEmail(string email) => await userService.GetUserIdByEmail(email);

        [HttpGet]
        [Route("urls/{id}")]
        public async Task<ICollection<ComplexUrl?>> GetUrlsByUserId(int id) => await userService.GetUrlsByUserId(id);

        [HttpPost]
        [Route("add-url/{id}")]
        public async Task<int> AddUrlsToUserById(ComplexUrl url, int id) => await userService.AddUrlToUserId(url, id);

        [HttpPut]
        [Route("update/{id}")]
        public async Task<int> UpdateUser(User user,int id) => await userService.UpdateUser(user, id);
    }
}
