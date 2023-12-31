﻿using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using BlueFlameHotel.Models.Services;
using BlueFlameHotel.Models;
using BlueFlameHotel.Data;

namespace BlueFlameHotel.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private UserManager<ApplicationUser> userManager;
        private JwtTokenService tokenService;
        public UsersController(UserManager<ApplicationUser> manager, JwtTokenService _tokenService)
        {
            userManager = manager;
            tokenService = _tokenService;
        }
        // ROUTES
        [HttpPost("Register")]
        public async Task<ActionResult<ApplicationUser>> Register(ApplicationUser data)
        {
            // Note: data (RegisterUser) comes from an inbound DTO/Model created for this purpose
            // this.ModelState?  This comes from MVC Binding and shares an interface with the Model
            //var user = await userService.Register(data, this.ModelState);
            var user = new ApplicationUser
            {
                UserName = data.UserName,
                Email = data.Email,
                PhoneNumber = data.PhoneNumber
            };
            var result = await userManager.CreateAsync(user, data.Password);
            if (result.Succeeded)
            {
                await userManager.AddToRolesAsync(user, data.Roles);
                return new ApplicationUser
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Token = await tokenService.GetToken(user, System.TimeSpan.FromMinutes(15)),
                    Roles = await userManager.GetRolesAsync(user)
                };
            }
            // What about our errors?
            foreach (var error in result.Errors)
            {
                var errorKey =
                    error.Code.Contains("Password") ? nameof(data.Password) :
                    error.Code.Contains("Email") ? nameof(data.Email) :
                    error.Code.Contains("UserName") ? nameof(data.UserName) :
                    "";
                ModelState.AddModelError(errorKey, error.Description);
            }
            if (ModelState.IsValid)
            {
                return user;
            }
            return BadRequest(new ValidationProblemDetails(ModelState));
        }
        [HttpPost("Login")]
        public async Task<ActionResult<ApplicationUser>> Login(ApplicationUser data)
        {
            var user = await userManager.FindByNameAsync(data.UserName);
            if (await userManager.CheckPasswordAsync(user, data.Password))
            {
                return new ApplicationUser()
                {
                    Id = user.Id,
                    UserName = user.UserName,
                };
            }
            if (user == null)
            {
                return Unauthorized();
            }
            return BadRequest();
        }
        [Authorize(Policy = "create")]
        [HttpGet("me")]
        public async Task<ApplicationUser> GetUser(ClaimsPrincipal principal)
        {
            var user = await userManager.GetUserAsync(principal);
            return new ApplicationUser
            {
                Id = user.Id,
                UserName = user.UserName
            };
        }
   
    }
}
