using DataAccess.Entities;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using MyMusicCollection_API.Models;

namespace MyMusicCollection_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private DataAccess.Data.MusicCollectionBDcontext ctx;
        private readonly IMapper mapper;
        public UserController(IMapper mapper, MusicCollectionBDcontext ctx)
        {
            this.ctx = ctx;
            this.mapper = mapper;
        }

        [HttpGet("GetAllUsers")]
        public IActionResult GetAllUsers()
        {
            // Logic to get all users
            var users = ctx.Users
                .Include(u => u.PlayLists)
                .Include(u => u.RatingsAndReviews)
                .Include(u => u.UserCollections)
                .ToList();
            var result = mapper.Map<List<GetAllUserModel>>(users);
            return Ok(result);
        }
        [HttpGet("GetUserById/{id}")]
        public IActionResult GetUserById(int id)
        {
            // Logic to get a specific user by ID
            var user = ctx.Users
                .Include(u => u.PlayLists)
                .Include(u => u.RatingsAndReviews)
                .Include(u => u.UserCollections)
                .FirstOrDefault(u => u.UserId == id);
            if (user == null)
            {
                return NotFound("User not found");
            }
            var result = mapper.Map<GetAllUserModel>(user);
            return Ok(result);
        }
        [HttpPost("CreateUser")]
        public IActionResult CreateUser(CreateUserModel model)
        {
            // Logic to create a new user
            var user = mapper.Map<User>(model);
            ctx.Users.Add(user);
            ctx.SaveChanges();
            return Ok("User created successfully");
        }
        [HttpPut("UpdateUser/{id}")]
        public IActionResult UpdateUser(int id, UpdateUserModel model)
        {
            // Logic to update an existing user
            var user = ctx.Users.Find(id);
            if (user == null)
            {
                return NotFound("User not found");
            }
            mapper.Map(model, user);
            ctx.SaveChanges();
            return Ok("User updated successfully");
        }
        [HttpDelete("DeleteUser/{id}")]
        public IActionResult DeleteUser(int id)
        {
            // Logic to delete a user
            var user = ctx.Users.Find(id);
            if (user == null)
            {
                return NotFound("User not found");
            }
            ctx.Users.Remove(user);
            ctx.SaveChanges();
            return Ok("User deleted successfully");
        }
    }
}
