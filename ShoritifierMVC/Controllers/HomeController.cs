using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShoritifierMVC.Intrerfaces;
using ShoritifierMVC.Log;
using ShoritifierMVC.Models;
using System.Diagnostics;
using System.Net;
using System.Security.Claims;

namespace ShoritifierMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService userService;
        private readonly IUrlUsageLogService urlUsageLogService;
        private readonly IUrlService urlService;
        private readonly IMapper mapper;

        public HomeController(IUserService userService, IUrlUsageLogService urlUsageLogService, IUrlService urlService, IMapper mapper)
        {
            this.userService = userService;
            this.urlUsageLogService = urlUsageLogService;
            this.urlService = urlService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var email = User.Claims.FirstOrDefault(c => c.Type == "Email").Value;
                var id = await userService.GetUserIdByEmail(email);
                var complexUrls = await userService.GetUrlsByUserId(id);
                return View(complexUrls.ToList()); 
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index([FromForm]string fullUrl)
        {
            string result;
            if (User.Identity.IsAuthenticated)
            {
                var email = User.Claims.FirstOrDefault(c => c.Type == "Email").Value;
                var id = await userService.GetUserIdByEmail(email);
                result = await urlService.GetShortUrlAuthenticated(fullUrl, id);
            }
            else
            {
                result = await urlService.GetShortUrlAnonymous(fullUrl);
            }
            TempData["Result"] = result;
            return Redirect("/Home/Index");
        }

        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> UrlUsageLogs()
        {
            List<UrlUsageLog> logs = await urlUsageLogService.GetUrlUsageLogs();
            return View(logs);
        }

        [HttpGet]
        public IActionResult Profile()
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
                return Unauthorized();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Profile(UserDto userDto)
        {
            var user = mapper.Map<User>(userDto);
            if (!ModelState.IsValid)
            {
                return View();
            }
            await Task.Run(() => { PopulateUserWithRequiredData(user); });
            var id = await userService.GetUserIdByEmail(user.Email);
            await Task.Run(() => { userService.UpdateUser(user, id); });
            return Redirect("/Home/Index");
        }

        private void PopulateUserWithRequiredData(User user)
        {
            user.Email = User.Claims.FirstOrDefault(c => c.Type == "Email").Value;
            user.Password = User.Claims.FirstOrDefault(c => c.Type == "Password").Value;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}