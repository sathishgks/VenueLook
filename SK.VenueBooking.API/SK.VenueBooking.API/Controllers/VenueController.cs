using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SK.VenueBooking.API.MiddleWare;
using SK.VenueBooking.Misc;
using SK.VenueBooking.Model;
using SK.VenueBooking.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SK.VenueBooking.API.Controllers
{
    [Authorize("Auth")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class VenueController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IBookService _bookService;
        private readonly IVenueService _venueService;
        private readonly IHttpContextAccessor _httpContextAccessor;
      

        public VenueController(IUserService userService,IBookService bookService,IVenueService venueService,IHttpContextAccessor httpContextAccessor)
        {
            _userService = userService;
            _bookService = bookService;
            _venueService = venueService;
            _httpContextAccessor = httpContextAccessor;
            getusername();
        }


        [HttpGet]
        public async Task<IActionResult> GetUserInfo()
        {
            UserInfo userInfo = new UserInfo();
            userInfo.UserName = getusername();
            return Ok(userInfo);
        }

        [HttpGet]
        public async Task<IActionResult> GetHallInfo()
        {
            var hall = await _venueService.GetVenueDetails();
            return Ok(hall);
        }

        [HttpPost]
        public async Task<IActionResult> CreateHall(VenueInfo venueInfo,string tenant)
        {
            DataStore.datastorecollection.Add(VenueConstants.AdminRunTimeTenant,tenant);
            _venueService.AddVenue(venueInfo,getusername());
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> BookHall(BookingInfo bookinginfo)
        {
            _bookService.BookVenue(bookinginfo, getusername());
            return Ok();
        }
        private string getusername()
        {
            var loggeduser = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            if(DataStore.datastorecollection.ContainsKey(VenueConstants.Loggedinuser))
            {
                DataStore.datastorecollection.Remove(VenueConstants.Loggedinuser);
            }
            DataStore.datastorecollection.Add(VenueConstants.Loggedinuser, loggeduser);
            return loggeduser;
        }
    }
}
