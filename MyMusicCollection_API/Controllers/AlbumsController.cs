using DataAccess.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyMusicCollection_API.Models;
using AutoMapper;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
namespace MyMusicCollection_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumsController : ControllerBase
    {
        private DataAccess.Data.MusicCollectionBDcontext ctx;
        private readonly IMapper mapper;
        public AlbumsController(IMapper mapper,MusicCollectionBDcontext ctx)
        {
            this.ctx = ctx;
            this.mapper = mapper;
        }
        [HttpGet("GetAllAlbums")]
        public IActionResult GetAllAlbums()
        {
            // Logic to get all albums  
          var albums = ctx.Albums
                .Include(a => a.Artist)
                .Include(a => a.Genres)
                .Include(a => a.Tracks)
                .Include(a => a.RatingsAndReviews)
                .ToList();

            var result = mapper.Map<List<GetAllAlbumModel>>(albums);
            return Ok(result);
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

            var albums = ctx.Albums
               .Where(a => a.AlbumId == id)
               .Include(a => a.Artist)
               .Include(a => a.Genres)
               .Include(a => a.Tracks)
               .Include(a => a.RatingsAndReviews)
               .ToList();

            var result = mapper.Map<List<GetAllAlbumModel>>(albums);
            return Ok(result);
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
        public IActionResult UpdateAlbum(int id,UpdateAlbumModel model)
        {
           if (model == null)
            {
                return BadRequest("Album data is null");
            }

           var album = ctx.Albums
                .Include(a => a.Tracks)
                .FirstOrDefault(a => a.AlbumId == id);

            if (album == null)
            {
                return NotFound($"Album with ID {id} not found");
            }

            mapper.Map(model, album);

            if (model.TrackCount == 0)
            {
                model.TrackCount = album.Tracks.Count;
            }
            
            if (model.AlbumDuration == 0)
            {
                model.AlbumDuration = album.Tracks.Sum(t => t.Duration);
            }
            ctx.SaveChanges();
            var updatedAlbum = mapper.Map<GetAllAlbumModel>(album);
            return Ok(updatedAlbum);
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
