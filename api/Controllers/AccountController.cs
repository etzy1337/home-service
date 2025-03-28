using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.AspNetCore.Identity;
using api.Dto.Account;
using api.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;


namespace api.Controllers
{
    [Route("api/account")]
    [ApiController]

    public class AccountController :ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<AppUser> _singInManager;
        public AccountController(UserManager<AppUser> userManager, ITokenService tokenService,SignInManager<AppUser> signInManager)
        {
            _userManager=userManager;
            _tokenService=tokenService;
            _singInManager=signInManager;
        }

        [HttpPost("register")]
        public async Task <IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try 
            {
                if(!ModelState.IsValid) 
                    return BadRequest(ModelState);
                var appUser= new AppUser
                {
                    UserName=registerDto.Email,
                    Email=registerDto.Email
                };

                var createUser= await _userManager.CreateAsync(appUser,registerDto.Password);

                if(createUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(appUser, "User");
                    if(roleResult.Succeeded)
                    {
                        var roles = new List<string> {"User"};
                        var token = _tokenService.CreateToken(appUser,roles);
                        return Ok(
                            new NewUserDto
                            {
                                Email=appUser.Email,
                                Token=token
                            }
                        );
                    }
                    else
                    {
                        return StatusCode(500, roleResult.Errors);
                    }
                }
                else
                {
                    return StatusCode(500, createUser.Errors);
                }
                
            }
            catch(Exception e)
            {
                return StatusCode(500, e);
            }

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.Users.FirstOrDefaultAsync(u=>u.Email==loginDto.Email.ToLower());
            if(user==null) return Unauthorized("Username not found and/or password is incorrect");

            var result = await _singInManager.CheckPasswordSignInAsync(user, loginDto.Password,false);
            if (!result.Succeeded) return Unauthorized("Username not found and/or password is incorrect!");


            var roles = await _userManager.GetRolesAsync(user);
            var token = _tokenService.CreateToken(user,roles);

            return Ok(
                new NewUserDto
                {
                    Email=user.Email,
                    Token=token
                }
            );
        }
    }
}