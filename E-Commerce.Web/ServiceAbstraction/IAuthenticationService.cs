using Shared.DTO_S.IdentityDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface IAuthenticationService
    {
        //Login 
        public Task<UserToReturnDto> LoginAsync(LoginDto loginDto);
        //Register
        public Task<UserToReturnDto> RegisterAsync(RegisterDto registerDto);

        //Check Email
        public Task<bool> CheckEmailAsync(string email);
        //Get Current User
        //Take Email , return UserToReturnDto
        public Task<UserToReturnDto> GetCurrentUserAsync(string email);
        //Get current User Address
        //Take email , Return AddressDto
        public Task<AddressDto> GetCurrentUserAddressAsync(string email);

        //Update User Address
        //take email and AddressDto , return AddressDto
        public Task<AddressDto> UpdateCurrentuserAddressAsync(string email , AddressDto NewAddress);
    }
}
