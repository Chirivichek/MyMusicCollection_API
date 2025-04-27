using AutoMapper;
using DataAccess.Data;
using DataAccess.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyMusicCollection_API.Models;

namespace MyMusicCollection_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserCollectionsController : ControllerBase
    {

        private readonly MusicCollectionBDcontext ctx;
        private readonly IMapper mapper;

        public UserCollectionsController(IMapper mapper, MusicCollectionBDcontext ctx)
        {
            this.ctx = ctx;
            this.mapper = mapper;
        }
        [HttpGet("GetAllUserCollections")]
        public IActionResult GetAllUserCollections()
        {
            var userCollections = ctx.UserCollections
                .Include(uc => uc.User)
                .Include(uc => uc.Album)
                .ToList();
            var result = mapper.Map<List<GetAllUserCollectionModel>>(userCollections);
            return Ok(result);

        }

     
        [HttpGet("GetUserCollectionById/{id}")]
        public IActionResult GetUserCollectionById(int id)
        {
            var userCollection = ctx.UserCollections
                .Include(uc => uc.User)
                .Include(uc => uc.Album)
                .FirstOrDefault(uc => uc.UserCollectionId == id);
            if (userCollection == null)
            {
                return NotFound("User collection not found");
            }
            var result = mapper.Map<GetAllUserCollectionModel>(userCollection);
            return Ok(result);
        }
        [HttpPost("CreateUserCollection")]
        public IActionResult CreateUserCollection(CreateUserCollectionModel model)
        { 
            var userCollection = mapper.Map<UserCollection>(model);
            ctx.UserCollections.Add(userCollection);
            ctx.SaveChanges();
            return Ok("User collection created successfully");
        }

        [HttpPut("UpdateUserCollection/{id}")]
        public IActionResult UpdateUserCollection(int id, UpdateUserCollectionModel model)
        {
            var userCollection = ctx.UserCollections.Find(id);
            if (userCollection == null)
            {
                return NotFound("User collection not found");
            }
            mapper.Map(model, userCollection);
            ctx.SaveChanges();
            return Ok("User collection updated successfully");
        }
        [HttpDelete("DeleteUserCollection/{id}")]
        public IActionResult DeleteUserCollection(int id)
        {
            var userCollection = ctx.UserCollections.Find(id);
            if (userCollection == null)
            {
                return NotFound("User collection not found");
            }
            ctx.UserCollections.Remove(userCollection);
            ctx.SaveChanges();
            return Ok("User collection deleted successfully");
        }

    }
}
