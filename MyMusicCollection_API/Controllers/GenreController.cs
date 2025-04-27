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
    public class GenreController : ControllerBase
    {
        private DataAccess.Data.MusicCollectionBDcontext ctx;
        private readonly IMapper mapper;
        public GenreController(IMapper mapper, MusicCollectionBDcontext ctx)
        {
            this.ctx = ctx;
            this.mapper = mapper;
        }
        [HttpGet("GetAllGenre")]
        public IActionResult GetAllGenre()
        {
            // Logic to get all genres
            var genres = ctx.Genres
                .Include(g => g.Albums)
                .ToList();
            var result = mapper.Map<List<GetAllGenreModel>>(genres);
            return Ok(result);
        }
        [HttpPost("CreateGenre")]
        public IActionResult CreateGenre(CreateGenreModel model)
        {
            // Logic to create a new genre
            var genre = mapper.Map<Genre>(model);
            ctx.Genres.Add(genre);
            ctx.SaveChanges();
            return Ok("Genre created successfully");
        }
        [HttpPut("UpdateGenre/{id}")]
        public IActionResult UpdateGenre(int id, UpdateGenreModel model)
        {
            // Logic to update an existing genre
            var genre = ctx.Genres.Find(id);
            if (genre == null)
            {
                return NotFound("Genre not found");
            }
            mapper.Map(model, genre);
            ctx.SaveChanges();
            return Ok("Genre updated successfully");
        }
        [HttpDelete("DeleteGenre/{id}")]
        public IActionResult DeleteGenre(int id)
        {
            // Logic to delete a genre
            var genre = ctx.Genres.Find(id);
            if (genre == null)
            {
                return NotFound("Genre not found");
            }
            ctx.Genres.Remove(genre);
            ctx.SaveChanges();
            return Ok("Genre deleted successfully");
        }
    }
}
