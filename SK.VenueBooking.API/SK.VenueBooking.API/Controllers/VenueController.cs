using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SK.VenueBooking.Model;
using SK.VenueBooking.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SK.VenueBooking.API.Controllers
{
    
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class VenueController : ControllerBase
    {
        private readonly IUserService _userService;

        public VenueController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpGet]
        public async Task<IActionResult> GetUserInfo()
        {
            var user = await _userService?.GetUserInfo("adminuser@sksvenue.onmicrosoft.com");
            return Ok(user);
        }
    }
}
