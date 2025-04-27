using DataAccess.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyMusicCollection_API.Models;
using AutoMapper;
using DataAccess.Entities;
namespace MyMusicCollection_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumController : ControllerBase
    {
        private DataAccess.Data.MusicCollectionBDcontext ctx;
        private readonly IMapper mapper;
        public AlbumController(IMapper mapper,MusicCollectionBDcontext ctx)
        {
            this.ctx = ctx;
        }
        [HttpGet("GetAllAlbums")]
        public IActionResult GetAllAlbums()
        {
            // Logic to get all albums  
            return Ok(ctx.Albums.ToList());
        }
        [HttpGet("GetAlbumById/{id}")]
        public IActionResult GetAlbumById(int id)
        {
            // Logic to get an album by ID  
            var item = ctx.Albums.Find(id);

            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }
        [HttpPost("CreateAlbum")]
        public IActionResult CreateAlbum(CreateAlbumModel model)
        {
            // Logic to create a new album  
            ctx.Albums.Add(mapper.Map<Album>(model));
            ctx.SaveChanges();
            return Created();
        }
        [HttpPut("UpdateAlbum")]
        public IActionResult UpdateAlbum(UpdateAlbumModel model)
        {
            // Logic to edit an existing album
            var item = ctx.Albums.Find(model.AlbumId);

            if (item == null)
            {
                return NotFound();
            }
            ctx.Albums.Update(mapper.Map<Album>(model));
            ctx.SaveChanges();
            return Ok();
        }
        [HttpDelete("DeleteAlbum/{id}")]
        public IActionResult DeleteAlbum(int id)
        {
            var item = ctx.Albums.Find(id);

            if (item == null)
            {
                return NotFound();
            }

            ctx.Albums.Remove(item);
            ctx.SaveChanges();

            return NoContent();
        }
    }

}
