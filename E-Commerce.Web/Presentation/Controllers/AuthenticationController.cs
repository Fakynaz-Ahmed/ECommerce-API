using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DTO_S.IdentityDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public class AuthenticationController(IServiceManager _serviceManager) : ApiBaseController
    {
        //Login
        [HttpPost("Login")] //POST BaseURL/api/Authentication/Login
        public async Task<ActionResult<UserToReturnDto>> Login(LoginDto loginDto)
        {
            var Result =await _serviceManager.AuthenticationService.LoginAsync(loginDto);
            return Ok(Result);
        }



        //Register
        [HttpPost("Register")] //POST BaseUrl/api/Authentication/Register
        public async Task<ActionResult<UserToReturnDto>> Register(RegisterDto registerDto)
        {
            var Result=await _serviceManager.AuthenticationService.RegisterAsync(registerDto);
            return Ok(Result);
        }



        //Check Email
        [HttpGet("CheckEmail")] //Get BaseUrl/api/Authentication/CheckEmail
        public async Task<ActionResult<bool>> CheckEmail(string email)
        {
            var Result=await _serviceManager.AuthenticationService.CheckEmailAsync(email);
            return Ok(Result);
        }



        //Get Current User
        [Authorize]
        [HttpGet("CurrentUser")] //User Now is logging in  //Get BaseUrl/api/Authentication/CurrentUser
        public async Task<ActionResult<UserToReturnDto>> GetCurrentUser()
        {
            var Email=User.FindFirstValue(ClaimTypes.Email);
            var Result=await _serviceManager.AuthenticationService.GetCurrentUserAsync(Email!);
            return Ok(Result);
        }


        //Get Current User Address
        [Authorize]
        [HttpGet("UserAddress")] //Get BaseUrl/api/Authentication/UserAddress
        public async Task<ActionResult<AddressDto>> GetCurrentUserAddress()
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);
            var Result=await _serviceManager.AuthenticationService.GetCurrentUserAddressAsync(Email!);
            return Ok(Result);
        }



        //Update Current User Address
        [Authorize]
        [HttpPut("UpdateAddress")] //PUT BaseUrl/api/Authentication/UpdateAddress
        public async Task<ActionResult<AddressDto>> UpdateCurrentUserAddress(AddressDto Updateaddress)
        {
            var Email= User.FindFirstValue(ClaimTypes.Email);
            var Result =await _serviceManager.AuthenticationService.UpdateCurrentuserAddressAsync(Email!, Updateaddress);
            return Ok(Result);
        }
    }
}
//P@ssw0rd