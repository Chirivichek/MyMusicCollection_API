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
    public class TracksController : ControllerBase
    {
        private readonly MusicCollectionBDcontext ctx;
        private readonly IMapper mapper;

        public TracksController(IMapper mapper, MusicCollectionBDcontext ctx)
        {
            this.ctx = ctx;
            this.mapper = mapper;
        }


        [HttpGet("GetAllTrack")]
        public IActionResult GetAllTrack()
        {  
            var tracks = ctx.Tracks
                .Include(t => t.Album)
                .ToList(); 

            var result = mapper.Map<List<GetAllTrackModel>>(tracks);
            return Ok(result);
        }
        [HttpGet("GetTrackById/{id}")]
        public IActionResult GetTrackById(int id)
        {
            var track = ctx.Tracks
                .Include(t => t.Album)
                .FirstOrDefault(t => t.TrackId == id);
            if (track == null)
            {
                return NotFound("Track not found");
            }
            var result = mapper.Map<GetAllTrackModel>(track);
            return Ok(result);
        }
        
        [HttpPut("{trackId}")]
        public IActionResult UpdateTrack(int trackId, UpdateTrackModel model)
        {
            var track = ctx.Tracks
                .Include(t => t.Album)
                .FirstOrDefault(t => t.TrackId == trackId);

            if (track == null)
            {
                return NotFound("Track not found");
            }
            mapper.Map(model, track);

            var album = ctx.Albums
                .Include(a => a.Tracks)
                .FirstOrDefault(a => a.AlbumId == track.AlbumId);

            if (album != null)
            {
                album.AlbumDuration = album.Tracks.Sum(t => t.Duration);
                album.TrackCount = album.Tracks.Count;
            }


            ctx.SaveChanges();
            return Ok("Track updated successfully");
        }

        [HttpDelete("{trackId}")]
        public IActionResult DeleteTrack(int trackId)
        {
            var track = ctx.Tracks
                .Include(t => t.Album)
                .FirstOrDefault(t => t.TrackId == trackId);
            if (track == null)
            {
                return NotFound("Track not found");
            }
            ctx.Tracks.Remove(track);
            var album = ctx.Albums
                .Include(a => a.Tracks)
                .FirstOrDefault(a => a.AlbumId == track.AlbumId);
            if (album != null)
            {
                album.AlbumDuration = album.Tracks.Sum(t => t.Duration);
                album.TrackCount = album.Tracks.Count;
            }
            ctx.SaveChanges();
            return Ok("Track deleted successfully");
        }
    }
    }
