using AutoMapper;
using DomainLayer.Exceptions;
using DomainLayer.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServiceAbstraction;
using Shared.DTO_S.IdentityDtos;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class AuthenticationService(UserManager<ApplicationUser> _userManager, IConfiguration _configuration, IMapper _mapper) : IAuthenticationService
    {
        public async Task<bool> CheckEmailAsync(string email)
        {
            var User = await _userManager.FindByEmailAsync(email);
            return User is not null;
        }

        public async Task<AddressDto> GetCurrentUserAddressAsync(string email)
        {
            var User = await _userManager.Users.Include(U => U.Address).FirstOrDefaultAsync(U => U.Email == email)
                       ?? throw new UserNotFoundException(email);
            if (User.Address is null) throw new AddressNotFoundException(User.UserName);
            var AddressDto = _mapper.Map<Address, AddressDto>(User.Address);
            return AddressDto;
        }

        public async Task<UserToReturnDto> GetCurrentUserAsync(string email)
        {
            var User = await _userManager.FindByEmailAsync(email);
            if (User is null) throw new UserNotFoundException(email);
            return new UserToReturnDto()
            {
                Email = User.Email!,
                DisplayName = User.DisplayName,
                Token = await GenerateTokenAsync(User)
            };

        }


        public async Task<AddressDto> UpdateCurrentuserAddressAsync(string email, AddressDto NewAddress)
        {
            var User = await _userManager.Users.Include(U => U.Address).FirstOrDefaultAsync(U => U.Email == email)
                         ?? throw new UserNotFoundException(email);
            //update Address
            if (User.Address is not null)
            {
                User.Address.Country = NewAddress.Country;
                User.Address.City = NewAddress.City;
                User.Address.Street = NewAddress.Street;
                User.Address.FirstName = NewAddress.FirstName;
                User.Address.LastName = NewAddress.LastName;
            }
            //Add New Address
            else
            {
                User.Address = _mapper.Map<AddressDto, Address>(NewAddress);
            }

           await _userManager.UpdateAsync(User);
            return _mapper.Map<Address, AddressDto>(User.Address);
        }


        public async Task<UserToReturnDto> LoginAsync(LoginDto loginDto)
        {
            //Check Email is Exist Or Not
            var User = await _userManager.FindByEmailAsync(loginDto.Email) ?? throw new UserNotFoundException(loginDto.Email);

            //Check Password correct or not 
            var IsPasswordValid = await _userManager.CheckPasswordAsync(User, loginDto.Password);
            if (!IsPasswordValid)
                throw new UnauthorizedException();

            //return UserToReturnDto (Email,Token,DisplayName)
            return new UserToReturnDto()
            {
                Email = User.Email,
                DisplayName = User.DisplayName,
                Token = await GenerateTokenAsync(User)
            };
        }


        public async Task<UserToReturnDto> RegisterAsync(RegisterDto registerDto)
        {
            //create user (Mapping RegisterDto to ApplicationUser)
            var User = new ApplicationUser()
            {
                Email = registerDto.Email,
                DisplayName = registerDto.DisplayName,
                PhoneNumber = registerDto.PhoneNumber,
                UserName = registerDto.UserName,
            };
            //create user with password
            var Result = await _userManager.CreateAsync(User, registerDto.Password);

            //return UserToReturnDto
            if (Result.Succeeded)
            {
                return new UserToReturnDto()
                {
                    Email = User.Email,
                    DisplayName = User.DisplayName,
                    Token = await GenerateTokenAsync(User)
                };
            }
            //Throw BadRequest Exceptions
            else
            {
                var Errors = Result.Errors.Select(E => E.Description).ToList();
                throw new BadRequestException(Errors);
            }
        }


        private async Task<string> GenerateTokenAsync(ApplicationUser User)
        {
            var Claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email , User.Email!),
                new Claim(ClaimTypes.Name , User.UserName!),
                new Claim(ClaimTypes.NameIdentifier , User.Id!)
            };
            var Roles = await _userManager.GetRolesAsync(User);
            foreach (var Role in Roles)
            {
                Claims.Add(new Claim(ClaimTypes.Role, Role));
            }
            var SecretKey = _configuration.GetSection("JWTOptions")["SecretKey"];
            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            var Creds = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);

            var Token = new JwtSecurityToken(
                issuer: _configuration["JWTOptions:Issuer"],
                audience: _configuration.GetSection("JWTOptions")["Audience"],
                claims: Claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: Creds
            );

            return new JwtSecurityTokenHandler().WriteToken(Token);





        }


    }
}
