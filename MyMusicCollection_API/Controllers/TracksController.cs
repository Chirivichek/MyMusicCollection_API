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

            var result = mapper.Map<List<GetAllTrack>>(tracks);
            return Ok(result);
        }

        [HttpPost("{albumId}")]
        public IActionResult AddTrackToAlbum(int albumId, UpdateTrackModel model)
        {
            var album = ctx.Albums
                .Include(a => a.Tracks)
                .FirstOrDefault(a => a.AlbumId == albumId);
            if (album == null)
            {
                return NotFound("Album not found");
            }

            var newTrack = mapper.Map<Track>(model);
            newTrack.AlbumId = album.AlbumId;
            album.Tracks.Add(newTrack);

            album.TrackCount = album.Tracks.Count;
            album.AlbumDuration = album.Tracks.Sum(t => t.Duration);

            ctx.SaveChanges();
            return Ok("Track added to album successfully");
        }
        //[HttpGet("{PlaylistId}")]
        //public IActionResult GetAllTracksInPlaylist(int PlaylistId)
        //{
        //    var playlist = ctx.PlayLists
        //        .Include(p => p.Tracks)
        //        .FirstOrDefault(p => p.PlayListId == PlaylistId);

        //    if (playlist == null)
        //    {
        //        return NotFound("Playlist not found");
        //    }

        //   var result = mapper.Map<List<GetAllTracksInPlaylist>>(playlist.Tracks);
        //    return Ok(result);
        //}
        //[HttpGet("GetAllTracksInPlaylist/{PlaylistId}")]

        //public IActionResult GetAllTracksInPlaylist(int PlaylistId)
        //{

        //}

        //[HttpPost("AddTrackToPlaylist/{PlaylistId}")]
        //public IActionResult AddTrackToPlaylist(int? TrackId, int PlaylistId, UpdateTrackModel model)
        //{
        //    var playlist = ctx.PlayLists
        //        .Include(p => p.Tracks)
        //        .FirstOrDefault(p => p.PlayListId == PlaylistId);

        //    if (playlist == null)
        //    {
        //        return NotFound("Playlist not found");
        //    }

        //    Track trackToAdd;

        //    if (TrackId.HasValue && TrackId != 0) // Якщо передано TrackId, додаємо існуючий трек
        //    {
        //        trackToAdd = ctx.Tracks
        //            .Include(t => t.PlayLists)
        //            .FirstOrDefault(t => t.TrackId == TrackId.Value);

        //        if (trackToAdd == null)
        //        {
        //            return NotFound("Track not found");
        //        }

        //        // Перевіряємо, чи трек уже є в плейлисті
        //        if (trackToAdd.PlayLists.Any(p => p.PlayListId == PlaylistId))
        //        {
        //            return BadRequest("Track is already in the playlist");
        //        }

        //        // Додаємо плейлист до колекції треку (EF Core автоматично оновить проміжну таблицю)
        //        trackToAdd.PlayLists.Add(playlist);
        //    }
        //    else // Якщо TrackId не передано, створюємо новий трек
        //    {
        //        if (model == null)
        //        {
        //            return BadRequest("Track data is required to create a new track");
        //        }

        //        trackToAdd = mapper.Map<Track>(model);
        //        trackToAdd.PlayLists = new List<PlayList> { playlist }; // Встановлюємо зв’язок із плейлистом
        //        ctx.Tracks.Add(trackToAdd); // Додаємо новий трек у контекст
        //    }


        //    ctx.SaveChanges();
        //    return Ok("Track added to playlist successfully");
        //}

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
