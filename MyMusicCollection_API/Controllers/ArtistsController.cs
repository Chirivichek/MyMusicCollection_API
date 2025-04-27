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
    public class ArtistsController : ControllerBase
    {
        private DataAccess.Data.MusicCollectionBDcontext ctx;
        private readonly IMapper mapper;

        public ArtistsController(IMapper mapper, MusicCollectionBDcontext ctx)
        {
            this.ctx = ctx;
            this.mapper = mapper;
        }

        [HttpGet("GetAllArtists")]
        public IActionResult GetAllArtists()
        {
            // Logic to get all artists
            var artists = ctx.Artists
                .Include(a => a.Albums)
                .ToList();
            var result = mapper.Map<List<GetAllArtistModel>>(artists);
            return Ok(result);
        }

        [HttpPost("CreateArtist")]
        public IActionResult CreateArtist(CreateArtistModel model)
        {
            // Logic to create a new artist
            var artist = mapper.Map<Artist>(model);
            ctx.Artists.Add(artist);
            ctx.SaveChanges();
            return Ok("Artist created successfully");
        }
        [HttpPut("UpdateArtist/{id}")]
        public IActionResult UpdateArtist(int id, UpdateArtistModel model)
        {
            // Logic to update an existing artist
            var artist = ctx.Artists.Find(id);
            if (artist == null)
            {
                return NotFound("Artist not found");
            }
            mapper.Map(model, artist);
            ctx.SaveChanges();
            return Ok("Artist updated successfully");
        }
        [HttpDelete("DeleteArtist/{id}")]
        public IActionResult DeleteArtist(int id)
        {
            // Logic to delete an artist
            var artist = ctx.Artists.Find(id);
            if (artist == null)
            {
                return NotFound("Artist not found");
            }
            ctx.Artists.Remove(artist);
            ctx.SaveChanges();
            return Ok("Artist deleted successfully");
        }
    }
}
